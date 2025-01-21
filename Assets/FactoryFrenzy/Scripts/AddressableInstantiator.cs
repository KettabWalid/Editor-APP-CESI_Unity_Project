using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class AddressableInstantiator : MonoBehaviour
{
    [SerializeField] private List<AssetReferenceGameObject> addressableObjects;
    [SerializeField] private TMP_Dropdown objectDropdown;

    private GameObject selectedPlatform;

    public void LoadSelectedPlatform()
    {
        int selectedIndex = objectDropdown.value;
        AssetReferenceGameObject selectedObject = addressableObjects[selectedIndex];

        selectedObject.InstantiateAsync().Completed += OnAddressableInstantiated;
    }

    void OnAddressableInstantiated(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject instance = handle.Result;

            // Ensure it has the GrabTracker for interaction
            if (instance.GetComponent<GrabTracker>() == null)
            {
                instance.AddComponent<GrabTracker>().Setup(this);
            }

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
            Destroy(selectedPlatform);
            Debug.Log("Selected platform deleted!");
            selectedPlatform = null;
        }
        else
        {
            Debug.LogWarning("No platform selected to delete!");
        }
    }
}
