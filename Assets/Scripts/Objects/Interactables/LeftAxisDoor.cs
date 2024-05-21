using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAxisDoor : MonoBehaviour, IInteractable {
	[SerializeField] bool isOpened = false;
	[SerializeField] float duration = 0.5f; // 여닫는데 걸리는 시간
	[SerializeField] float targetRotationY = -80f;

	bool isOpening = false;

	float originXRot;
	float originZRot;

	Coroutine openCoroutine = null;
	Coroutine closeCoroutine = null;

	void Start() {
		originXRot = transform.localEulerAngles.x;
		originZRot = transform.localEulerAngles.z;
		Debug.Log("xRot: " + originXRot);
		Debug.Log("yRot: " + transform.localRotation.y);
		Debug.Log("zRot: " + originZRot);
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

	void Open() {
		StartCoroutine(OpenCoroutine());
		isOpened = true;
	}

	void Close() {
		StartCoroutine(CloseCoroutine());
		isOpened = false;
	}

	IEnumerator OpenCoroutine() {
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

	IEnumerator CloseCoroutine() {
		float startRotationY = transform.localEulerAngles.y;
		Debug.Log(startRotationY);
		float elapsedTime = 0f;

		while (elapsedTime < duration) {
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / duration;
			float smoothStep = Mathf.SmoothStep(0f, 1f, t);
			float currentRotationY = Mathf.LerpAngle(startRotationY, 180f, smoothStep);
			transform.localRotation = Quaternion.Euler(originXRot, currentRotationY, originZRot);
			/*Debug.Log(transform.localEulerAngles);*/

			yield return null;
		}
		// Ensure the final rotation is set exactly to the target
		transform.localRotation = Quaternion.Euler(originXRot, 180f, originZRot);
	}
}
