using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMaker : MonoBehaviour
{
    public int height = 10;
    public int width = 6;
    public float scale = 1;

    private int mapPieceHegiht = 18, mapPieceWidth = 40;

    // 오브젝트에 붙여서 스테이지를 생성
    private void Awake()
    {
        BluePrint bluePrint = new BluePrint(height, width);
        MapData[,] mapData = bluePrint.getBluePrint();
        MapPiece mapPiece = GetComponent<MapPiece>();
        mapPiece.listChecking();
        Vector3 pos;
        GameObject go;

        for ( int i = 0; i < height; i++ )
        {
            for( int j = 0; j < width; j++ )
            {
                if(mapData[i, j].getType() == Type.V )
                {
                    continue;
                }

                pos = new Vector3(j * mapPieceWidth,
                                                mapPieceHegiht * height - i * mapPieceHegiht, 0);
                go = Instantiate(mapPiece.getMapPiece(mapData[i, j]),
                                pos, Quaternion.identity);
                go.transform.SetParent(gameObject.transform);
            }
        }

        // 가장자리 벽치기
        // 세로 벽
        for( int i = -1; i < height + 3; i++ )
        {
            go = Instantiate(mapPiece.mapWall,
                                new Vector3(-mapPieceWidth, mapPieceHegiht * i, 0), Quaternion.identity);
            go.transform.SetParent(gameObject.transform);

            go = Instantiate(mapPiece.mapWall,
                                new Vector3(-mapPieceWidth * 2, mapPieceHegiht * i, 0), Quaternion.identity);
            go.transform.SetParent(gameObject.transform);

            go = Instantiate(mapPiece.mapWall,
                                new Vector3(mapPieceWidth * width, mapPieceHegiht * i, 0), Quaternion.identity);
            go.transform.SetParent(gameObject.transform);

            go = Instantiate(mapPiece.mapWall,
                                new Vector3(mapPieceWidth * (width + 1), mapPieceHegiht * i, 0), Quaternion.identity);
            go.transform.SetParent(gameObject.transform);
        }

        // 가로 벽
        for( int i = 0; i < width; i ++ )
        {
            go = Instantiate(mapPiece.mapWall,
                                new Vector3(mapPieceWidth * i, 0, 0), Quaternion.identity);
            go.transform.SetParent(gameObject.transform);

            go = Instantiate(mapPiece.mapWall,
                                new Vector3(mapPieceWidth * i, -mapPieceHegiht , 0), Quaternion.identity);
            go.transform.SetParent(gameObject.transform);

            go = Instantiate(mapPiece.mapWall,
                                new Vector3(mapPieceWidth * i, mapPieceHegiht * (height + 1), 0), Quaternion.identity);
            go.transform.SetParent(gameObject.transform);

            go = Instantiate(mapPiece.mapWall,
                                new Vector3(mapPieceWidth * i, mapPieceHegiht * (height + 2), 0), Quaternion.identity);
            go.transform.SetParent(gameObject.transform);
        }

        transform.localScale = new Vector3(transform.localScale.x * scale,
                                           transform.localScale.y * scale,
                                           transform.localScale.z * scale);
    }
}
