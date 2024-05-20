using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightAxisDoor : MonoBehaviour, IInteractable
{
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
		float startRotationY = transform.transform.localEulerAngles.y;
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
		Debug.Log("opened local: " + transform.localRotation);
		Debug.Log("opened global: " + transform.rotation);
	}

	IEnumerator CloseCoroutine() {
		float startRotationY = transform.localEulerAngles.y;
		Debug.Log("startRotY: "+ startRotationY);

		float elapsedTime = 0f;

		while (elapsedTime < duration) {
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / duration;
			float smoothStep = Mathf.SmoothStep(0f, 1f, t);
			float currentRotationY = Mathf.LerpAngle(startRotationY, 0f, smoothStep);
			transform.localRotation = Quaternion.Euler(originXRot, currentRotationY, originZRot);
			/*Debug.Log(transform.localEulerAngles);*/

			yield return null;
		}
		// Ensure the final rotation is set exactly to the target
		transform.localRotation = Quaternion.Euler(originXRot, 0f, originZRot);
		Debug.Log("closed local: " + transform.localRotation);
		Debug.Log("closed global: " + transform.rotation);
	}
}
