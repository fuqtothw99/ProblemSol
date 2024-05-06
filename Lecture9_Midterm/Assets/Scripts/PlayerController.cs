using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // �÷��̾� �̵� �ӵ�
    public float rotateSpeed = 90f; // ī�޶� ȸ�� �ӵ� (90��/��)

    private Camera mainCamera;
    private bool isRotating; // ȸ�� ������ ����

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Ű���� �Է� ����
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // �̵� ���� ���
        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // ȸ�� ���� ���
        float rotateAmount = 0f;
        if (Input.GetKeyDown(KeyCode.O) && !isRotating)
        {
            rotateAmount = -90f; // �ݽð���� ȸ��
            StartCoroutine(RotateCamera(rotateAmount));
        }
        else if (Input.GetKeyDown(KeyCode.P) && !isRotating)
        {
            rotateAmount = 90f; // �ð���� ȸ��
            StartCoroutine(RotateCamera(rotateAmount));
        }

        // �̵� ������ ī�޶��� ���� ��ǥ��� ��ȯ
        moveDirection = mainCamera.transform.TransformDirection(moveDirection);
        moveDirection.y = 0f; // y �� �̵� ���� (�÷��̾ ���Ϸ� �������� �ʵ���)

        // �÷��̾� �̵�
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    IEnumerator RotateCamera(float rotateAmount)
    {
        isRotating = true; // ȸ�� ������ ǥ��

        Quaternion currentRotation = mainCamera.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles + Vector3.forward * rotateAmount); // Z �� ȸ�� �߰�

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * (rotateSpeed / 90f); // ȸ�� �ӵ��� ���� ����

            mainCamera.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, t);
            yield return null;
        }

        isRotating = false; // ȸ�� ����
    }

}
