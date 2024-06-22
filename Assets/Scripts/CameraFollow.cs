using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform; // Reference to the player's transform
    public Vector3 offset; // Offset for the camera position

    void LateUpdate()
    {
        // Ensure the playerTransform is assigned
        if (playerTransform != null)
        {
            // Set the camera's position based on the player's position and the offset
            Vector3 newPosition = playerTransform.position + offset;
            transform.position = newPosition;
        }
    }
}
