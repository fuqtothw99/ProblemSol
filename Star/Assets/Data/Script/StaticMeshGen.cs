using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StaticMeshGen))]

public class StaticMeshGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StaticMeshGen script = (StaticMeshGen)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.GenerateMesh();
        }
    }
}

public class StaticMeshGen : MonoBehaviour
{
    // Start is called before the first frame update
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        // �������� �����մϴ�.
        Vector3[] vertices = new Vector3[]
        {
            // ������
            new Vector3(0.0f, 0.0f, 0.0f),     // ������ 0 (�� �Ʒ� �߾�)
            new Vector3(-0.7f, 0.5f, 0.0f),   // ������ 1 (���� �ϴ�)
            new Vector3(-0.5f, 1.2f, 0.0f),  // ������ 2 (���� �߾� ��)
            new Vector3(0.5f, 1.2f, 0.0f), // ������ 3 (������ �߾� ��)
            new Vector3(0.7f, 0.5f, 0.0f),  // ������ 4 (������ �ϴ�)
            // �ﰢ��
            new Vector3(-1.0f, -0.5f, 0.0f),     // ������ 5 (���� �ϴ�)
            new Vector3(-1.5f, 1.2f, 0.0f),    // ������ 6 (���� ���)
            new Vector3(0.0f, 1.9f, 0.0f),   // ������ 7 (�� �� �߾�)
            new Vector3(1.5f, 1.2f, 0.0f),  // ������ 8 (������ ���)
            new Vector3(1.0f, -0.5f, 0.0f),    // ������ 9 (������ �ϴ�)
            // �簢��(���)1
            new Vector3(0.0f, 0.0f, 1.0f),    // ������ 10 (�簢�� ���� ���� �Ʒ�)
            new Vector3(-1.0f, -0.5f, 1.0f),    // ������ 11 (�簢�� ���� ������ �Ʒ�)
            // �簢��(���)2
            new Vector3(-0.5f, 0.5f, 1.0f),    // ������ 12
            new Vector3(-1.0f, -0.5f, 1.0f),    // ������ 13(11)
            // �簢��(���)3
            new Vector3(-0.5f, 0.5f, 1.0f),    // ������ 14(12)
            new Vector3(-1.5f, 1.2f, 1.0f),    // ������ 15
            // �簢��(���)4 
            new Vector3(-0.5f, 1.2f, 1.0f),    // ������ 16
            new Vector3(-1.5f, 1.2f, 1.0f),    // ������ 17(15)
            // �簢��(���)5 
            new Vector3(0.0f, 1.9f, 1.0f),    // ������ 18
            new Vector3(-0.5f, 1.2f, 1.0f),    // ������ 19(16)
            // �簢��(���)6
            new Vector3(0.5f, 1.2f, 1.0f),    // ������ 20
            new Vector3(0.0f, 1.9f, 1.0f),    // ������ 21(18)
            // �簢��(���)7
            new Vector3(1.5f, 1.2f, 1.0f),    // ������ 22
            new Vector3(0.5f, 1.2f, 1.0f),    // ������ 23(20)
            // �簢��(���)8
            new Vector3(0.7f, 0.5f, 1.0f),    // ������ 24
            new Vector3(1.5f, 1.2f, 1.0f),    // ������ 25(22)
            // �簢��(���)9
            new Vector3(1.0f, -0.5f, 1.0f),    // ������ 26
            new Vector3(0.7f, 0.5f, 1.0f),    // ������ 27(24)
            // �簢��(���)10
            new Vector3(1.0f, -0.5f, 1.0f),    // ������ 28(26)
            new Vector3(0.0f, 0.0f, 1.0f),    // ������ 29(0)

            //�ٴڸ�
            // ������
            new Vector3(0.0f, 0.0f, 1.0f),     // ������ 30 (�ٴڸ� �߾�)
            new Vector3(-0.7f, 0.5f, 1.0f),   // ������ 31 (�ٴڸ� ���� �ϴ�)
            new Vector3(-0.5f, 1.2f, 1.0f),  // ������ 32 (�ٴڸ� ���� �߾� ��)
            new Vector3(0.5f, 1.2f, 1.0f), // ������ 33 (�ٴڸ� ������ �߾� ��)
            new Vector3(0.7f, 0.5f, 1.0f),  // ������ 34 (�ٴڸ� ������ �ϴ�)
            // �ﰢ��
            new Vector3(-1.0f, -0.5f, 1.0f),     // ������ 35 (���� �ϴ�)
            new Vector3(-1.5f, 1.2f, 1.0f),    // ������ 36 (���� ���)
            new Vector3(0.0f, 1.9f, 1.0f),   // ������ 37 (�� �� �߾�)
            new Vector3(1.5f, 1.2f, 1.0f),  // ������ 38 (������ ���)
            new Vector3(1.0f, -0.5f, 1.0f),    // ������ 39 (������ �ϴ�)
        };

        // �ﰢ�� �ε��� �迭�� �����մϴ�.
        int[] triangleIndices = new int[]
        {
            // ������
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            // �ﰢ��
            0, 5, 1,
            1, 6, 2,
            2, 7, 3,
            3, 8, 4,
            4, 9, 0,
            // �簢��1
            0, 10, 11,
            0, 11, 5,
            //�簢��2
            1, 12, 11,
            1, 11, 5,
            //�簢��3
            1, 12, 15,
            1, 15, 6,
            //�簢��4
            2, 16, 15,
            2, 15, 6,
            //�簢��5
            7, 18, 16,
            7, 16, 2,
            //�簢��6
            3, 20, 18,
            3, 18, 7,
            //�簢��7
            8, 22, 20,
            8, 20, 3,
            //�簢��8
            8, 22, 24,
            8, 24, 4,
            //�簢��9
            9, 26, 24,
            9, 24, 4,
            //�簢��10
            9, 26, 10,
            9, 10, 0,
            // ������2
            30, 31, 32,
            30, 32, 33,
            30, 33, 34,
            // �ﰢ��2
            30, 35, 31,
            31, 36, 32,
            32, 37, 33,
            33, 38, 34,
            34, 39, 30,
        };

        mesh.vertices = vertices;
        mesh.triangles = triangleIndices;

        // ������ ����մϴ�.
        mesh.RecalculateNormals();

        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        if (mf == null)
            mf = gameObject.AddComponent<MeshFilter>();

        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        if (mr == null)
            mr = gameObject.AddComponent<MeshRenderer>();

        mf.mesh = mesh;

        // ī�� ���̵��� �����մϴ�.
        mr.sharedMaterial.shader = Shader.Find("YellowShader");
    }
}
