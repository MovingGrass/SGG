using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    
    [SerializeField] private float moveSoundCooldown = 0.5f;
    private float lastMoveSoundTime;
    private bool isMoving = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check for movement input
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        isMoving = (moveHorizontal != 0 || moveVertical != 0);

        // Play movement sound
        if (!isMoving && Time.time > lastMoveSoundTime + moveSoundCooldown)
        {
            AudioManager.Instance.PlayMoveSound();
            lastMoveSoundTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        // Movement logic
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Vector3 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);

        if (movement != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(newRotation);
        }
    }
}

