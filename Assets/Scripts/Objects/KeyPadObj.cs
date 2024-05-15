using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadObj : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject interactableKeyPad;
    public GameObject InteractableKeyPad { get => interactableKeyPad; set => interactableKeyPad = value; }
    public void Interact()
    {
        if(interactableKeyPad != null)
        {
            interactableKeyPad.SetActive(true);
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
