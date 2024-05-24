using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataDefine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData", order = int.MaxValue)]
public class BaseItemData : ScriptableObject
{
    [SerializeField]
    private EItemType itemType;
    public EItemType ItemType { get { return itemType; } set { itemType = value; } }

    [SerializeField]
    private Sprite itemSprite;
    public Sprite ItemSprite { get { return itemSprite; } set { itemSprite = value; } }

}
