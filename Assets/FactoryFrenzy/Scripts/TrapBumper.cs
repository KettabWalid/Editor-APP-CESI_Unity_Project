using UnityEngine;

public class TrapBumper : MonoBehaviour
{
    [SerializeField] private float pushForce = 3f; // Adjust force strength in the Inspector

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collider belongs to the box (or player)
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                // Calculate the push direction (away from the bumper)
                Vector3 pushDirection = (collision.transform.position - transform.position).normalized;

                // Apply force to the player's Rigidbody
                playerRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);

                Debug.Log("Player hit the bumper and was pushed back!");
                //Debug.DrawRay(transform.position, pushDirection * 2, Color.red, 1f);
            }

        }

    }
}
