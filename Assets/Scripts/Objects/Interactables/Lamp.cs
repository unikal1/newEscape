using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable
{
	[SerializeField] bool isActive = false;
	[SerializeField] GameObject pointLight;

	void IInteractable.Interact() {
		if (!isActive) {
			TurnOn();
		} else {
			TurnOff();
		}
	}

	void TurnOn() {
		isActive = true;
		pointLight.SetActive(true);
	}

	void TurnOff() {
		isActive = false;
		pointLight.SetActive(false);
	}
}
