using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable
{
	[SerializeField] bool isActive = false;

	[SerializeField] List<AudioClip> turnOnSounds;
	[SerializeField] List<AudioClip> turnOffSounds;
	[SerializeField] AudioClip lightBuzzSound;

	private AudioSource audioSource;
	GameObject pointLight;

	void Start()
	{
		pointLight = transform.GetChild(0).gameObject;
		audioSource = GetComponent<AudioSource>();
		AudioSourceUtil.Instance.SetAudioSourceProperties(audioSource);
		if (isActive)
		{
			pointLight.SetActive(true);
			PlayLightBuzzSound();
		}
	}

	void IInteractable.Interact()
	{
		if (!isActive)
		{
			TurnOn();
		} else
		{
			TurnOff();
		}
	}

	void TurnOn()
	{
		isActive = true;
		pointLight.SetActive(true);
		PlayTurnOnSound();
		PlayLightBuzzSound();
	}

	void TurnOff()
	{
		isActive = false;
		pointLight.SetActive(false);
		StopLightBuzzSound();
	}



	void PlayTurnOnSound()
	{
		if (turnOnSounds.Count > 0)
		{
			AudioClip clip = turnOnSounds[Random.Range(0, turnOnSounds.Count)];
			audioSource.PlayOneShot(clip);
		}
	}

	void PlayTurnOffSound()
	{
		if (turnOffSounds.Count > 0)
		{
			AudioClip clip = turnOffSounds[Random.Range(0, turnOffSounds.Count)];
			audioSource.PlayOneShot(clip);
		}
	}
	void PlayLightBuzzSound()
	{
		if (isActive)
		{
			audioSource.loop = true;
			audioSource.clip = lightBuzzSound;
			audioSource.Play();
		}
	}
	void StopLightBuzzSound()
	{
		audioSource.loop = false;
		audioSource.Stop();
	}
}
