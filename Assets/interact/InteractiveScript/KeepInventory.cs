using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInventory : MonoBehaviour, IObtainable
{

    public void Obtain()
    {
        Destroy(gameObject);
    }
}
