using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PickUp : MonoBehaviour
{
    private GameObject objectTaken;
    private Transform originalParent;
    private bool isHoldingObject = false;
    private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();

    private EvacuationSystem evacuationSystem;

    [SerializeField] private TextMeshProUGUI actionText;

    void Start()
    {
        evacuationSystem = FindObjectOfType<EvacuationSystem>();
        if (evacuationSystem == null)
        {
            Debug.LogError("EvacuationSystem not found in the scene!");
        }

        if (actionText == null)
        {
            Debug.LogError("Action Text not assigned in the inspector!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHoldingObject)
            {
                DropObject();
            }
            else if (objectTaken != null)
            {
                PickUpObject();
            }
        }

        UpdateActionText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("canBeTaken") && !isHoldingObject)
        {
            objectTaken = other.gameObject;
            HighlightObject(objectTaken, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectTaken)
        {
            HighlightObject(objectTaken, false);
            objectTaken = null;
        }
    }

    void PickUpObject()
    {
        if (objectTaken != null)
        {
            originalParent = objectTaken.transform.parent;
            objectTaken.transform.parent = transform;
            objectTaken.transform.localPosition = new Vector3(0, 2, 0);
            isHoldingObject = true;
            AudioManager.Instance.PlayPickupSound();
        }
    }

    void DropObject()
    {
        if (objectTaken != null)
        {
            Vector3 dropPosition = transform.position + transform.forward * 1;
            dropPosition.y = transform.position.y;
            objectTaken.transform.parent = originalParent;
            objectTaken.transform.position = dropPosition;

            isHoldingObject = false;
            HighlightObject(objectTaken, false);

            if (evacuationSystem != null)
            {
                evacuationSystem.AttemptEvacuation(objectTaken);
            }

            AudioManager.Instance.PlayDropSound();
            objectTaken = null;
        }
    }

    void HighlightObject(GameObject obj, bool highlight)
    {
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            if (highlight)
            {
                if (!originalColors.ContainsKey(obj))
                {
                    originalColors[obj] = objRenderer.material.color;
                }
                objRenderer.material.color = Color.yellow;
            }
            else
            {
                if (originalColors.ContainsKey(obj))
                {
                    objRenderer.material.color = originalColors[obj];
                    originalColors.Remove(obj);
                }
                else
                {
                    objRenderer.material.color = Color.white;
                }
            }
        }
    }

    void UpdateActionText()
    {
        if (actionText != null)
        {
            if (isHoldingObject)
            {
                actionText.text = "[E] Drop";
            }
            else if (objectTaken != null)
            {
                actionText.text = "[E] Pick up";
            }
            else
            {
                actionText.text = "";
            }
        }
    }
}