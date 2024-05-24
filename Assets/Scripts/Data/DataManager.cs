using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DataDefine;

public class DataManager : MonoBehaviour
{
    public static DataManager singleTon;
    public GameObject _gameObject;
    [SerializeField]
    private Inventory inventory;
    public Inventory Inventory { get { return inventory; }  set { inventory = Inventory; } }
    // Start is called before the first frame update


    public void Awake()
    {
        if (singleTon == null)
        {
            singleTon = this;
            DontDestroyOnLoad(_gameObject);
        }
        else if (singleTon != this)
        {
            Destroy(singleTon._gameObject);
        }
        Init();
    }

    public void Init()
    {
        Inventory = JsonManager.Load<Inventory>();
    }
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
}
