using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataStructs;

public class PointOfInterest : MonoBehaviour
{
    public PointOfInterestStruct _pointOfInterestData;
    public PointOfInterestStruct PointOfInterestData
    {
        get { return _pointOfInterestData; }
        set
        {
            _pointOfInterestData = value;
            SetGameObjectPositionByPOI();
        }
    }

    public void InitializePOI(int id, PositionStruct position)
    {
        PointOfInterestData = new PointOfInterestStruct(id, position);
        Debug.Log($"POI initialized with ID: {id}");
    }

    public void SetGameObjectPositionByPOI()
    {
        var position = _pointOfInterestData.Position;
        this.transform.position = new Vector3(position.x, 1f, position.y);
    }

}

