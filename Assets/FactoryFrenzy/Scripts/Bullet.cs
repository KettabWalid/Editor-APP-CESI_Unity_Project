using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private float knockbackForce;
    private Rigidbody rb;

    // Initialize bullet components
    private void Start()
    {
        // Ensure Rigidbody is attached
        rb = GetComponent<Rigidbody>();

        // Disable gravity for the bullet if you don't want it to fall
        rb.useGravity = false;

        // Set collision detection to continuous for better collision handling
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Give the bullet an initial forward velocity
        rb.velocity = transform.forward * speed;
    }

    // Set knockback force when the bullet hits an object
    public void SetKnockbackForce(float force)
    {
        knockbackForce = force;
    }

    // When the bullet collides with an object (box or player)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the Rigidbody of the box to apply knockback
            var boxRb = collision.gameObject.GetComponent<Rigidbody>();
            if (boxRb != null)
            {
                // Apply knockback force to the box
                boxRb.AddForce(transform.forward * knockbackForce, ForceMode.Impulse);
            }
        }

        // Destroy the bullet after collision
        Destroy(gameObject);
    }

    // Debug for trigger-based solutions, if needed (only use if "Is Trigger" is checked)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var boxRb = other.GetComponent<Rigidbody>();
            if (boxRb != null)
            {
                boxRb.AddForce(transform.forward * knockbackForce, ForceMode.Impulse);
            }
            Destroy(gameObject);
        }
    }
}
