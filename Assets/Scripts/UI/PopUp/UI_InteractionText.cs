using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InteractionText : UI_Popup
{
    [SerializeField]
    private Text interactionText;
    public Text InteractionText { get => interactionText; private set => interactionText = value; }
    public override void Init()
    {
        base.Init();
        if (InteractionText == null)
            InteractionText = Util.FindChild<Text>(gameObject);
        if(InteractionText.IsActive())
            InteractionText.gameObject.SetActive(false);
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
