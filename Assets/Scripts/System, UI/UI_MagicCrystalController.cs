using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MagicCrystalController : MonoBehaviour
{
    public GameObject[] crystals;
    public GameObject countObj;
    public GameObject countMaxObj;
    private MagicCrystalManager magicCrystalM;
    private TextMesh countMesh;
    private Transform box;

    private void Awake()
    {
        magicCrystalM =
            GameObject.FindGameObjectWithTag("Player").GetComponent<MagicCrystalManager>();
        box = transform.Find("CrystalBox0");
    }

    private void Start()
    {
        Instantiate(crystals[1], box);
        countMesh = countObj.GetComponent<TextMesh>();
        countMesh.text = magicCrystalM.getCount().ToString();
        countMaxObj.GetComponent<TextMesh>().text = magicCrystalM.getCountMax().ToString();
    }

    private void Update()
    {
        countMesh.text = magicCrystalM.getCount().ToString();
    }

    public void inactiveCrystal()
    {
        box.gameObject.SetActive(false);
    }
}
