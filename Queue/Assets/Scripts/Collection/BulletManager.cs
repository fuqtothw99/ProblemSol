using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform spawnPoint; // 총알 생성 위치
    public GameObject redBox; // 빨간 상자
   
    private Queue<GameObject> bulletQueue = new Queue<GameObject>(); // 총알을 담을 Queue
    private bool canFire = true; // 총알 발사 가능 여부를 나타내는 변수

    void Start()
    {
        // 총알을 Queue에 추가
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            bullet.SetActive(false);
            bulletQueue.Enqueue(bullet);
        }
    }

    void Update()
    {
        // 마우스 클릭 시 총알 발사
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
        canFire = false; // 총알 발사 중에는 다시 발사하지 못하도록 설정

        for (int i = 0; i < 10; i++)
        {
            if (bulletQueue.Count() > 0)
            {
                GameObject bullet = bulletQueue.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = spawnPoint.position;
                StartCoroutine(MoveBullet(bullet));
            }
            yield return new WaitForSeconds(0.1f); // 각 총알 간의 간격을 조절할 수 있습니다.
        }

        yield return new WaitForSeconds(1f); // 총알이 모두 발사된 후 잠시 대기합니다.
        canFire = true; // 총알 발사가 끝났으므로 다시 발사 가능하도록 설정
    }

    IEnumerator MoveBullet(GameObject bullet)
    {
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * 10f;

        while (bullet.activeSelf)
        {
            bullet.transform.Translate(Vector2.right * Time.deltaTime * 5f);

            yield return null;

            // 총알이 빨간 상자에 닿으면
            if (redBox != null && redBox.GetComponent<Collider2D>().bounds.Contains(bullet.transform.position))
            {
                bullet.SetActive(false); // 총알 비활성화
                bulletQueue.Enqueue(bullet); // 총알을 다시 Queue에 추가
            }

        }
    }
}



