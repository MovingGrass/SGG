using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Kecepatan gerakan pemain
    private Rigidbody rb;

    void Start()
    {
        // Mendapatkan komponen Rigidbody dari objek pemain
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Ambil input dari tombol WASD
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Buat vektor gerakan berdasarkan input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Hitung posisi baru menggunakan metode MovePosition
        Vector3 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;

        // Pindahkan pemain ke posisi baru
        rb.MovePosition(newPosition);

        if (movement != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(newRotation);
        }
    }
}

