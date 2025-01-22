using UnityEngine;

public class PlatformSnapper : MonoBehaviour
{
    // Reference to the parent platform of this snap point
    private Transform platform;

    private void Start()
    {
        // Get the parent platform of this snap point
        platform = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider has the "Snap" tag
        if (other.CompareTag("Snap"))
        {
            // Get the target snap point's transform
            Transform targetSnapPoint = other.transform;

            // Align the platforms based on snap points
            SnapToTarget(targetSnapPoint);
        }
    }

    private void SnapToTarget(Transform targetSnapPoint)
    {
        // Calculate the offset to align the snap points
        Vector3 offset = transform.position - platform.position;

        // Move the platform so its snap point aligns with the target snap point
        platform.position = targetSnapPoint.position - offset;

        // Align the rotation of the platform to the target snap point
        platform.rotation = targetSnapPoint.rotation;

        Debug.Log($"Platform snapped to {targetSnapPoint.name}");
    }
}
