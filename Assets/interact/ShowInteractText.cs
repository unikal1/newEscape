using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    // Start is called before the first frame update

    // Update is called once per frame
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
            Debug.Log("watch out");
            if(interactionText != null)
            {
                interactionText.text = "F to interact";
                interactionText.transform.position = Camera.main.WorldToScreenPoint(hit.transform.position);
                interactionText.gameObject.SetActive(true);
            }
            else
            {
                InteractText.transform.position = Camera.main.WorldToScreenPoint(hit.transform.position);
                InteractText.gameObject.SetActive(true);
            }


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
			} else {
				InteractText.transform.position = Camera.main.WorldToScreenPoint(hit.transform.position);
				InteractText.gameObject.SetActive(true);
			}


			EnableOutline(hit.collider.gameObject);
			lastInteractedObject = hit.collider.gameObject;
			if (Input.GetKeyDown(KeyCode.F)) {
				IObtainable obtainable = hit.collider.GetComponent<IObtainable>();
				if (obtainable != null) {
					obtainable.Obtain();
				}
			}
		}
        else
        {
            if (interactionText != null)
                interactionText.gameObject.SetActive(false);
            else
                InteractText.gameObject.SetActive(false);

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
