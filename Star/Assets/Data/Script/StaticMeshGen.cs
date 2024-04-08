/*
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StaticMeshGen))]
public class StaticMeshGenEditor : Editor
{
    //��ư����� ����
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

//�޽������ ����
public class StaticMeshGen : MonoBehaviour
{
    // Start is called before the first frame update
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            new Vector3 (0.0f, 0.0f, 0.0f),
            new Vector3 (1.0f, 0.0f, 0.0f),
            new Vector3 (1.0f, 1.0f, 0.0f),
        };

        mesh.vertices = vertices;

        int[] triangleIndices = new int[]
        {
            0,2,1,
        };

        mesh.triangles = triangleIndices;

        MeshFilter mf = this.AddComponent<MeshFilter>();
        MeshRenderer mr = this.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StaticMeshGen))]
public class StaticMeshGenEditor : Editor
{
    //��ư����� ����
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

//�޽������ ����
public class StaticMeshGen : MonoBehaviour
{
    // Start is called before the first frame update
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        // ������� ����� ���� ������ �迭
        Vector3[] vertices = new Vector3[]
        {
            // �Ʒ����� �� ��� ������
            new Vector3 (0.0f, 0.0f, 0.0f), // 0
            new Vector3 (0.0f, 1.0f, 0.0f), // 1
            new Vector3 (0.5f, 0.5f, 0.0f), // 2
            new Vector3 (1.0f, 1.0f, 0.0f), // 3
            new Vector3 (0.5f, 0.0f, 0.0f), // 4
            new Vector3 (1.0f, -1.0f, 0.0f), // 5
            new Vector3 (0.0f, 0.0f, 0.0f), // 6
            new Vector3 (-1.0f, -1.0f, 0.0f), // 7
            new Vector3 (-0.5f, 0.0f, 0.0f), // 8
            new Vector3 (-1.0f, 1.0f, 0.0f), // 9
            new Vector3 (-0.5f, 0.5f, 0.0f), // 10
        };

        mesh.vertices = vertices;

        // ������� ����� ���� �ﰢ�� �ε��� �迭
        int[] triangleIndices = new int[]
        {
            // �Ʒ��� �ﰢ��
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 5,
            0, 5, 6,
            0, 6, 7,
            0, 7, 8,
            0, 8, 9,
            0, 9, 10,
            0, 10, 1,
        };

        mesh.triangles = triangleIndices;

        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        if (mf == null)
            mf = gameObject.AddComponent<MeshFilter>();

        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        if (mr == null)
            mr = gameObject.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
