using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataDefine;

public class BaseItemData
{
    [SerializeField]
    private EItemType itemType;
    public EItemType ItemType { get { return itemType; } set { itemType = value; } }

    [SerializeField]
    private string itemSprite;
    public string ItemSprite { get { return itemSprite; } set { itemSprite = value; } }
        
    public BaseItemData()
    {
        ItemType = EItemType.Default;
        ItemSprite = null;
    }
    public BaseItemData(EItemType itemType, string itemSprite)
    {
        ItemType = itemType;
        ItemSprite = itemSprite;
    }
}
