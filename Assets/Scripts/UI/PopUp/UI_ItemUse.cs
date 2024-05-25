using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemUse : UI_Popup
{
    [SerializeField]
    protected Image popupImage;
    public Image PopupImage { get => popupImage; set => popupImage = value; }
    public override void Init()
    {
        base.Init();
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
