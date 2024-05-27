using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataDefine
{
    [System.Serializable]
    public enum EItemType
    {
        Default,
        Paper,
        Map,
        Light,
        Key,
        Battery,
		Flashlight,
    }

    [System.Serializable]
    public class Inventory
    {
        public List<BaseItemData> itemDatas;
        public Inventory()
        {
            itemDatas = new List<BaseItemData>();
        }
    }
}
