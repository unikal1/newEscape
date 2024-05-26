using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Right Axis Door - TargetRotationY: -80, OriginYRotation: 0
/// Left Axis Door - TargetRotationY: -100, OriginYRotation: 180
/// </summary>
public class Door : MonoBehaviour, IInteractable
{
	[SerializeField] bool isOpened = false;
	[SerializeField] bool isLocked = false;
	[SerializeField] float duration = 0.5f; // 여닫는데 걸리는 시간
	[SerializeField] float targetRotationY = -80f;
	[SerializeField] float originYRotation = 0f;

	[SerializeField] List<AudioClip> openSounds;
	[SerializeField] List<AudioClip> closeSounds;
	[SerializeField] List<AudioClip> lockedInteractSounds;

	public UnityEvent OnDoorOpened;
	public UnityEvent OnDoorClosed;

	private AudioSource audioSource;

	bool isOpening = false;
	float originXRot;
	float originZRot;

	Coroutine openCoroutine = null;
	Coroutine closeCoroutine = null;

	void Awake() {
		originXRot = transform.localEulerAngles.x;
		originZRot = transform.localEulerAngles.z;

		audioSource = GetComponent<AudioSource>();
		if(audioSource != null)
			AudioSourceUtil.Instance.SetAudioSourceProperties(audioSource);
        else
            Debug.LogWarning("AudioSource is not attached to " + gameObject.name);
    }

	void IInteractable.Interact() {
		if (isLocked)
		{
			PlayLockedInteractSound();
			return;
		}

		if (!isOpened) {
			Open();
		} else {
			Close();
		}
	}

	public void Open(bool playSound = true, float duration = -1f) {
		IEnumerator OpenCoroutine() {
			if (playSound) PlayOpenSound();
			float startRotationY = transform.localEulerAngles.y;
			float elapsedTime = 0f;

			while (elapsedTime < duration) {
				elapsedTime += Time.deltaTime;
				float t = elapsedTime / duration;
				float smoothStep = Mathf.SmoothStep(0f, 1f, t);
				float currentRotationY = Mathf.LerpAngle(startRotationY, targetRotationY, smoothStep);
				transform.localRotation = Quaternion.Euler(originXRot, currentRotationY, originZRot);

				yield return null;
			}
			// Ensure the final rotation is set exactly to the target
			transform.localRotation = Quaternion.Euler(originXRot, targetRotationY, originZRot);
		}

		if (duration == -1f) duration = this.duration;
		if (closeCoroutine != null) {
			StopCoroutine(closeCoroutine);
		}
		openCoroutine = StartCoroutine(OpenCoroutine());
		isOpened = true;
		OnDoorOpened?.Invoke();
	}

	public void Close(bool playSound = true, float duration = -1f) {
		IEnumerator CloseCoroutine() {
			if (playSound) PlayCloseSound();
			float startRotationY = transform.localEulerAngles.y;

			float elapsedTime = 0f;

			while (elapsedTime < duration) {
				elapsedTime += Time.deltaTime;
				float t = elapsedTime / duration;
				float smoothStep = Mathf.SmoothStep(0f, 1f, t);
				float currentRotationY = Mathf.LerpAngle(startRotationY, originYRotation, smoothStep);
				transform.localRotation = Quaternion.Euler(originXRot, currentRotationY, originZRot);

				yield return null;
			}
			// Ensure the final rotation is set exactly to the target
			transform.localRotation = Quaternion.Euler(originXRot, originYRotation, originZRot);
		}

		if (duration == -1f) duration = this.duration;
		if (openCoroutine != null) {
			StopCoroutine(openCoroutine);
		}
		closeCoroutine = StartCoroutine(CloseCoroutine());
		isOpened = false;
		OnDoorClosed?.Invoke();
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
			AudioClip clip = closeSounds[Random.Range(0, closeSounds.Count)];
			audioSource.PlayOneShot(clip);
		}
	}

	void PlayLockedInteractSound()
	{
		if (lockedInteractSounds.Count > 0)
		{
			AudioClip clip = lockedInteractSounds[Random.Range(0, lockedInteractSounds.Count)];
			audioSource.PlayOneShot(clip);
		}
	}
}
