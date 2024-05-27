using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public override void Clear()
    {
        
    }
    protected override void Init()
    {
        base.Init();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneType = Define.Scene.MainScene;
        Managers.UI.ShowPopUpUI<UI_MainScene>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
