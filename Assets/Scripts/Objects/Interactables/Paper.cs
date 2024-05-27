using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : BaseObtainableObj
{ 
	void Start()
	{
		Init();
	}
	
	public override void Init()
	{
		base.Init();
        Sprite sprite = Managers.Resource.Load<Sprite>("Sprites/Paper");
        ItemData = new BaseItemData(DataDefine.EItemType.Paper, sprite);
    }

	public override void Obtain()
	{
		base.Obtain();
		Managers.Sound.Play("Sounds/Objects/paper-collect-1", Define.Sound.SFX);
	}
	
}
