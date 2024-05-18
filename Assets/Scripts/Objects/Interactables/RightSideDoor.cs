using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSideDoor : MonoBehaviour
{
	[SerializeField] bool isOpened = false;
	[SerializeField] float duration = 2f; // 여닫는데 걸리는 시간
	[SerializeField] float openedRotationY = -80f;

	float closedRotationY = 0f;	
	bool isOpening = false; // 문이 열리고 있는지 여부
	void Start() {
		Open();
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
		float currentRotationY = transform.localEulerAngles.y;
		float elapsedTime = 0f;
		Debug.Log(transform.localEulerAngles);
		while (currentRotationY > openedRotationY) {
			elapsedTime += Time.deltaTime;
			currentRotationY = Mathf.Lerp(currentRotationY, openedRotationY, elapsedTime / duration);
			transform.localRotation = Quaternion.Euler(0f, currentRotationY, 0f);
			Debug.Log(transform.localEulerAngles);

			yield return null;
		}
	}
	IEnumerator CloseCoroutine() {
		float currentRotationY = transform.localEulerAngles.y;
		float elapsedTime = 0f;
		while (currentRotationY < closedRotationY) {
			elapsedTime += Time.deltaTime;
			currentRotationY = Mathf.Lerp(currentRotationY, closedRotationY, elapsedTime / duration);
			transform.localRotation = Quaternion.Euler(0f, currentRotationY, 0f);
			yield return null;
		}
	}

}
