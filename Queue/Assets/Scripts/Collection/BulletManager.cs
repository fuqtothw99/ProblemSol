using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform spawnPoint; // �Ѿ� ���� ��ġ
    public GameObject redBox; // ���� ����
   
    private Queue<GameObject> bulletQueue = new Queue<GameObject>(); // �Ѿ��� ���� Queue
    private bool canFire = true; // �Ѿ� �߻� ���� ���θ� ��Ÿ���� ����

    void Start()
    {
        // �Ѿ��� Queue�� �߰�
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            bullet.SetActive(false);
            bulletQueue.Enqueue(bullet);
        }
    }

    void Update()
    {
        // ���콺 Ŭ�� �� �Ѿ� �߻�
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            StartCoroutine(FireBullets());
        }
    }

    void FireBullet()
    {
        if (bulletQueue.Count() > 0)
        {
            GameObject bullet = bulletQueue.Dequeue();
            bullet.SetActive(true);
            bullet.transform.position = spawnPoint.position;
            StartCoroutine(MoveBullet(bullet));
        }
    }

    IEnumerator FireBullets()
    {
        canFire = false; // �Ѿ� �߻� �߿��� �ٽ� �߻����� ���ϵ��� ����

        for (int i = 0; i < 10; i++)
        {
            if (bulletQueue.Count() > 0)
            {
                GameObject bullet = bulletQueue.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = spawnPoint.position;
                StartCoroutine(MoveBullet(bullet));
            }
            yield return new WaitForSeconds(0.1f); // �� �Ѿ� ���� ������ ������ �� �ֽ��ϴ�.
        }

        yield return new WaitForSeconds(1f); // �Ѿ��� ��� �߻�� �� ��� ����մϴ�.
        canFire = true; // �Ѿ� �߻簡 �������Ƿ� �ٽ� �߻� �����ϵ��� ����
    }

    IEnumerator MoveBullet(GameObject bullet)
    {
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * 10f;

        while (bullet.activeSelf)
        {
            bullet.transform.Translate(Vector2.right * Time.deltaTime * 5f);

            yield return null;

            // �Ѿ��� ���� ���ڿ� ������
            if (redBox != null && redBox.GetComponent<Collider2D>().bounds.Contains(bullet.transform.position))
            {
                bullet.SetActive(false); // �Ѿ� ��Ȱ��ȭ
                bulletQueue.Enqueue(bullet); // �Ѿ��� �ٽ� Queue�� �߰�
            }

        }
    }
}



