using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagicCrystalList
{
    ACTIONDJUMP,
    ACTIONHEAL,
    ACTIONWIRE,
    UPATTACK,
    UPDEFENSE,
    UPHP,
    INACTIVE,
    NON
}

public class MagicCrystalManager : MonoBehaviour
{
    private MagicCrystalList[] crystalSlots;

    private void Awake()
    {
        crystalSlots = new MagicCrystalList[3];

        crystalSlots[0] = MagicCrystalList.ACTIONWIRE;
        crystalSlots[1] = MagicCrystalList.ACTIONDJUMP;
        crystalSlots[2] = MagicCrystalList.INACTIVE;
    }

    public MagicCrystalList getMagicCrystal(int i)
    {
        return crystalSlots[i];
    }

    public void setMagicCrystal(int i, MagicCrystalList crystal)
    {
        crystalSlots[i] = crystal;
    }

    public bool haveMagicCrystal(MagicCrystalList crystal)
    {
        if( (crystalSlots[0] == crystal ) || (crystalSlots[1] == crystal) || (crystalSlots[2] == crystal))
        {
            return true;
        }

        return false;
    }
}
