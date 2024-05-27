using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    [SerializeField]
    static Managers s_instance = null;
    public static Managers Instance { get { Init(); return s_instance; } }

    UI_Manager _ui = new UI_Manager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    InputManager _input = new InputManager();
    SoundManager _sound = new SoundManager();
    GameManagerEx _game = new GameManagerEx();
    DataManager _data = new DataManager();

    public static GameManagerEx Game { get { return Instance._game; } }
    public static UI_Manager UI { get { return Instance._ui; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static InputManager Input { get { return Instance._input; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static DataManager Data { get { return Instance._data; } }

    private void Awake()
    {
        Init();
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        GameObject go = GameObject.Find("@Manager");
        if (s_instance == null)
        {
            if (go == null)
            {
                go = new GameObject { name = "@Manager" };
            }
            s_instance = go.GetOrAddComponent<Managers>();
        }
        else
        {
            return;
        }

        s_instance._data.Init();
        s_instance._sound.Init();
    }
    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
    }
}
