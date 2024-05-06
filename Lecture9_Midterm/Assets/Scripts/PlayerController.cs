using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도
    public float rotateSpeed = 90f; // 카메라 회전 속도 (90도/초)

    private Camera mainCamera;
    private bool isRotating; // 회전 중인지 여부

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // 키보드 입력 감지
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 이동 방향 계산
        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // 회전 방향 계산
        float rotateAmount = 0f;
        if (Input.GetKeyDown(KeyCode.O) && !isRotating)
        {
            rotateAmount = -90f; // 반시계방향 회전
            StartCoroutine(RotateCamera(rotateAmount));
        }
        else if (Input.GetKeyDown(KeyCode.P) && !isRotating)
        {
            rotateAmount = 90f; // 시계방향 회전
            StartCoroutine(RotateCamera(rotateAmount));
        }

        // 이동 방향을 카메라의 로컬 좌표계로 변환
        moveDirection = mainCamera.transform.TransformDirection(moveDirection);
        moveDirection.y = 0f; // y 축 이동 제거 (플레이어가 상하로 움직이지 않도록)

        // 플레이어 이동
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    IEnumerator RotateCamera(float rotateAmount)
    {
        isRotating = true; // 회전 중임을 표시

        Quaternion currentRotation = mainCamera.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles + Vector3.forward * rotateAmount); // Z 축 회전 추가

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * (rotateSpeed / 90f); // 회전 속도에 따른 보간

            mainCamera.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, t);
            yield return null;
        }

        isRotating = false; // 회전 종료
    }

}
