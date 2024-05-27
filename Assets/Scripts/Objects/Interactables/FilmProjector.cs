using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FilmProjector : BaseInteractiveObj {
	[SerializeField] VideoPlayer videoPlayer;
	public VideoPlayer VideoPlayer { get => videoPlayer; set => videoPlayer = value; }
	[SerializeField] bool _haveBattery = false;
	[SerializeField] Canvas videoCanvas;
	[SerializeField] Light spotLight;
	public bool HaveBattery { get => _haveBattery; set => _haveBattery = value; }

	public override void Interact() {
		base.Interact();

		if (!_haveBattery) {
			Managers.Sound.Play("Sounds/Objects/projector-button-click-1");
			for (int i = 0; i < Managers.Data.InventoryData.itemDatas.Count; i++) {
				if (Managers.Data.InventoryData.itemDatas[i].ItemType == DataDefine.EItemType.Battery) {
					Managers.Data.InventoryData.itemDatas.RemoveAt(i);
					((UI_GameScene)Managers.Scene.CurrentScene.SceneUI).UI_Inventory.UpdateInventory();
					_haveBattery = true;
					videoCanvas.enabled = true;
					Managers.Sound.Play("Sounds/Objects/projector-activate-1");
					StartCoroutine(PlayVideoCoroutine());
					break;
				}
			}
		} else {
			Managers.Sound.Play("Sounds/Objects/projector-button-click-1");
			Managers.Sound.Play("Sounds/Objects/projector-activate-1");
			StartCoroutine(PlayVideoCoroutine());
		}
	}

	void Start() {
		if (videoPlayer == null)
			videoPlayer = GetComponentInChildren<VideoPlayer>();
		videoPlayer.playOnAwake = false;
		videoPlayer.Stop();
		spotLight.enabled = false;
		videoCanvas.enabled = false;
	}

	IEnumerator PlayVideoCoroutine() {
		gameObject.tag = "unInteractable";
		spotLight.enabled = true;
		videoCanvas.enabled = true;
		videoPlayer.Play();
		yield return new WaitForSeconds((float)videoPlayer.clip.length);
		videoPlayer.Stop();
		videoCanvas.enabled = false;
		spotLight.enabled = false;
		gameObject.tag = "interactable";
	}
}
