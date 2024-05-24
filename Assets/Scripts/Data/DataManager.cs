using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DataDefine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    private Inventory inventorydata;
    public Inventory InventoryData { get { return inventorydata; }  set { inventorydata = value; } }

    [SerializeField]
    private int selectedSlotIndex;
    public int SelectedSlotIndex { get { return selectedSlotIndex; } set { selectedSlotIndex = value; } }
    // Start is called before the first frame update


    public void Awake()
    {
    }

    public void Init()
    {
        InventoryData = JsonManager.Load<Inventory>();
    }
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }
}
