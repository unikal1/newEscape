using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : UI_Base
{
    [SerializeField]
    private int index = 0;
    public int Index { get { return index; } set { index = value; } }

    [SerializeField]
    private BaseItemData itemData;
    public BaseItemData ItemData { get { return itemData; } set { itemData = value; } }

    [SerializeField]
    private Image backGround;
    public Image BackGround { get { return backGround; } set { backGround = value; } }
    [SerializeField]
    private Image slotImage;
    public Image SlotImage { get { return slotImage; } set { slotImage = value; } }
    public override void Init()
    {
        if(itemData != null)
        {
            Sprite itemSprite = Managers.Resource.Load<Sprite>($"Sprites/{itemData.ItemSprite}");
            if(itemSprite != null)
            {
                SlotImage.sprite = itemSprite;
            }
            else
            {
                SlotImage.sprite = null;
            }
            SlotImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            SlotImage.sprite = null;
            SlotImage.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
    }
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void UseItem()
    {
        switch (ItemData.ItemType)
        {
            case DataDefine.EItemType.Default:
                Debug.Log("No Item");
                break;
            case DataDefine.EItemType.Paper0:
                Debug.Log("Paper0 Use");
                break;
            case DataDefine.EItemType.Paper1:
                Debug.Log("Paper1 Use");
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
