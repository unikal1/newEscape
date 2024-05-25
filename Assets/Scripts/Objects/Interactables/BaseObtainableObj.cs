using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObtainableObj : MonoBehaviour, IObtainable
{
    [SerializeField]
    private BaseItemData itemData;
    public BaseItemData ItemData { get { return itemData; } set { itemData = value; } }
    public virtual void Obtain()
    {
        Managers.Data.InventoryData.itemDatas.Add(ItemData);
        ((UI_GameScene)Managers.Scene.CurrentScene.SceneUI).UI_Inventory.UpdateInventory();
        // 오브젝트를 즉시 비활성화
        Destroy(gameObject);
    }

    public virtual void Init()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
