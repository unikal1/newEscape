using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInventory : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        Destroy(gameObject);
    }
}
