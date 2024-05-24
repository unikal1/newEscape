using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour, IInteractable
{
	[SerializeField] bool isOpened = false;
	[SerializeField] float duration = 0.5f; // 여닫는데 걸리는 시간
	[SerializeField] float targetPositionZ = -0.4f;

	[SerializeField] List<AudioClip> openSounds;
	[SerializeField] List<AudioClip> closeSounds;
	private AudioSource audioSource;

	float originXPos;
	float originYPos;
	[SerializeField] float originZPos = -0.0497f;

	Coroutine openCoroutine = null;
	Coroutine closeCoroutine = null;

	void Start()
	{
		originXPos = transform.localPosition.x;
		originYPos = transform.localPosition.y;


		audioSource = GetComponent<AudioSource>();
		AudioSourceUtil.Instance.SetAudioSourceProperties(audioSource);
	}

	void IInteractable.Interact()
	{
		if (!isOpened)
		{
			if (closeCoroutine != null)
			{
				StopCoroutine(closeCoroutine);
			}
			openCoroutine = StartCoroutine(OpenCoroutine());
			isOpened = true;
		} else
		{
			if (openCoroutine != null)
			{
				StopCoroutine(openCoroutine);
			}
			closeCoroutine = StartCoroutine(CloseCoroutine());
			isOpened = false;
		}
	}

	void Open()
	{
		StartCoroutine(OpenCoroutine());
		isOpened = true;
	}

	void Close()
	{
		StartCoroutine(CloseCoroutine());
		isOpened = false;
	}

	IEnumerator OpenCoroutine()
	{
		PlayOpenSound();
		float startPositionZ = transform.localPosition.z;
		float elapsedTime = 0f;

		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / duration;
			float smoothStep = Mathf.SmoothStep(0f, 1f, t);
			float currentPositionZ = Mathf.Lerp(startPositionZ, targetPositionZ, smoothStep); // Change ypos to zpos
			transform.localPosition = new Vector3(originXPos, originYPos, currentPositionZ); // Change ypos to zpos

			yield return null;
		}
		// Ensure the final position is set exactly to the target
		transform.localPosition = new Vector3(originXPos, originYPos, targetPositionZ); // Change ypos to zpos
	}

	IEnumerator CloseCoroutine()
	{
		PlayCloseSound();
		float startPositionZ = transform.localPosition.z;
		float elapsedTime = 0f;

		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / duration;
			float smoothStep = Mathf.SmoothStep(0f, 1f, t);
			float currentPositionZ = Mathf.Lerp(startPositionZ, originZPos, smoothStep);
			transform.localPosition = new Vector3(originXPos, originYPos, currentPositionZ);

			yield return null;
		}
		// Ensure the final position is set exactly to the target
		transform.localPosition = new Vector3(originXPos, originYPos, originZPos);
	}

	void PlayOpenSound()
	{
		if (openSounds.Count > 0)
		{
			AudioClip clip = openSounds[Random.Range(0, openSounds.Count)];
			audioSource.PlayOneShot(clip);
		}
	}

	void PlayCloseSound()
	{
		if (closeSounds.Count > 0)
		{
			AudioClip clip = openSounds[Random.Range(0, closeSounds.Count)];
			audioSource.PlayOneShot(clip);
		}
	}
}
