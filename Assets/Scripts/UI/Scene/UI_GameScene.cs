using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameScene : UI_Scene
{
    [SerializeField]
    private UI_SceneInventory ui_Inventory;
    public UI_SceneInventory UI_Inventory { get { return ui_Inventory; } set { ui_Inventory = value; } }
    [SerializeField]
    private Image crossHair;
    public Image CrossHair { get { return crossHair; } set { crossHair = value; } }
    public override void Init()
    {
        base.Init();
        if(UI_Inventory == null)
            UI_Inventory = Util.FindChild<UI_SceneInventory>(gameObject);
        if(CrossHair == null)
            CrossHair = Util.FindChild<Image>(gameObject, "CrossHair");
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
