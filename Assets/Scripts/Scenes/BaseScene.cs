using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    private UI_Scene _sceneui;
    public UI_Scene SceneUI
    {
        get
        {
            if (_sceneui == null)
                _sceneui = GetComponentInChildren<UI_Scene>();
            return _sceneui;
        }
        protected set { _sceneui = value; }
    }
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;
    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
        {
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    }

    public abstract void Clear();
}