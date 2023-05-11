using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MagicCrystalController : MonoBehaviour
{
    public GameObject[] crystals;
    private MagicCrystalManager magicCrystalM;

    private Transform[] boxList;

    private void Awake()
    {
        magicCrystalM =
            GameObject.FindGameObjectWithTag("Player").GetComponent<MagicCrystalManager>();

        boxList = new Transform[3];

        boxList[0] = transform.Find("CrystalBox0");
        boxList[1] = transform.Find("CrystalBox1");
        boxList[2] = transform.Find("CrystalBox2");
    }

    private void Start()
    {
        for( int i = 0; i < 3; i++ )
        {
            switch (magicCrystalM.getMagicCrystal(i))
            {
                case MagicCrystalList.ACTIONDJUMP:
                    Instantiate(crystals[0], boxList[i]);
                    break;
                case MagicCrystalList.ACTIONHEAL:
                    Instantiate(crystals[1], boxList[i]);
                    break;
                case MagicCrystalList.ACTIONWIRE:
                    Instantiate(crystals[2], boxList[i]);
                    break;
                case MagicCrystalList.UPATTACK:
                    Instantiate(crystals[3], boxList[i]);
                    break;
                case MagicCrystalList.UPDEFENSE:
                    Instantiate(crystals[4], boxList[i]);
                    break;
                case MagicCrystalList.UPHP:
                    Instantiate(crystals[5], boxList[i]);
                    break;
                case MagicCrystalList.INACTIVE:
                    boxList[i].GetComponent<SpriteRenderer>().enabled = false;
                    break;
                default:
                    break;
            }
        }
    }
}