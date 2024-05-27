using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MainScene : UI_Popup
{
    public enum Buttons
    {
        PlayButton,
        ExitButton
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.PlayButton).gameObject.AddUIEvent(OnClickPlay);
        GetButton((int)Buttons.ExitButton).gameObject.AddUIEvent(OnClickExit);
    }

    private void OnClickPlay(PointerEventData data)
    {
        Managers.Game.State = Define.GameState.Play;
        Managers.Scene.LoadScene(Define.Scene.Stage1);
    }
    public void OnClickExit(PointerEventData data)
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
}
