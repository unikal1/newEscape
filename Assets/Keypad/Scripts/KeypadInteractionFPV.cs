using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad { 
    public class KeypadInteractionFPV : MonoBehaviour
    {
        private Camera cam;
        private void Awake() => cam = Camera.main;
        private void Update()
        {
            if(gameObject.activeInHierarchy == true)
            {
                var ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out var hit))
                    {
                        if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
                        {
                            if(keypadButton != null && keypadButton.gameObject.activeInHierarchy)
                                keypadButton.PressButton();
                        }
                    }
                }
             }
        }
    }
}