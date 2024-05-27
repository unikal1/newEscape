using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseFlashlight : MonoBehaviour
{
	[SerializeField] GameObject light;
	[SerializeField] List<AudioClip> turnOnSounds;
	[SerializeField] List<AudioClip> turnOffSounds;

	void Start()
    {
        
    }

    // Update is called once per frame
/*    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
		{
			light.enabled = !light.enabled;
		}
    }*/

	public void Use() {
		if (light.activeSelf) {
			light.SetActive(false);
			PlayTurnOffSound();
		} else {
			light.SetActive(true);
			PlayTurnOnSound();
		}
	}

	void PlayTurnOnSound() {
		if (turnOnSounds.Count > 0) {
			AudioClip clip = turnOnSounds[Random.Range(0, turnOnSounds.Count)];
			Managers.Sound.Play(clip);
		}
	}

	void PlayTurnOffSound() {
		if (turnOffSounds.Count > 0) {
			AudioClip clip = turnOffSounds[Random.Range(0, turnOffSounds.Count)];
			Managers.Sound.Play(clip);
		}
	}
}
