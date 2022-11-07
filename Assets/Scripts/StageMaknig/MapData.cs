using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    NON,
    SPECIAL,
    L,
    R,
    D,
    LR,
    LD,
    RD,
    LRD,
    V,
    F,
}

public class MapData
{
    protected string prev;
    protected string now;
    protected Type type;

    public MapData()
    {
        prev = "non";
        now = "non";
        type = Type.NON;
    }

    public string getPrev()
    {
        return prev;
    }

    public virtual void setPrev(string prev)
    {
        this.prev = prev;
    }

    public string getNow()
    {
        return now;
    }

    public virtual void setNow(string now)
    {
        this.now = now;
    }

    public Type getType()
    {
        return type;
    }

    public virtual void setType(Type type)
    {
        this.type = type;
    }
}
