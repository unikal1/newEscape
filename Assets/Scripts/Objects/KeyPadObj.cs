using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavKeypad;

public class KeyPadObj : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Keypad interactableKeyPad;
    public Keypad InteractableKeyPad { get => interactableKeyPad; set => interactableKeyPad = value; }
    [SerializeField]
    private Light keyPadLight;
    public Light KeyPadLight { get => keyPadLight; set => keyPadLight = value; }
    public void Interact()
    {
        if(InteractableKeyPad == null)
            InteractableKeyPad = FindAnyObjectByType<Keypad>();
        if(InteractableKeyPad != null)
        {
            keyPadLight.gameObject.SetActive(true);
            InteractableKeyPad.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            var fpsLook = FindObjectOfType<FirstPersonLook>();
            fpsLook.IsInteracting = true;
        }
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
