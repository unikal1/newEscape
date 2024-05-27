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

    [SerializeField] float duration = 0.5f;
    [SerializeField] float targetRotationY = -80f;
    [SerializeField] float originYRotation = 0f;

	[SerializeField] List<AudioClip> openSounds;
	[SerializeField] List<AudioClip> closeSounds;

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
                    Open();
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
        {
            Managers.Sound.Play("Sounds/Objects/main-door-open-1");
            anim.SetBool("IsOpen", true);
			isOpened = true;
        }
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
        {
            Managers.Sound.Play("Sounds/Objects/main-door-open-1");
            anim.SetBool("IsOpen", false);
			isOpened = false;
		}
        else
        {
            if (openCoroutine != null)
                StopCoroutine(openCoroutine);

            closeCoroutine = StartCoroutine(CloseCoroutine());
        }
	}

    public IEnumerator OpenCoroutine()
    {
        PlayOpenSound();
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
        PlayCloseSound();
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

	void PlayOpenSound() {
		if (openSounds.Count > 0) {
			AudioClip clip = openSounds[Random.Range(0, openSounds.Count)];
			Managers.Sound.Play(clip);
		}
	}

	void PlayCloseSound() {
		if (closeSounds.Count > 0) {
			AudioClip clip = closeSounds[Random.Range(0, closeSounds.Count)];
			Managers.Sound.Play(clip);
		}
	}

}
