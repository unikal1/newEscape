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

	void Awake()
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

	public void TurnOn(bool playSound = true)
	{
		isActive = true;
		pointLight.SetActive(true);
		PlayTurnOnSound();
		PlayLightBuzzSound();
	}

	public void TurnOff(bool playSound = true)
	{
		isActive = false;
		pointLight.SetActive(false);
		PlayTurnOffSound();
		StopLightBuzzSound();
	}

	/// <summary>
	/// Blinks the light of the lamp.
	/// </summary>
	/// <param name="repetition">The number of times the light should blink. Use -1 for infinite blinking.</param>
	/// <param name="blinkTime">The maximum time in seconds for which the light remains off during each blink.</param>
	/// <param name="blinkInterval">The maximum time in seconds between each blink.</param>
	public Coroutine BlinkLight(int repetition = -1, float blinkTime=0.3f, float blinkInterval=1.0f, bool randomize = true) {
		IEnumerator BlinkLightCoroutine() {
			while (isActive && (repetition == -1 || repetition-- > 0)) {
				pointLight.SetActive(false);
				float _blinkTime = randomize? Random.Range(0f, blinkTime * 2) : blinkTime;
				yield return new WaitForSeconds(_blinkTime);
				pointLight.SetActive(true);
				float _blinkInterval = randomize ? Random.Range(0f, blinkInterval * 2) : blinkInterval;
				yield return new WaitForSeconds(_blinkInterval);
			}
		}
		return StartCoroutine(BlinkLightCoroutine());
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
