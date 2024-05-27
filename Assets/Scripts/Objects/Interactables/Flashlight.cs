using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flashlight : BaseObtainableObj {
	[SerializeField] List<AudioClip> obtainSounds;

	public override void Init() {
		base.Init();
		Sprite sprite = Managers.Resource.Load<Sprite>("Sprites/Flashlight");
		if (sprite == null)
			ItemData = new BaseItemData(DataDefine.EItemType.Flashlight, null);
		else
			ItemData = new BaseItemData(DataDefine.EItemType.Flashlight, sprite);
	}

	private void Start() {
		Init();
	}

	public void Obtain() {
		base.Obtain();
	}

}
