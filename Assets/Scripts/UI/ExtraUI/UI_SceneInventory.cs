using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SceneInventory : UI_Base
{
    [SerializeField]
    private List<UI_Slot> ui_Slots;
    public List<UI_Slot> UI_Slots { get { return ui_Slots; } set { ui_Slots = value; } }

    public override void Init()
    {
        for(int i = 0; i < 4; i++)
        {
            UI_Slots.Add(Managers.UI.ShowExtraUI<UI_Slot>());
            UI_Slots[i].Index = i;
            UI_Slots[i].transform.SetParent(transform);
        }
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
