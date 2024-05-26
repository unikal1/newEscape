using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DataDefine;

public class DataManager
{
    [SerializeField]
    private Inventory inventorydata;
    public Inventory InventoryData { get { return inventorydata; }  set { inventorydata = value; } }

    [SerializeField]
    private int selectedSlotIndex;
    public int SelectedSlotIndex { get { return selectedSlotIndex; } set { selectedSlotIndex = value; } }

    // Start is called before the first frame update
    public void Init()
    {
        if(InventoryData == null)
            InventoryData = JsonManager.Load<Inventory>();
    }
}
