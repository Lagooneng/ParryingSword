using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitImpact : MonoBehaviour
{
    // Collider_Attack에 붙여서 사용
    private float stiffnessTime = 0.1f;
    private float timeScale = 0.3f;
    private GameObject effect;
    public Animator hitAnim;

    private void Awake()
    {
        effect = GameObject.Find("Effect_Hit");
        hitAnim = effect.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == "EnemyBody" )
        {
            // 시각 효과
            effect.transform.position =
                new Vector3(collision.transform.position.x +  (float)Random.Range(-5, 6) / 10,
                            collision.transform.position.y + (float)Random.Range(-5, 6) / 10,
                            collision.transform.position.z);
            effect.transform.rotation =
                Quaternion.Euler(effect.transform.rotation.x,
                                 effect.transform.rotation.y,
                                 Random.Range(-20.0f, 21.0f));

            hitAnim.SetTrigger("On");
            // Debug.Log(effect.transform.position);

            // 역경직 효과
            Time.timeScale = timeScale;
            Invoke("restoreTimeScale", stiffnessTime * timeScale);
        }
    }

    public void restoreTimeScale()
    {
        Time.timeScale = 1.0f;
    }
}
