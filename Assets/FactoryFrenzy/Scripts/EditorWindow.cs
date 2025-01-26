using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static DataStructs;

public class EditorWindow : MonoBehaviour
{
    public EditorManager EditorManager;

    private PointOfInterestStruct _poiInfo;
    public TMP_InputField TitleInputField, ContentTextField;

    public void FillData(PointOfInterestStruct poiInfo)
    {
        this._poiInfo = poiInfo;
        TitleInputField.text = poiInfo.Title;
        ContentTextField.text = poiInfo.Description;
    }

    public void SavePOI()
    {
        _poiInfo.Title = TitleInputField.text;
        _poiInfo.Description = ContentTextField.text;

        EditorManager.SavePointOfInterest(_poiInfo);

        print(_poiInfo.Title + ", " + _poiInfo.Description + ", " + _poiInfo.Position + ", " + _poiInfo.Id + ", SAVED");
    }

    public void DeletePOI()
    {
        EditorManager.DeletePointOfInterest(_poiInfo.Id);
        print(_poiInfo.Id + " Deleted");
    }
}
