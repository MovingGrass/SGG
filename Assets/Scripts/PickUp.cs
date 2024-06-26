using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUp : MonoBehaviour
{
    private GameObject objectTaken;
    private Transform originalParent;
    private bool isHoldingObject = false;
    private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();

    private EvacuationSystem evacuationSystem;

    void Start()
    {
        // Find the EvacuationSystem in the scene
        evacuationSystem = FindObjectOfType<EvacuationSystem>();
        if (evacuationSystem == null)
        {
            Debug.LogError("EvacuationSystem not found in the scene!");
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

            // Attempt to evacuate the item using EvacuationSystem
            if (evacuationSystem != null)
            {
                evacuationSystem.AttemptEvacuation(objectTaken);
            }

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
}