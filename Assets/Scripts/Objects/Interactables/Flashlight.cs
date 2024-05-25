using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flashlight : MonoBehaviour, IObtainable {
	[SerializeField] List<AudioClip> obtainSounds;

	public UnityEvent OnObtain;

	private AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
		AudioSourceUtil.Instance.SetAudioSourceProperties(audioSource);
	}

	public void Obtain() {
		PlayObtainSound();
		OnObtain?.Invoke();
		// 오브젝트를 즉시 비활성화
		Destroy(gameObject);
	}

	public void TurnOn() {
		
	}

	void PlayObtainSound() {
		if (obtainSounds.Count > 0) {
			AudioClip clip = obtainSounds[Random.Range(0, obtainSounds.Count)];
			// 오디오 소스를 새로운 오브젝트에 복사
			GameObject audioObject = new GameObject("TempAudio");
			AudioSource tempAudioSource = audioObject.AddComponent<AudioSource>();
			tempAudioSource.clip = clip;
			tempAudioSource.Play();
			Destroy(audioObject, clip.length); // 오디오 클립이 재생된 후 오브젝트를 파괴
		}
	}
}
