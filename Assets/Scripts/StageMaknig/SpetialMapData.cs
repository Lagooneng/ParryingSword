using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpetialMapData : MapData
{
    public SpetialMapData(string s)
    {
        prev = s;
        now = s;
        type = Type.SPECIAL;
    }

    public override void setPrev(string prev)
    {
        // 내용을 바꿀 수 없도록 변경
    }

    public override void setNow(string now)
    {
        // 내용을 바꿀 수 없도록 변경
    }

    public override void setType(Type type)
    {
        // 내용을 바꿀 수 없도록 변경
    }
}
