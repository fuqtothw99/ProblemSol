using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject tilePrefab; // 맵 타일 프리팹
    public Vector2Int mapSize; // 맵의 가로세로 크기
    public float tileSize = 1f; // 타일의 크기

    // CSV 파일 경로
    public string csvFilePath;

    void Start()
    {
        // Plane의 크기를 기준으로 맵의 크기 계산
        Vector3 planeScale = transform.localScale;
        mapSize.x = Mathf.RoundToInt(planeScale.x / tileSize);
        mapSize.y = Mathf.RoundToInt(planeScale.z / tileSize);

        // CSV 파일에서 맵 생성
        CreateMapFromCSV();
    }

    // CSV 파일을 읽고 맵을 생성하는 함수
    void CreateMapFromCSV()
    {
        // CSV 파일 읽기
        string[] lines = System.IO.File.ReadAllLines(csvFilePath);

        for (int y = 0; y < mapSize.y; y++)
        {
            string line = lines[y];
            string[] values = line.Split(',');

            for (int x = 0; x < mapSize.x; x++)
            {
                int tileType = int.Parse(values[x]);

                // 타일의 위치 계산
                Vector3 tilePosition = new Vector3(x * tileSize, 0, y * tileSize);

                // 타일 생성
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);

                // 타일의 크기 설정
                tile.transform.localScale = new Vector3(tileSize, tileSize, tileSize);

                // 타일 종류에 따라 적절한 색상 또는 모양 적용
                switch (tileType)
                {
                    case 0: // 없음
                        Destroy(tile); // 타일 삭제
                        break;
                    case 1: // 벽(낮음)
                        // 벽으로 설정
                        break;
                    case 2: // 벽(높음)
                        // 높은 벽으로 설정
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
