using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class AddressableInstantiator : MonoBehaviour
{
    [SerializeField] private List<AssetReferenceGameObject> addressableObjects; 
    [SerializeField] private TMP_Dropdown objectDropdown; 

    private GameObject _instanceReference;

    private GameObject selectedPlatform;

    private void Update()
    {
        HandleObjectSelection();
    }

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
            _instanceReference = handle.Result;
        }
    }

    private void HandleObjectSelection()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null)
                {
                    selectedPlatform = hit.collider.gameObject; 
                    Debug.Log($"Selected: {selectedPlatform.name}");
                }
            }
        }
    }


    public void DeletePlatform()
    {
        if (selectedPlatform != null)
        {
            foreach (var obj in addressableObjects)
            {
                if (obj.RuntimeKeyIsValid() && obj.Asset != null)
                {
                    obj.ReleaseInstance(selectedPlatform);
                }
            }

            selectedPlatform = null;
        }
        else
        {
            Debug.LogWarning("No object selected to delete!");
        }
    }
}
