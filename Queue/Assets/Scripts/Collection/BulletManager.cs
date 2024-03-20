/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Queue<GameObject> bulletQueue = new Queue<GameObject>(); // �Ѿ��� �����ϴ� ť
    public int ballCount = 1; // �Ѿ� ����
    private bool is_start = false; // ���� ���� ����
    public GameObject ballPrefab; // �Ѿ� ������
    public Transform target; // ��ǥ ����
    private int shotCount = 10; // �߻� Ƚ��
    private bool isShooting = false; // �߻� ������ ����

    void Start()
    {
        for (int i = 0; i < 10; i++) // �ʱ⿡ 10���� �Ѿ��� �����Ͽ� ť�� �߰�
        {
            Enqueue(CreateBullet());
        }
    }

    void Update()
    {
        if (is_start && bulletQueue.Count () > 0 && shotCount > 0) // ������ ���۵Ǿ��� �Ѿ��� ������ �߻� Ƚ���� ���� ���� ��
        {
            if (!isShooting) // �߻� ���� �ƴ� ��
            {
                GameObject bullet = Dequeue(); // ť���� �Ѿ� ��������
                bullet.transform.position = transform.position; // �Ѿ� ��ġ ����
                StartCoroutine(MoveBullet(bullet)); // �Ѿ� �̵� �ڷ�ƾ ����
                shotCount--; // �߻� Ƚ�� ����
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.is_start = true; // ���콺 Ŭ�� �� �߻� ����
        }
    }

    // �Ѿ� ����
    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(ballPrefab); // �������� �̿��Ͽ� �Ѿ� ����
        bullet.SetActive(false); // ��Ȱ��ȭ ���·� ����
        return bullet;
    }

    // ť�� �Ѿ� �߰�
    private void Enqueue(GameObject bullet)
    {
        bulletQueue.Enqueue(bullet);
    }

    // ť���� �Ѿ� ���� �� ��ȯ
    private GameObject Dequeue()
    {
        return bulletQueue.Dequeue();
    }

    // �Ѿ� �̵� �ڷ�ƾ
    IEnumerator MoveBullet(GameObject bullet)
    {
        isShooting = true; // �߻� ������ ����
        bullet.SetActive(true); // �Ѿ� Ȱ��ȭ

        while (Vector3.Distance(bullet.transform.position, target.position) > 0.1f) // ��ǥ ������ ������ ������ �ݺ�
        {
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.right * 10f;
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target.position, Time.deltaTime * 8.0f); // �Ѿ��� ��ǥ ����(������ ����)���� �̵�
            yield return null;
        }

        bullet.SetActive(false); // ��ǥ������ ������ �Ѿ� ��Ȱ��ȭ
        Enqueue(bullet); // �Ѿ��� �ٽ� ť�� �߰�
        isShooting = false; // �߻� ����
    }
}
*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform spawnPoint; // �Ѿ� ���� ��ġ
    public GameObject redBox; // ���� ����
    private Queue<GameObject> bulletQueue = new Queue<GameObject>(); // �Ѿ��� ���� Queue

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
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
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
*/

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



