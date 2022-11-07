using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPiece : MonoBehaviour
{
    public GameObject[] mapPieceList_L;
    public GameObject[] mapPieceList_R;
    public GameObject[] mapPieceList_D;
    public GameObject[] mapPieceList_LR;
    public GameObject[] mapPieceList_LD;
    public GameObject[] mapPieceList_RD;
    public GameObject[] mapPieceList_LRD;
    public GameObject mapPiece_F;
    public GameObject mapPiece_Start, mapPiece_End;
    private bool listChecked;

    public void listChecking()
    {
        if( mapPieceList_L.Length != 0 &&
            mapPieceList_R.Length != 0 &&
            mapPieceList_D.Length != 0 &&
            mapPieceList_LR.Length != 0 &&
            mapPieceList_LD.Length != 0 &&
            mapPieceList_RD.Length != 0 &&
            mapPieceList_LRD.Length != 0 &&
            mapPiece_F != null )
        {
            listChecked = true;
        }
        else
        {
            Debug.Log("The one of the list is null");
        }
    }

    public GameObject getMapPiece(MapData mapData)
    {
        if( !listChecked )
        {
            Debug.Log("You should check the list");
            return null;
        }

        Type type = mapData.getType();

        switch( type )
        {
            case Type.L:
                return mapPieceList_L[Random.Range(0, mapPieceList_L.Length)];
            case Type.R:
                return mapPieceList_R[Random.Range(0, mapPieceList_R.Length)];
            case Type.D:
                return mapPieceList_D[Random.Range(0, mapPieceList_D.Length)];
            case Type.LR:
                return mapPieceList_LR[Random.Range(0, mapPieceList_LR.Length)];
            case Type.LD:
                return mapPieceList_LD[Random.Range(0, mapPieceList_LD.Length)];
            case Type.RD:
                return mapPieceList_RD[Random.Range(0, mapPieceList_RD.Length)];
            case Type.LRD:
                return mapPieceList_LRD[Random.Range(0, mapPieceList_LRD.Length)];
            case Type.F:
                return mapPiece_F;
            default:
                return getSpecialPiece(mapData.getNow());
        }
    }

    private GameObject getSpecialPiece(string name)
    {
        switch( name )
        {
            case "Start":
                return mapPiece_Start;
            case "End":
                return mapPiece_End;
            default:
                return null;
        }
    }
}
