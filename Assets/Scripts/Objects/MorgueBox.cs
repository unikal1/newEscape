using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorgueBox : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public bool IsOpoen => isOpen;
    private bool isOpen = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        anim.SetBool("isOpen", isOpen);
    }

    public void Open()
    {
        isOpen = true;
        anim.SetBool("isOpen", isOpen);
    }
    public void Close()
    {
        isOpen = false;
        anim.SetBool("isOpen", isOpen);
    }
}
