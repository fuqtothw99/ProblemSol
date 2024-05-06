using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject tilePrefab; // �� Ÿ�� ������
    public Vector2Int mapSize; // ���� ���μ��� ũ��
    public float tileSize = 1f; // Ÿ���� ũ��

    // CSV ���� ���
    public string csvFilePath;

    void Start()
    {
        // Plane�� ũ�⸦ �������� ���� ũ�� ���
        Vector3 planeScale = transform.localScale;
        mapSize.x = Mathf.RoundToInt(planeScale.x / tileSize);
        mapSize.y = Mathf.RoundToInt(planeScale.z / tileSize);

        // CSV ���Ͽ��� �� ����
        CreateMapFromCSV();
    }

    // CSV ������ �а� ���� �����ϴ� �Լ�
    void CreateMapFromCSV()
    {
        // CSV ���� �б�
        string[] lines = System.IO.File.ReadAllLines(csvFilePath);

        for (int y = 0; y < mapSize.y; y++)
        {
            string line = lines[y];
            string[] values = line.Split(',');

            for (int x = 0; x < mapSize.x; x++)
            {
                int tileType = int.Parse(values[x]);

                // Ÿ���� ��ġ ���
                Vector3 tilePosition = new Vector3(x * tileSize, 0, y * tileSize);

                // Ÿ�� ����
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);

                // Ÿ���� ũ�� ����
                tile.transform.localScale = new Vector3(tileSize, tileSize, tileSize);

                // Ÿ�� ������ ���� ������ ���� �Ǵ� ��� ����
                switch (tileType)
                {
                    case 0: // ����
                        Destroy(tile); // Ÿ�� ����
                        break;
                    case 1: // ��(����)
                        // ������ ����
                        break;
                    case 2: // ��(����)
                        // ���� ������ ����
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
