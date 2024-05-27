using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShowInteractText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text interactText;
    public TMP_Text InteractText { get => interactText; set => interactText = value; }

    public Text interactionText;
    public string text;
    private RaycastHit hit;
    private Ray ray;
    private GameObject lastInteractedObject;

    void Start()
    {
        if (interactText == null)
            interactionText = Managers.UI.ShowPopUpUI<UI_InteractionText>().InteractionText;
    }
    void Update()
    {
        ObjectHit();
    }
    void ObjectHit()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        bool hitSomething = Physics.Raycast(ray, out hit, 1f);

        if (hitSomething && hit.collider.gameObject.tag == "interactable")
        {
            if(interactionText != null)
            {
                interactionText.text = "F to interact";
                interactionText.transform.position = Camera.main.WorldToScreenPoint(hit.transform.position);
                interactionText.gameObject.SetActive(true);
            }

			if (lastInteractedObject != null)
				DisableOutline(lastInteractedObject);
			EnableOutline(hit.collider.gameObject);
            lastInteractedObject = hit.collider.gameObject;
            if(Input.GetKeyDown(KeyCode.F))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if(interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
		else if (hitSomething && hit.collider.gameObject.tag == "obtainable") {
			if (interactionText != null) {
				interactionText.text = "F to pick up";
				interactionText.transform.position = Camera.main.WorldToScreenPoint(hit.transform.position);
				interactionText.gameObject.SetActive(true);
			}

			if (lastInteractedObject != null)
				DisableOutline(lastInteractedObject);
			EnableOutline(hit.collider.gameObject);
			lastInteractedObject = hit.collider.gameObject;
			if (Input.GetKeyDown(KeyCode.F)) {
				IObtainable obtainable = hit.collider.GetComponent<IObtainable>();
				if (obtainable != null)
                {
                    obtainable.Obtain();
                    lastInteractedObject = null;
                }
			}
		}
        else
        {
            if (interactionText != null)
                interactionText.gameObject.SetActive(false);


            if (lastInteractedObject != null)
            {
                DisableOutline(lastInteractedObject);
                lastInteractedObject = null;
            }
        }
    }
    void EnableOutline(GameObject obj)
    {
        Outline outline = obj.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = true;
        }
    }

    void DisableOutline(GameObject obj)
    {
        Outline outline = obj.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }
    }
}
