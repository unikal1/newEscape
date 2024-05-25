using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
	[Header("Initialization")]
	[SerializeField] GameObject lamp;
	[SerializeField] GameObject bodyContainingMorgueBoxDoor;
	[SerializeField] GameObject body;
	[SerializeField] AudioClip ScareSound;
	[SerializeField] GameObject key;

	[Header("Event Parameters")]
	bool firstEventFlag = false;
    bool bodyEncounter = false;
	bool keyObtained = false;

	[Header("others")]
	Coroutine lampBlinkCoroutine;
	AudioSource audioSource;

    void Start()
    {
		lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight();
		bodyContainingMorgueBoxDoor.GetComponent<Door>().OnDoorOpened.AddListener(HandleDoorOpened);
		key.GetComponent<Key>().OnObtain.AddListener(HandleKeyObtained);
		audioSource = GetComponent<AudioSource>();
	}

	void HandleDoorOpened() {
		IEnumerator coroutine() {
			// Error: 외부의 Coroutine을 그냥 중단시킬 시 Continue Coroutine failure 발생함
			lamp.GetComponent<Lamp>().StopCoroutine(lampBlinkCoroutine);
			lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight(1, 1f, 0f, false);
			yield return new WaitForSeconds(1f);
			audioSource.PlayOneShot(ScareSound);
			lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight();

			// 5초 후에 모든 문 열림, 시체 사라짐
			yield return new WaitForSeconds(5f);
			lamp.GetComponent<Lamp>().StopCoroutine(lampBlinkCoroutine);
			lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight(1, 1f, 0f, false);
			List<GameObject> doorList = FindGameObjects.ContainsString("MorgueBox_Door");
			doorList.ForEach(door => door.GetComponent<Door>().Open(false));
			bodyContainingMorgueBoxDoor.GetComponent<Door>().OnDoorOpened.RemoveListener(HandleDoorOpened);
			bodyContainingMorgueBoxDoor.GetComponent<Door>().Close(false);
			body.SetActive(false);
			key.SetActive(true);
			
			// 전등을 다시 원상태로 복구
			yield return new WaitForSeconds(1f);
			lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight();
		}
		StartCoroutine(coroutine());
	}

	void HandleKeyObtained() {
		key.GetComponent<Key>().OnObtain.RemoveListener(HandleKeyObtained);

	}

	


}
