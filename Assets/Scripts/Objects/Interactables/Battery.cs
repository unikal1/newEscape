using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : BaseObtainableObj
{
    public override void Init()
    {
        base.Init(); 
        Sprite sprite = Managers.Resource.Load<Sprite>("Sprites/Battery");
        if (sprite == null)
            ItemData = new BaseItemData(DataDefine.EItemType.Battery, null);
        else
            ItemData = new BaseItemData(DataDefine.EItemType.Battery, sprite);
    }

    public override void Obtain()
    {
        base.Obtain();
        Managers.Sound.Play("Assets/Resources/Sounds/Objects/paper-collect-1", Define.Sound.SFX);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}
