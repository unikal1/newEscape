using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour, IObtainable
{
	[SerializeField] List<AudioClip> obtainSounds;

	private AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

    public void Obtain()
	{
		PlayObtainSound();
		
		gameObject.SetActive(false);
	}
	void PlayObtainSound()
	{
		if (obtainSounds.Count > 0)
		{
			AudioClip clip = obtainSounds[Random.Range(0, obtainSounds.Count)];
			audioSource.PlayOneShot(clip);
		}
	}
}
