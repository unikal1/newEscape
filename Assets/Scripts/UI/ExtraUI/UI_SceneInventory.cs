using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SceneInventory : UI_Base
{
    [SerializeField]
    private List<UI_Slot> ui_Slots;
    public List<UI_Slot> UI_Slots { get { return ui_Slots; } set { ui_Slots = value; } }

    [SerializeField]
    private UI_Slot selectedSlot;
    public UI_Slot SelectedSlot { get { return selectedSlot; } set { selectedSlot = value; } }

    public override void Init()
    {
        for(int i = 0; i < 4; i++)
        {
            UI_Slots.Add(Managers.UI.ShowExtraUI<UI_Slot>());
            UI_Slots[i].Index = i;
            UI_Slots[i].transform.SetParent(transform);
            UI_Slots[i].ItemData = null;
        }
    }

    public void UpdateInventory()
    {
        for(int i = 0; i < Managers.Data.InventoryData.itemDatas.Count; i++)
        {
            UI_Slots[i].ItemData = Managers.Data.InventoryData.itemDatas[i];
            UI_Slots[i].Init();
        }
    }
    public void SelectSlot(int index)
    {
        if(Managers.Data.InventoryData == null || Managers.Data.InventoryData.itemDatas[index] == null)
            return;
        if(SelectedSlot != null)
            SelectedSlot.BackGround.color = Color.white;

        SelectedSlot = UI_Slots[index];
        SelectedSlot.BackGround.color = Color.yellow;
    }

    public void UseItem()
    {
        if (SelectedSlot == null)
            return;

        SelectedSlot.UseItem();
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
