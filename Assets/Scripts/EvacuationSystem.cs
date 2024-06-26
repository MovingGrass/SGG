using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EvacuationSystem : MonoBehaviour
{
    private EvacuationZone evacuationZone;

    private void Start()
    {
        evacuationZone = FindObjectOfType<EvacuationZone>();
        if (evacuationZone == null)
        {
            Debug.LogError("EvacuationZone not found in the scene!");
        }
    }

    public void AttemptEvacuation(GameObject item)
    {
        if (evacuationZone != null && evacuationZone.IsInEvacuationZone(item.transform.position))
        {
            EvacuateItem(item);
        }
    }

    private void EvacuateItem(GameObject item)
    {
        ItemValue itemValue = item.GetComponent<ItemValue>();
        if (itemValue != null)
        {
            ScoreManager.Instance.AddScore(itemValue.GetValue());
            Destroy(item);
        }
    }
}