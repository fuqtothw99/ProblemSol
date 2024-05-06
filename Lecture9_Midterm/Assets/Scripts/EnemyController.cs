using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class EnemyController : MonoBehaviour
{
    public float patrolSpeed = 2f; // 돌아다니는 속도
    public Vector3 patrolAreaSize; // 돌아다니는 범위 크기
    public Vector3 patrolAreaCenter; // 돌아다니는 범위 중심
    public float wanderDuration = 3f; // 두리번거리는 시간

    public Transform player; // 플레이어 Transform

    private Camera enemyCamera;
    private bool isWandering = false; // 두리번거리는 상태인지 여부
    private Vector3 patrolStartPosition; // 돌아다니기 시작 위치
    private Vector3 patrolEndPosition; // 돌아다니기 끝 위치
    private float wanderTimer = 0f; // 두리번거리는 타이머

    void Start()
    {
        // 카메라 컴포넌트 가져오기
        enemyCamera = GetComponent<Camera>();
        if (enemyCamera == null)
        {
            Debug.LogError("Camera component not found on the object.");
        }
        else
        {
            // Perspective 속성 유지
            enemyCamera.orthographic = false;

            // 시야각 조절
            enemyCamera.fieldOfView = 45f;
        }

        // 플레이어 찾기
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // 돌아다니기 시작 위치 설정
        patrolStartPosition = GetRandomPositionInPatrolArea();
        patrolEndPosition = GetRandomPositionInPatrolArea();
    }

    void Update()
    {
        // 플레이어 추격
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
        // 두 개의 점 사이를 이동하도록 설정
        transform.position = Vector3.MoveTowards(transform.position, patrolEndPosition, patrolSpeed * Time.deltaTime);

        // 목표 위치에 도달하면 다음 목표 위치 설정
        if (Vector3.Distance(transform.position, patrolEndPosition) < 0.1f)
        {
            // 현재 도착한 위치를 시작 위치로 설정하고 새로운 끝 위치를 얻어옴
            patrolStartPosition = patrolEndPosition;
            patrolEndPosition = GetRandomPositionInPatrolArea();
        }
    }

    // 랜덤한 위치를 얻는 메서드
    Vector3 GetRandomPositionInPatrolArea()
    {
        float minX = patrolAreaCenter.x - patrolAreaSize.x / 2f;
        float maxX = patrolAreaCenter.x + patrolAreaSize.x / 2f;
        float minZ = patrolAreaCenter.z - patrolAreaSize.z / 2f;
        float maxZ = patrolAreaCenter.z + patrolAreaSize.z / 2f;

        return new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
    }

    // 플레이어를 시야 내에 감지하는 메서드
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

    // 플레이어 추격
    void ChasePlayer()
    {
        // 플레이어 쪽으로 이동
        Vector3 destination = player.position;

        // 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, destination, patrolSpeed * Time.deltaTime);
    }

    // 에디터 상에서 범위를 표시하기 위한 메서드
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(patrolAreaCenter, patrolAreaSize);
    }
}
