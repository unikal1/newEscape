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
    private Sprite itemSprite;
    public Sprite ItemSprite { get { return itemSprite; } set { itemSprite = value; } }
        
    public BaseItemData()
    {
        ItemType = EItemType.Default;
        ItemSprite = null;
    }
    public BaseItemData(EItemType itemType, Sprite itemSprite)
    {
        ItemType = itemType;
        ItemSprite = itemSprite;
    }
}
