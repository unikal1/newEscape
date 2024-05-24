using System.Collections;
using UnityEngine;

public class TapWater : MonoBehaviour, IInteractable {
	[SerializeField] bool isActive = false;
	[SerializeField] ParticleSystem dropParticle;
	[SerializeField] ParticleSystem hitParticle;
	[SerializeField] float delay = 0.5f;

	[SerializeField] AudioClip[] tapAudios; // 0: 물을 트는 소리, 1: 물이 틀어지고 있는 소리, 2: 물을 끄는 소리
	AudioSource audioSource;

	private Coroutine playWaterSoundCoroutine;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}


	void IInteractable.Interact() {
		if (isActive) {
			TurnOff();
			isActive = false;
		} else {
			TurnOn();
			isActive = true;
		}
	}

	void TurnOn() {
		StartCoroutine(PlayHitParticleAfterDelay());
		if (playWaterSoundCoroutine != null) {
			StopCoroutine(playWaterSoundCoroutine);
		}
		playWaterSoundCoroutine = StartCoroutine(PlayWaterSoundAfterFirstSound());
	}

	void TurnOff() {
		StartCoroutine(StopHitParticleAfterDelay());
		if (playWaterSoundCoroutine != null) {
			StopCoroutine(playWaterSoundCoroutine);
			playWaterSoundCoroutine = null;
		}
	}

	IEnumerator PlayHitParticleAfterDelay() {
		dropParticle.Play();
		yield return new WaitForSeconds(delay);
		hitParticle.Play();
	}

	IEnumerator StopHitParticleAfterDelay() {
		dropParticle.Stop();
		yield return new WaitForSeconds(delay);
		hitParticle.Stop();
		audioSource.Stop(); // 물이 틀어지고 있는 소리 정지
		PlayAudio(2, false); // 물을 끄는 소리 재생
	}

	IEnumerator PlayWaterSoundAfterFirstSound() {
		PlayAudio(0, false); // 물을 트는 소리 재생
							 // 첫 번째 오디오가 끝날 때까지 대기
		yield return new WaitUntil(() => !audioSource.isPlaying);
		PlayAudio(1, true); // 물이 틀어지고 있는 소리 재생 (루프 설정)
	}

	void PlayAudio(int index, bool loop) {
		if (index >= 0 && index < tapAudios.Length) {
			audioSource.clip = tapAudios[index];
			audioSource.loop = loop;
			audioSource.Play();
		}
	}
}
