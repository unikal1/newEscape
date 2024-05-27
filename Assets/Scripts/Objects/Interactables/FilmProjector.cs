using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FilmProjector : BaseInteractiveObj
{
    [SerializeField] VideoPlayer videoPlayer;
    public VideoPlayer VideoPlayer { get => videoPlayer; set => videoPlayer = value; }
    [SerializeField]
    bool _haveBattery = false;
    public bool HaveBattery { get => _haveBattery; set => _haveBattery = value; }
    public override void Interact()
    {
        base.Interact();

        if (!_haveBattery)
        {
            for (int i = 0; i < Managers.Data.InventoryData.itemDatas.Count; i++)
            {
                if (Managers.Data.InventoryData.itemDatas[i].ItemType == DataDefine.EItemType.Battery)
                {
                    Managers.Data.InventoryData.itemDatas.RemoveAt(i);
                    ((UI_GameScene)Managers.Scene.CurrentScene.SceneUI).UI_Inventory.UpdateInventory();
                    _haveBattery = true;
                    videoPlayer.Play();
                    Managers.Sound.Play("Sounds/Objects/projector-activate-1");
                    break;
                }
            }
        }
        else
        {
            videoPlayer.Play();
            Managers.Sound.Play("Sounds/Objects/projector-activate-1");
        }
    }
       
    // Start is called before the first frame update
    void Start()
    {
        if(videoPlayer == null)
        {
            videoPlayer = GetComponentInChildren<VideoPlayer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
