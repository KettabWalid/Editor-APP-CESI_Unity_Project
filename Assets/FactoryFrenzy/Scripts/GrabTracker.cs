using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabTracker : MonoBehaviour
{
    private AddressableInstantiator instantiator;

    // Link to the AddressableInstantiator script
    public void Setup(AddressableInstantiator parentInstantiator)
    {
        instantiator = parentInstantiator;
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        // Subscribe to grab events
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Check if the object has the "Platform" tag
        if (gameObject.CompareTag("Platform"))
        {
            if (instantiator != null)
            {
                instantiator.SetSelectedPlatform(gameObject);
                Debug.Log($"Platform grabbed: {gameObject.name}");
            }
        }
        else
        {
            Debug.LogWarning($"Grabbed object {gameObject.name} does not have the 'Platform' tag.");
        }
    }
}
