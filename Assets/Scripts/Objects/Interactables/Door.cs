using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Right Axis Door - TargetRotationY: -80, OriginYRotation: 0
/// Left Axis Door - TargetRotationY: -100, OriginYRotation: 180
/// </summary>
public class Door : BaseInteractiveObj
{
	[SerializeField] bool isOpened = false;
	[SerializeField] bool isLocked = false;
    [SerializeField] Animator anim;


	public override void Interact() 
	{
		if (isLocked)
		{
            Managers.Sound.Play("Sounds/Objects/try-opening-locked-door-1");
            foreach(var item in Managers.Data.InventoryData.itemDatas)
            {
                if(item.ItemType == DataDefine.EItemType.Key)
                {
                    isLocked = false;
                    break;
                }
            }
			return;
		}

		if (!isOpened) 
			Open();
		else 
			Close();
	}
    public void Open() 
	{
        anim.SetBool("IsOpen", true);
    }

	public void Close() 
	{
        anim.SetBool("IsOpen", false);
	}
}
