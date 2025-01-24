using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static DataStructs;

public class EditorWindow : MonoBehaviour
{
    public EditorManager EditorManager;

    private PointOfInterestStruct _poiInfo;

    public void SavePOI()
    {
        _poiInfo.Title = "Default Title";
        _poiInfo.Description = "Default Description";

        EditorManager.SavePointOfInterest(_poiInfo);
    }

    public void DeletePOI()
    {
        EditorManager.DeletePointOfInterest(_poiInfo.Id);
    }
}
