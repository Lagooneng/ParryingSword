using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 맵 조각 Start에 빈 게임 오브젝터를 만들고 거기에 붙여서 플레이어 소환
public class SummonPlayer : MonoBehaviour
{
    private GameObject player;

    // Awake에하면 안됨
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position;
    }
}
