using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class AudioSourceUtil
{
	/*const float DEFAULT_VOLUME = 1.0f;*/
	const float DEFAULT_SPATIAL_BLEND = 0.7f;
	const float DEFAULT_MIN_DISTANCE = 0.0f;
	const float DEFAULT_MAX_DISTANCE = 15.0f;

	private static AudioSourceUtil instance;

	//
	public static AudioSourceUtil Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new AudioSourceUtil();
			}
			return instance;
		}
	}

	// default audio source properties
	/*private float volume = DEFAULT_VOLUME;*/
	private float spatialBlend = DEFAULT_SPATIAL_BLEND;
	private float minDistance = DEFAULT_MIN_DISTANCE;
	private float maxDistance = DEFAULT_MAX_DISTANCE;




	private AudioSourceUtil()
	{
		// Private constructor to prevent instantiation
	}

	public void SetAudioSourceProperties(
		AudioSource audioSource,
		/*float volume = DEFAULT_VOLUME,*/
		float spatialBlend = DEFAULT_SPATIAL_BLEND,
		float minDistance = DEFAULT_MIN_DISTANCE,
		float maxDistance = DEFAULT_MAX_DISTANCE
	)
	{
		/*audioSource.volume = volume;*/
		audioSource.spatialBlend = spatialBlend;
		audioSource.minDistance = minDistance;
		audioSource.maxDistance = maxDistance;
	}

}
