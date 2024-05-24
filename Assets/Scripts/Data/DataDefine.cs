using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataDefine
{
    [System.Serializable]
    public enum EItemType
    {
        Paper0,
        Paper1,
        Light,
    }

    [System.Serializable]
    public class Inventory
    {
        public List<BaseItemData> itemDatas;
        public Inventory()
        {
            itemDatas = null;
        }
    }
}
