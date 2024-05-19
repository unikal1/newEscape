using System.Collections;
using UnityEngine;

public class LeftAxisDoor : MonoBehaviour, IInteractable {
	[SerializeField] bool isOpened = false;
	[SerializeField] float duration = 0.5f; // 여닫는데 걸리는 시간
	[SerializeField] float targetRotationY = 80f;

	bool isOpening = false;

	float localRotX;
	float localRotZ;

	Coroutine openCoroutine = null;
	Coroutine closeCoroutine = null;

	void Start() {
		localRotX = transform.localEulerAngles.x;
		localRotZ = transform.localEulerAngles.z;
		Debug.Log("localX: " + localRotX);
		Debug.Log("localY: " + transform.localEulerAngles.y);
		Debug.Log("localZ: " + localRotZ);
	}

	void IInteractable.Interact() {
		if (!isOpened) {
			if (closeCoroutine != null) {
				StopCoroutine(closeCoroutine);
			}
			openCoroutine = StartCoroutine(OpenCoroutine());
			isOpened = true;
		} else {
			if (openCoroutine != null) {
				StopCoroutine(openCoroutine);
			}
			closeCoroutine = StartCoroutine(CloseCoroutine());
			isOpened = false;
		}
	}

	IEnumerator OpenCoroutine() {
		float startRotationY = transform.localEulerAngles.y;
		float endRotationY = targetRotationY;
		float elapsedTime = 0f;

		while (elapsedTime < duration) {
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / duration;
			float smoothStep = Mathf.SmoothStep(0f, 1f, t);
			float currentRotationY = Mathf.LerpAngle(startRotationY, endRotationY, smoothStep);
			transform.localRotation = Quaternion.Euler(localRotX, currentRotationY, localRotZ);

			yield return null;
		}
		// Ensure the final rotation is set exactly to the target
		transform.localRotation = Quaternion.Euler(localRotX, endRotationY, localRotZ);
		Debug.Log("localY: " + transform.localEulerAngles.y);
	}

	IEnumerator CloseCoroutine() {
		float startRotationY = transform.localEulerAngles.y;
		float endRotationY = 0f;
		float elapsedTime = 0f;

		while (elapsedTime < duration) {
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / duration;
			float smoothStep = Mathf.SmoothStep(0f, 1f, t);
			float currentRotationY = Mathf.LerpAngle(startRotationY, endRotationY, smoothStep);
			transform.localRotation = Quaternion.Euler(localRotX, currentRotationY, localRotZ);

			yield return null;
		}
		// Ensure the final rotation is set exactly to the target
		transform.localRotation = Quaternion.Euler(localRotX, endRotationY, localRotZ);
		Debug.Log("localY: " + transform.localEulerAngles.y);
	}
}
