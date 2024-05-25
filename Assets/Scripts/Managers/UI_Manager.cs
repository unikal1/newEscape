using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;

public class UI_Manager
{
    int _order = 10;

    Stack<UI_Popup> _popUpStack = new Stack<UI_Popup>();
    public Stack<UI_Popup> PopUpStack { get { return _popUpStack; } }

    UI_Scene _sceneUI = null;
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_root" };
            }
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 5;
        }
    }
    public T ShowExtraUI<T>(string name = null, Transform parent = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }
        GameObject go = Managers.Resource.Instantiate($"UI/ExtraUI/{name}");
        T anyUI = Util.GetOrAddComponent<T>(go);

        if(parent == null)
            go.transform.SetParent(Root.transform);

        go.transform.SetParent(parent);

        return anyUI;
    }
    public T ShowPopUpUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }
        GameObject go = Managers.Resource.Instantiate($"UI/PopUp/{name}");
        T popUp = Util.GetOrAddComponent<T>(go);
        _popUpStack.Push(popUp);
        go.transform.SetParent(Root.transform);
        return popUp;
    }
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }
        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;
        go.transform.SetParent(Root.transform);
        return sceneUI;
    }

    public void ClosePopUpUI()
    {
        if (_popUpStack.Count == 0)
        {
            return;
        }
        UI_Popup popUP = _popUpStack.Pop();
        Managers.Resource.Destroy(popUP.gameObject);
        popUP = null;

        _order--;
    }
    public void ClosePopUpUI(UI_Popup popUp)
    {
        if (_popUpStack.Count == 0)
        {
            return;
        }
        if (_popUpStack.Peek() != popUp)
        {
            Debug.Log("Close PopUp Failed");
            return;
        }
        ClosePopUpUI();

    }
    public void CloseAllPopUPUI()
    {
        while (_popUpStack.Count > 0)
        {
            ClosePopUpUI();
        }
    }
    public void Clear()
    {
        CloseAllPopUPUI();
        _sceneUI = null;
    }
}
