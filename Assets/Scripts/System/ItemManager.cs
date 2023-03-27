using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemList
{
    ITEM_FUROFBLANC,
    ITEM_WINGSOFHORENA,
    ITEM_STINGOFHORISE,
    ITEM_HORNOFOCCI
}

public class ItemManager : MonoBehaviour
{
    private Dictionary<ItemList, int> ownItems;

    private void Awake()
    {
        ownItems = new Dictionary<ItemList, int>();

        ownItems.Add(ItemList.ITEM_FUROFBLANC, 0);
        ownItems.Add(ItemList.ITEM_WINGSOFHORENA, 0);
        ownItems.Add(ItemList.ITEM_STINGOFHORISE, 0);
        ownItems.Add(ItemList.ITEM_HORNOFOCCI, 0);
    }

    public void renewItem( ItemList item, int i )
    {
        ownItems[item] = i;
        Debug.Log(i);
    }

    public int countItem( ItemList item )
    {
        return ownItems[item];
    }
}
