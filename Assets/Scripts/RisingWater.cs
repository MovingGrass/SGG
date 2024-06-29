using UnityEngine;

public class RisingWater : MonoBehaviour
{
    public float riseSpeed = 1f; // Speed at which the water rises
    public float maxHeight = 5f; // Maximum height the water will rise to

    void Update()
    {
        // Check if the water has reached its maximum height
        if (transform.position.y < maxHeight)
        {
            // Move the water upwards
            transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);
        }
    }
}