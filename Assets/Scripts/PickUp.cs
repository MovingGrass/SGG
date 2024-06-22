using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameObject objectTaken;
    private Transform originalParent;
    private bool isHoldingObject = false;

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
            // Position the object 1 unit in front of the player with y=0
            Vector3 dropPosition = transform.position + transform.forward * 1;
            dropPosition.y = 3.120279f;
            objectTaken.transform.parent = originalParent;
            objectTaken.transform.position = dropPosition;

            isHoldingObject = false;
            objectTaken = null;
        }
    }

    void HighlightObject(GameObject obj, bool highlight)
    {
        // Example of changing color to indicate highlight (requires a Renderer component)
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            if (highlight)
            {
                objRenderer.material.color = Color.yellow; // Change color to yellow
            }
            else
            {
                objRenderer.material.color = Color.white; // Revert to original color
            }
        }
    }
}
