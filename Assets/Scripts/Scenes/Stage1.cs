using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage1 : BaseScene
{
	[Header("Initialization")]
	[SerializeField] GameObject lamp;
	//[SerializeField] GameObject bodyContainingMorgueBoxDoor;
	//[SerializeField] GameObject body;
	[SerializeField] AudioClip ScareSound;
	[SerializeField] GameObject key;

	[Header("Event Parameters")]
	bool firstEventFlag = false;
    bool bodyEncounter = false;
	bool keyObtained = false;

	[Header("others")]
	Coroutine lampBlinkCoroutine;

	protected override void Init()
	{
        base.Init();
		SceneType = Define.Scene.Stage1;
		SceneUI = Managers.UI.ShowSceneUI<UI_GameScene>();
    }

    void Start()
    {
        lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight();
        //bodyContainingMorgueBoxDoor.GetComponent<Door>().OnDoorOpened.AddListener(HandleDoorOpened);
        //key.GetComponent<Key>().OnObtain.AddListener(HandleKeyObtained);
        //audioSource = GetComponent<AudioSource>();
    }

	void HandleDoorOpened() {
		IEnumerator coroutine() {
			// Error: 외부의 Coroutine을 그냥 중단시킬 시 Continue Coroutine failure 발생함
			lamp.GetComponent<Lamp>().StopCoroutine(lampBlinkCoroutine);
			lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight(1, 1f, 0f, false);
			yield return new WaitForSeconds(1f);
			//audioSource.PlayOneShot(ScareSound);
			Managers.Sound.Play(ScareSound);
			lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight();

			// 5초 후
			yield return new WaitForSeconds(5f);
			lamp.GetComponent<Lamp>().StopCoroutine(lampBlinkCoroutine);
			lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight(1, 1f, 0f, false);

			// 모든 시체박스 문 열림
			List<GameObject> doorList = FindGameObjects.ContainsString("MorgueBox_Door");
			//doorList.ForEach(door => door.GetComponent<Door>().Open(false, 0f));
			//bodyContainingMorgueBoxDoor.GetComponent<Door>().OnDoorOpened.RemoveListener(HandleDoorOpened);
			//bodyContainingMorgueBoxDoor.GetComponent<Door>().Close(false, 0f);
			///body.SetActive(false);
			//key.SetActive(true);

			// 다시 원상태로 복구

			yield return new WaitForSeconds(1f);
			lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight();
		}
		///bodyContainingMorgueBoxDoor.GetComponent<Door>().OnDoorOpened.RemoveListener(HandleDoorOpened);
		StartCoroutine(coroutine());
	}

	void HandleKeyObtained() {
		//key.GetComponent<Key>().OnObtain.RemoveListener(HandleKeyObtained);

	}

    public override void Clear()
    {
        
    }
}
