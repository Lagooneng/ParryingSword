using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public bool dontDestroyOnLoad = true;

    void Start()
    {
        if( dontDestroyOnLoad )
        {
            DontDestroyOnLoad(this);
        }
    }
}
