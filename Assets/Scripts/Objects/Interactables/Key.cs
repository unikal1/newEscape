using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : BaseObtainableObj
{
    public void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Sprite sprite = Managers.Resource.Load<Sprite>("Sprites/Key");
        if (sprite == null)
            ItemData = new BaseItemData(DataDefine.EItemType.Key, null);
        else
            ItemData = new BaseItemData(DataDefine.EItemType.Key, sprite);
    }

    public override void Obtain()
	{
		base.Obtain();
		Managers.Sound.Play("Sounds/Objects/key-get-1", Define.Sound.SFX);
		//PlayObtainSound();
		//OnObtain?.Invoke();
		// 오브젝트를 즉시 비활성화
		//Destroy(gameObject);
	}

}
