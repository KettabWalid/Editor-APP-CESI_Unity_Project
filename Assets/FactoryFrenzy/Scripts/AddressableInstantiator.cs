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
    public List<GameObject> instantiatedPlatforms = new List<GameObject>(); // Track all instantiated platforms

    public void LoadSelectedPlatform()
    {
        int selectedIndex = objectDropdown.value; // Get selected index from dropdown
        AssetReferenceGameObject selectedObject = addressableObjects[selectedIndex];

        selectedObject.InstantiateAsync().Completed += OnAddressableInstantiated; // Instantiate the selected platform

        //instantiatedPlatforms.Add(selectedObject);
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

            instantiatedPlatforms.Add(instance);

            Debug.Log($"Platform instantiated: {instance.name}");
        }
    }

    public void SetSelectedPlatform(GameObject platform)
    {
        selectedPlatform = platform;
        Debug.Log($"Selected Platform: {selectedPlatform.name}");
    }

    //public void DeletePlatform()
    //{
    //    if (selectedPlatform != null)
    //    {
    //        instantiatedPlatforms.Remove(selectedPlatform); // Remove from list
    //        Destroy(selectedPlatform);
    //        Debug.Log("Selected platform deleted!");
    //        selectedPlatform = null;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No platform selected to delete!");
    //    }
    //}

}

