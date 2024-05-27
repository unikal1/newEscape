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
    }

}
