using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class AddressableInstantiator : MonoBehaviour
{
    [SerializeField] private List<AssetReferenceGameObject> addressableObjects; // List of addressable objects
    [SerializeField] private TMP_Dropdown objectDropdown; // Dropdown for platform selection
    public Transform startPos; // Starting position transform
    public float snapRadius = 2.0f; // Distance within which snapping can occur

    private GameObject selectedPlatform; // Reference to the selected platform
    private List<GameObject> instantiatedPlatforms = new List<GameObject>(); // Track all instantiated platforms

    public void LoadSelectedPlatform()
    {
        int selectedIndex = objectDropdown.value; // Get selected index from dropdown
        AssetReferenceGameObject selectedObject = addressableObjects[selectedIndex];

        selectedObject.InstantiateAsync().Completed += OnAddressableInstantiated; // Instantiate the selected platform
    }

    void OnAddressableInstantiated(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject instance = handle.Result;

            // Set position at startPos initially
            if (startPos != null)
            {
                instance.transform.position = startPos.position;
                instance.transform.rotation = startPos.rotation;
            }

            //// Ensure it has the GrabTracker for interaction
            //if (instance.GetComponent<GrabTracker>() == null)
            //{
            //    instance.AddComponent<GrabTracker>().OnEnable(this);
            //}

            // Add to the list of instantiated platforms
            instantiatedPlatforms.Add(instance);

            Debug.Log($"Platform instantiated: {instance.name}");
        }
    }

    public void SetSelectedPlatform(GameObject platform)
    {
        selectedPlatform = platform;
        Debug.Log($"Selected Platform: {selectedPlatform.name}");
    }

    public void DeletePlatform()
    {
        if (selectedPlatform != null)
        {
            instantiatedPlatforms.Remove(selectedPlatform); // Remove from list
            Destroy(selectedPlatform);
            Debug.Log("Selected platform deleted!");
            selectedPlatform = null;
        }
        else
        {
            Debug.LogWarning("No platform selected to delete!");
        }
    }

    //public void SnapPlatform(GameObject grabbedPlatform)
    //{
    //    if (grabbedPlatform == null)
    //    {
    //        Debug.LogWarning("No grabbed platform provided for snapping.");
    //        return;
    //    }

    //    // Find the closest platform with the "Platforme" tag to snap to
    //    GameObject closestPlatform = null;
    //    float closestDistance = float.MaxValue;

    //    foreach (GameObject platform in instantiatedPlatforms)
    //    {
    //        if (platform == grabbedPlatform || !platform.CompareTag("Platforme")) continue; // Skip the grabbed platform or unrelated objects

    //        float distance = Vector3.Distance(grabbedPlatform.transform.position, platform.transform.position);
    //        if (distance < snapRadius && distance < closestDistance)
    //        {
    //            closestPlatform = platform;
    //            closestDistance = distance;
    //        }
    //    }

    //    if (closestPlatform != null)
    //    {
    //        // Calculate the snap position relative to the closest platform
    //        Vector3 snapOffset = new Vector3(
    //            closestPlatform.transform.localScale.x / 2 + grabbedPlatform.transform.localScale.x / 2,
    //            0,
    //            0); // Adjust based on your platform orientation and desired snapping

    //        Vector3 snapPosition = closestPlatform.transform.position + snapOffset;

    //        // Snap the grabbed platform to the calculated position
    //        grabbedPlatform.transform.position = snapPosition;

    //        Debug.Log($"Platform snapped to: {closestPlatform.name} at {snapPosition}");
    //    }
    //    else
    //    {
    //        Debug.Log("No nearby platform to snap to within the radius.");
    //    }
    //}

}

