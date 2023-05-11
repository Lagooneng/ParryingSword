using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HPController : MonoBehaviour
{
    private PlayerController playerCtrl;
    private LineRenderer bar;
    private float HPMax;
    private float widthX = 20.0f;

    private void Awake()
    {
        playerCtrl =
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        bar = GetComponent<LineRenderer>();
        HPMax = playerCtrl.hpMax;
    }

    private void FixedUpdate()
    {
        float difference = dif();

        // HP 표시를 감소 시켜야 하는 경우
        if (bar.GetPosition(1).x - widthX * difference > 0.1f)
        {
            bar.SetPosition(1, new Vector3(bar.GetPosition(1).x - 1.0f, 0, 10.0f));
        }
        else
        {
            bar.SetPosition(1, new Vector3(widthX * difference, 0, 10.0f));
        }

    }

    private float dif()
    {
        return (playerCtrl.hp / HPMax);
    }
}