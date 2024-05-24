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
            itemDatas = new List<BaseItemData>();
            for (int i = 0; i < 4; i++)
            {
                itemDatas.Add(new BaseItemData());
            }
        }
    }
}
