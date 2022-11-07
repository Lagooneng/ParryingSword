using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMaker : MonoBehaviour
{
    public int height = 10;
    public int width = 6;
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
    }
}
