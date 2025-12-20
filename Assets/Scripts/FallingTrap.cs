using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
    }

    // Detect player underneath
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasFallen) return;

        if (other.CompareTag("Player"))
        {
            hasFallen = true;

            // Enable falling
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f;
        }
    }

    // Kill player on hit
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Destroy(collision.gameObject);
        }
    }
}
