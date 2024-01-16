using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCrystalManager : MonoBehaviour
{
    public static int count = 5;
    private int countMax = 9;

    public int getCount()
    {
        return count;
    }

    public int getCountMax()
    {
        return countMax;
    }

    public void setCount(int n)
    {
        if (n > 9 || n < 0) return;
        count = n;
    }
}
