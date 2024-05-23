using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable
{
	[SerializeField] bool isActive = false;
	[SerializeField] GameObject pointLight;

	[SerializeField] List<AudioClip> turnOnSounds;
	[SerializeField] List<AudioClip> turnOffSounds;

	private AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		AudioSourceUtil.Instance.SetAudioSourceProperties(audioSource);
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
	}

	void TurnOff()
	{
		isActive = false;
		pointLight.SetActive(false);
		PlayTurnOffSound();
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
}
