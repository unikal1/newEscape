using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : BaseObtainableObj
{
    public override void Init()
    {
        base.Init();
        Sprite sprite = Managers.Resource.Load<Sprite>("Sprites/Map");
        ItemData = new BaseItemData(DataDefine.EItemType.Map, sprite);
    }
    public override void Obtain()
    {
        base.Obtain();
        Managers.Sound.Play("Sounds/Objects/paper-collect-1", Define.Sound.SFX);
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
