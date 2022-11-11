using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    public override void Dead()
    {
        base.Dead();
        // 리로드
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Camera.main.transform.position =
            new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }
}
