using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsters;
    private int num;

    private void Awake()
    {
        num = Random.Range(0, monsters.Length);
        GameObject go = Instantiate(monsters[num], this.transform);
    }
}
