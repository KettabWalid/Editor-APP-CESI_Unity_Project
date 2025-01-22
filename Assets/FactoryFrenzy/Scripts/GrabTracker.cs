using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabTracker : MonoBehaviour
{
    [SerializeField] private string snapTag = "Snap"; // Tag for snap points (cube colliders)
    private GameObject grabbedPlatform; // The currently grabbed platform
    private Transform grabbedSnapPoint; // The snap point on the grabbed platform

    private void OnEnable()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnGrab);
        print("grabbed");
        interactable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.RemoveListener(OnGrab);
        interactable.selectExited.RemoveListener(OnRelease);
        print("released");
    }

    // Called when a platform is grabbed
    private void OnGrab(SelectEnterEventArgs args)
    {
        grabbedPlatform = args.interactableObject.transform.gameObject;

        // Find the snap point (child with tag "Snap")
        grabbedSnapPoint = grabbedPlatform.transform.FindChildWithTag(snapTag);
        if (grabbedSnapPoint != null)
        {
            Debug.Log($"Grabbed platform: {grabbedPlatform.name}, SnapPoint: {grabbedSnapPoint.name}");
        }
        else
        {
            Debug.LogWarning("No snap point found on grabbed platform.");
        }
    }

    // Called when a platform is released
    private void OnRelease(SelectExitEventArgs args)
    {
        if (grabbedPlatform != null)
        {
            Debug.Log($"Released platform: {grabbedPlatform.name}");
            grabbedPlatform = null; // Clear the grabbed platform
            grabbedSnapPoint = null;
        }
    }

    // Detect collision with snap cubes and snap the grabbed platform
    private void OnCollisionEnter(Collision collision)
    {
        if (grabbedPlatform != null && collision.gameObject.CompareTag(snapTag))
        {
            // Get the snap point of the collided platform (the other platform's snap cube)
            Transform targetSnapPoint = collision.transform;

            if (targetSnapPoint != null && grabbedSnapPoint != null)
            {
                // Snap the grabbed platform to the target snap point's position and rotation
                grabbedSnapPoint.position = targetSnapPoint.position;
                print("Snapped");
                grabbedSnapPoint.rotation = targetSnapPoint.rotation;

                // Optionally, you could disable the grabbed platform's interaction once snapped
                grabbedPlatform.GetComponent<XRGrabInteractable>().enabled = false;
                Debug.Log($"Platform snapped to target at: {targetSnapPoint.position}");
            }
        }
    }
}

public static class TransformExtensions
{
    // Utility function to find a child by tag
    public static Transform FindChildWithTag(this Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child;
            }
        }
        return null;
    }
}
