using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameScene : UI_Scene
{
    [SerializeField]
    private UI_SceneInventory ui_Inventory;
    public UI_SceneInventory UI_Inventory { get { return ui_Inventory; } set { ui_Inventory = value; } }
    public override void Init()
    {
        base.Init();
        UI_Inventory = Util.FindChild<UI_SceneInventory>(gameObject);
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
