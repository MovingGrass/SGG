using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemValue : MonoBehaviour
{
    [SerializeField] private int value;

    public int GetValue()
    {
        return value;
    }
}
