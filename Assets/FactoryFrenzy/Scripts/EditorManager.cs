using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DataStructs;

public class EditorManager : MonoBehaviour
{
    public PointOfInterest PointOfInterestPrefab;
    List<PointOfInterest> pointOfInterestList = new List<PointOfInterest>();
    int _currentID = 0;

    public void CreateNewPointOfInterest(Vector3 playerPos, Vector3 playerRotation)
    {
        var poiPosition = playerPos + (playerRotation);
        poiPosition.y = 0.0f;
        PositionStruct posStruct = new PositionStruct(poiPosition.x, poiPosition.z);
        var poi = Instantiate(PointOfInterestPrefab, poiPosition, PointOfInterestPrefab.transform.rotation);
        poi.InitializePOI(_currentID++, posStruct);
        pointOfInterestList.Add(poi);
    }

    public void DeletePointOfInterest(int id)
    {
        var poi = GetPointOfInterestById(id);
        pointOfInterestList.Remove(poi);
        GameObject.Destroy(poi.gameObject);
    }

    public void SavePointOfInterest(PointOfInterestStruct poiData)
    {
        var poi = GetPointOfInterestById(poiData.Id);
        poi.PointOfInterestData = poiData;
    }

    protected PointOfInterest GetPointOfInterestById(int id)
    {
        return pointOfInterestList.Where(x => x.PointOfInterestData.Id == id).First();
    }

}
