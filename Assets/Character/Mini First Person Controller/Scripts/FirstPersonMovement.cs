using NavKeypad;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
	public bool canMove = true;

	[Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    [SerializeField]
    private Keypad keypad;
    public Keypad Keypad { get => keypad; set => keypad = value; }

    [SerializeField]
    private Light keyPadLight;
    public Light KeyPadLight { get => keyPadLight; set => keyPadLight = value; }

    [SerializeField]
    private bool isUsingItem;
    public bool IsUsingItem { get => isUsingItem; set => isUsingItem = value; }
    void Start()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();

        if(Managers.Input.KeyActions.ContainsKey(KeyCode.Alpha1))
            Managers.Input.KeyActions.Remove(KeyCode.Alpha1);
        Managers.Input.KeyActions.Add(KeyCode.Alpha1, On1KeyDown);

        if(Managers.Input.KeyActions.ContainsKey(KeyCode.Alpha2))
            Managers.Input.KeyActions.Remove(KeyCode.Alpha2);
        Managers.Input.KeyActions.Add(KeyCode.Alpha2, On2KeyDown);

        if(Managers.Input.KeyActions.ContainsKey(KeyCode.Alpha3))
            Managers.Input.KeyActions.Remove(KeyCode.Alpha3);
        Managers.Input.KeyActions.Add(KeyCode.Alpha3, On3KeyDown);

        if(Managers.Input.KeyActions.ContainsKey(KeyCode.Alpha4))
            Managers.Input.KeyActions.Remove(KeyCode.Alpha4);
        Managers.Input.KeyActions.Add(KeyCode.Alpha4, On4KeyDown);

        if(Managers.Input.KeyActions.ContainsKey(KeyCode.E))
            Managers.Input.KeyActions.Remove(KeyCode.E);
        Managers.Input.KeyActions.Add(KeyCode.E, OnEKeyDown);

        if(Managers.Input.KeyActions.ContainsKey(KeyCode.Escape))
            Managers.Input.KeyActions.Remove(KeyCode.Escape);
        Managers.Input.KeyActions.Add(KeyCode.Escape, OnESCDown);
    }

    void FixedUpdate()
    {
        var interactUI = FindObjectOfType<UI_ItemUse>();
        if (interactUI != null)
        {
            return;
        }

        // Update IsRunning from input.
        IsRunning = canMove && canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }

    public void On1KeyDown()
    {
        var interactUI = FindObjectOfType<UI_ItemUse>();
        if (interactUI != null)
        {
            return;
        }
        if (((UI_GameScene)Managers.Scene.CurrentScene.SceneUI).UI_Inventory.UI_Slots[0].ItemData != null)
        {
            ((UI_GameScene)Managers.Scene.CurrentScene.SceneUI).UI_Inventory.SelectSlot(0);
        }
    }
    public void On2KeyDown()
    {
        var interactUI = FindObjectOfType<UI_ItemUse>();
        if (interactUI != null)
        {
            return;
        }
        if (((UI_GameScene)Managers.Scene.CurrentScene.SceneUI).UI_Inventory.UI_Slots[1].ItemData != null)
        {
            ((UI_GameScene)Managers.Scene.CurrentScene.SceneUI).UI_Inventory.SelectSlot(1);
        }
    }
    private void On3KeyDown()
    {
        var interactUI = FindObjectOfType<UI_ItemUse>();
        if (interactUI != null)
        {
            return;
        }
        if (((UI_GameScene)Managers.Scene.CurrentScene.SceneUI).UI_Inventory.UI_Slots[2].ItemData != null)
        {
            ((UI_GameScene)Managers.Scene.CurrentScene.SceneUI).UI_Inventory.SelectSlot(2);
        }
    }
    public void On4KeyDown()
    {
        var interactUI = FindObjectOfType<UI_ItemUse>();
        if (interactUI != null)
        {
            return;
        }
        if (((UI_GameScene)Managers.Scene.CurrentScene.SceneUI).UI_Inventory.UI_Slots[3].ItemData != null)
        {
            ((UI_GameScene)Managers.Scene.CurrentScene.SceneUI).UI_Inventory.SelectSlot(3);
        }
    }
    public void OnEKeyDown()
    {
        var interactUI = FindObjectOfType<UI_ItemUse>();
        if(interactUI != null)
        {
            return;
        }
        ((UI_GameScene)Managers.Scene.CurrentScene.SceneUI).UI_Inventory.UseItem();
        // TODO : 아이템 사용 구현하면 됨
    }

    public void OnESCDown()
    {
        if (Keypad != null)
        {
            if (Keypad.gameObject.activeSelf)
                Keypad.gameObject.SetActive(false);
        }
        if(KeyPadLight != null)
        {
            if (KeyPadLight.gameObject.activeSelf)
                KeyPadLight.gameObject.SetActive(false);
        }
        
        var interactUI = FindObjectOfType<UI_ItemUse>();
        if(interactUI != null)
            Managers.UI.ClosePopUpUI(interactUI);

        var fpsLook = FindObjectOfType<FirstPersonLook>();
        if (fpsLook.IsInteracting)
        {
            fpsLook.IsInteracting = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}