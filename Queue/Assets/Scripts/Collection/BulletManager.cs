/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Queue<GameObject> bulletQueue = new Queue<GameObject>(); // 총알을 저장하는 큐
    public int ballCount = 1; // 총알 개수
    private bool is_start = false; // 게임 시작 여부
    public GameObject ballPrefab; // 총알 프리팹
    public Transform target; // 목표 지점
    private int shotCount = 10; // 발사 횟수
    private bool isShooting = false; // 발사 중인지 여부

    void Start()
    {
        for (int i = 0; i < 10; i++) // 초기에 10개의 총알을 생성하여 큐에 추가
        {
            Enqueue(CreateBullet());
        }
    }

    void Update()
    {
        if (is_start && bulletQueue.Count () > 0 && shotCount > 0) // 게임이 시작되었고 총알이 있으며 발사 횟수가 남아 있을 때
        {
            if (!isShooting) // 발사 중이 아닐 때
            {
                GameObject bullet = Dequeue(); // 큐에서 총알 가져오기
                bullet.transform.position = transform.position; // 총알 위치 설정
                StartCoroutine(MoveBullet(bullet)); // 총알 이동 코루틴 시작
                shotCount--; // 발사 횟수 감소
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.is_start = true; // 마우스 클릭 시 발사 시작
        }
    }

    // 총알 생성
    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(ballPrefab); // 프리팹을 이용하여 총알 생성
        bullet.SetActive(false); // 비활성화 상태로 설정
        return bullet;
    }

    // 큐에 총알 추가
    private void Enqueue(GameObject bullet)
    {
        bulletQueue.Enqueue(bullet);
    }

    // 큐에서 총알 제거 후 반환
    private GameObject Dequeue()
    {
        return bulletQueue.Dequeue();
    }

    // 총알 이동 코루틴
    IEnumerator MoveBullet(GameObject bullet)
    {
        isShooting = true; // 발사 중으로 설정
        bullet.SetActive(true); // 총알 활성화

        while (Vector3.Distance(bullet.transform.position, target.position) > 0.1f) // 목표 지점에 도달할 때까지 반복
        {
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.right * 10f;
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target.position, Time.deltaTime * 8.0f); // 총알을 목표 지점(빨간색 상자)으로 이동
            yield return null;
        }

        bullet.SetActive(false); // 목표지점에 닿으면 총알 비활성화
        Enqueue(bullet); // 총알을 다시 큐에 추가
        isShooting = false; // 발사 종료
    }
}
*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform spawnPoint; // 총알 생성 위치
    public GameObject redBox; // 빨간 상자
    private Queue<GameObject> bulletQueue = new Queue<GameObject>(); // 총알을 담을 Queue

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

            // 총알이 빨간 상자에 닿으면
            if (redBox != null && redBox.GetComponent<Collider2D>().bounds.Contains(bullet.transform.position))
            {
                bullet.SetActive(false); // 총알 비활성화
                bulletQueue.Enqueue(bullet); // 총알을 다시 Queue에 추가
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



