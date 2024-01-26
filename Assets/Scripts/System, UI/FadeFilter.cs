using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FadeState
{
    FADEIN,     // 나타남
    FADEOUT,    // 흐려짐
    NON         // 작동 안함
}

public class FadeFilter : MonoBehaviour
{
    // 페이드 필터에 붙여서 사용
    SpriteRenderer sprite;
    FadeState fadeState;
    float colorChangeSpeed;

    private void Awake()
    {
        fadeState = FadeState.FADEIN;
        colorChangeSpeed = 0.01f;
    }

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        switch( fadeState )
        {
            case FadeState.FADEIN:
                if( sprite.color.a - colorChangeSpeed < 0.0f )
                {
                    fadeState = FadeState.NON;
                    break;
                }

                sprite.color = new Color(sprite.color.r, sprite.color.g,
                                            sprite.color.b, sprite.color.a - colorChangeSpeed);
                break;

            case FadeState.FADEOUT:
                if( sprite.color.a + colorChangeSpeed > 255.0f )
                {
                    fadeState = FadeState.NON;
                    break;
                }

                sprite.color = new Color(sprite.color.r, sprite.color.g,
                                            sprite.color.b, sprite.color.a + colorChangeSpeed);

                break;
        }
    }

    public void fadeIn()
    {
        fadeState = FadeState.FADEIN;
    }

    public void fadeOut()
    {
        fadeState = FadeState.FADEOUT;
    }
}
