using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage1 : BaseScene
{
	[Header("Initialization")]
	[SerializeField] GameObject lamp;
	[SerializeField] GameObject bodyContainingMorgueBoxDoor;
	[SerializeField] GameObject body;
	[SerializeField] AudioClip ScareSound;

	[Header("Event Parameters")]
	

	[Header("others")]
	Coroutine lampBlinkCoroutine;

    public override void Clear()
    {
        
    }

    protected override void Init()
	{
        base.Init();
		SceneType = Define.Scene.Stage1;
		SceneUI = Managers.UI.ShowSceneUI<UI_GameScene>();
    }

    void Start()
    {
        lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight();
		bodyContainingMorgueBoxDoor.GetComponent<Door>().OnDoorOpened.AddListener(HandleDoorOpened);
	}

	void HandleDoorOpened() {
		IEnumerator coroutine() {
			// Error: 외부의 Coroutine을 그냥 중단시킬 시 Continue Coroutine failure 발생함
			lamp.GetComponent<Lamp>().StopCoroutine(lampBlinkCoroutine);
			lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight(1, 1f, 0f, false);
			yield return new WaitForSeconds(1f);
			Managers.Sound.Play(ScareSound);
			lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight();

			// 5초 후
			yield return new WaitForSeconds(5f);
			lamp.GetComponent<Lamp>().StopCoroutine(lampBlinkCoroutine);
			lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight(1, 1f, 0f, false);

			// 모든 시체박스 문 열림, 시체 사라짐
			List<GameObject> doorList = FindGameObjects.ContainsString("MorgueBox_Door");
			doorList.ForEach(door => door.GetComponent<Door>().Open(false, 0f));
			bodyContainingMorgueBoxDoor.GetComponent<Door>().OnDoorOpened.RemoveListener(HandleDoorOpened);
			bodyContainingMorgueBoxDoor.GetComponent<Door>().Close(false, 0f);
			body.SetActive(false);

			// 다시 원상태로 복구
			yield return new WaitForSeconds(1f);
			lampBlinkCoroutine = lamp.GetComponent<Lamp>().BlinkLight();
		}
		bodyContainingMorgueBoxDoor.GetComponent<Door>().OnDoorOpened.RemoveListener(HandleDoorOpened);
		StartCoroutine(coroutine());
	}
}
