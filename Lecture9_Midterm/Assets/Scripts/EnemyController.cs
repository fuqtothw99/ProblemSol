using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class EnemyController : MonoBehaviour
{
    public float patrolSpeed = 2f; // ���ƴٴϴ� �ӵ�
    public Vector3 patrolAreaSize; // ���ƴٴϴ� ���� ũ��
    public Vector3 patrolAreaCenter; // ���ƴٴϴ� ���� �߽�
    public float wanderDuration = 3f; // �θ����Ÿ��� �ð�

    public Transform player; // �÷��̾� Transform

    private Camera enemyCamera;
    private bool isWandering = false; // �θ����Ÿ��� �������� ����
    private Vector3 patrolStartPosition; // ���ƴٴϱ� ���� ��ġ
    private Vector3 patrolEndPosition; // ���ƴٴϱ� �� ��ġ
    private float wanderTimer = 0f; // �θ����Ÿ��� Ÿ�̸�

    void Start()
    {
        // ī�޶� ������Ʈ ��������
        enemyCamera = GetComponent<Camera>();
        if (enemyCamera == null)
        {
            Debug.LogError("Camera component not found on the object.");
        }
        else
        {
            // Perspective �Ӽ� ����
            enemyCamera.orthographic = false;

            // �þ߰� ����
            enemyCamera.fieldOfView = 45f;
        }

        // �÷��̾� ã��
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ���ƴٴϱ� ���� ��ġ ����
        patrolStartPosition = GetRandomPositionInPatrolArea();
        patrolEndPosition = GetRandomPositionInPatrolArea();
    }

    void Update()
    {
        // �÷��̾� �߰�
        if (PlayerInSight())
        {
            ChasePlayer();
        }
        else if (!isWandering)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // �� ���� �� ���̸� �̵��ϵ��� ����
        transform.position = Vector3.MoveTowards(transform.position, patrolEndPosition, patrolSpeed * Time.deltaTime);

        // ��ǥ ��ġ�� �����ϸ� ���� ��ǥ ��ġ ����
        if (Vector3.Distance(transform.position, patrolEndPosition) < 0.1f)
        {
            // ���� ������ ��ġ�� ���� ��ġ�� �����ϰ� ���ο� �� ��ġ�� ����
            patrolStartPosition = patrolEndPosition;
            patrolEndPosition = GetRandomPositionInPatrolArea();
        }
    }

    // ������ ��ġ�� ��� �޼���
    Vector3 GetRandomPositionInPatrolArea()
    {
        float minX = patrolAreaCenter.x - patrolAreaSize.x / 2f;
        float maxX = patrolAreaCenter.x + patrolAreaSize.x / 2f;
        float minZ = patrolAreaCenter.z - patrolAreaSize.z / 2f;
        float maxZ = patrolAreaCenter.z + patrolAreaSize.z / 2f;

        return new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
    }

    // �÷��̾ �þ� ���� �����ϴ� �޼���
    bool PlayerInSight()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < enemyCamera.fieldOfView / 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }

    // �÷��̾� �߰�
    void ChasePlayer()
    {
        // �÷��̾� ������ �̵�
        Vector3 destination = player.position;

        // ��ǥ ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, destination, patrolSpeed * Time.deltaTime);
    }

    // ������ �󿡼� ������ ǥ���ϱ� ���� �޼���
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(patrolAreaCenter, patrolAreaSize);
    }
}
