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

    [SerializeField] float duration = 0.5f; // ���ݴµ� �ɸ��� �ð�
    [SerializeField] float targetRotationY = -80f;
    [SerializeField] float originYRotation = 0f;

    float originXRot;
    float originZRot;

    Coroutine openCoroutine = null;
    Coroutine closeCoroutine = null;

    void Start()
    {
        originXRot = transform.localEulerAngles.x;
        originZRot = transform.localEulerAngles.z;
    }

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
		if(anim != null)
			anim.SetBool("IsOpen", true);
        else
        {
            if (closeCoroutine != null)
                StopCoroutine(closeCoroutine);

            openCoroutine = StartCoroutine(OpenCoroutine());
        }
    }

	public void Close() 
	{
		if(anim != null)
			anim.SetBool("IsOpen", false);
        else
        {
            if (openCoroutine != null)
                StopCoroutine(openCoroutine);

            closeCoroutine = StartCoroutine(CloseCoroutine());
        }
	}

    public IEnumerator OpenCoroutine()
    {
        Managers.Sound.Play("Sounds/Objects/metal-door-open-1");
        float startRotationY = transform.localEulerAngles.y;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float smoothStep = Mathf.SmoothStep(0f, 1f, t);
            float currentRotationY = Mathf.LerpAngle(startRotationY, targetRotationY, smoothStep);
            transform.localRotation = Quaternion.Euler(originXRot, currentRotationY, originZRot);

            yield return null;
        }
        // Ensure the final rotation is set exactly to the target
        transform.localRotation = Quaternion.Euler(originXRot, targetRotationY, originZRot);
        isOpened = true;
    }

    public IEnumerator CloseCoroutine()
    {
        Managers.Sound.Play("Sounds/Objects/metal-door-close-1");
        float startRotationY = transform.localEulerAngles.y;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float smoothStep = Mathf.SmoothStep(0f, 1f, t);
            float currentRotationY = Mathf.LerpAngle(startRotationY, originYRotation, smoothStep);
            transform.localRotation = Quaternion.Euler(originXRot, currentRotationY, originZRot);

            yield return null;
        }
        // Ensure the final rotation is set exactly to the target
        transform.localRotation = Quaternion.Euler(originXRot, originYRotation, originZRot);
        isOpened = false;
    }
}
