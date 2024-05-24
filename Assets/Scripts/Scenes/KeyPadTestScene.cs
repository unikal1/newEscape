using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadTestScene : BaseScene
{
    public override void Clear()
    {
        
    }

    protected override void Init()
    {
        base.Init();
        SceneUI = Managers.UI.ShowSceneUI<UI_GameScene>();
    }
}
