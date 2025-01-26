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

    public EditorManager manager;
   [SerializeField] int selectedIndex;
    public void LoadSelectedPlatform()
    {
         selectedIndex = objectDropdown.value; // Get selected index from dropdown
        AssetReferenceGameObject selectedObject = addressableObjects[selectedIndex];

        selectedObject.InstantiateAsync().Completed += OnAddressableInstantiated; // Instantiate the selected platform
        
        //instantiatedPlatforms.Add(selectedObject);
    }

    public void CreatePOIOnScene(GameObject poi)
    {
        Transform camera = Camera.main.transform;
        
        manager.CreateNewPointOfInterest(poi, camera.position, camera.forward);
    }

    void OnAddressableInstantiated(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject instance = handle.Result;

            // Set position at startPos initially
            if (startPos != null)
            {

                instance.transform.position =new Vector3( instance.GetComponent<PointOfInterest>().PointOfInterestData.Position.x,
                0,
                1);
                instance.transform.rotation =Quaternion.identity;
            }

            instantiatedPlatforms.Add(instance);

            Debug.Log($"Platform instantiated: {instance.name}");

            Debug.Log(" index number " + instantiatedPlatforms.Count);
            CreatePOIOnScene(instantiatedPlatforms[instantiatedPlatforms.Count-1]);
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

