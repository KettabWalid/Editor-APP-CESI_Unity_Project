using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static DataStructs;

public class EditorManager : MonoBehaviour
{
    //public PointOfInterest PointOfInterestPrefab;

    List<PointOfInterest> pointOfInterestList = new List<PointOfInterest>();
    int _currentID = 0;

    private PointOfInterestStruct _poiInfo;
    //public AddressableInstantiator Instantiator;

    private string path;
    private const string ConfigFileName = "SceneData.json"; // File name for saving/loading scene data

    private void Start()
    {
        path = Application.streamingAssetsPath + "/" + ConfigFileName;
        LoadScene();
    }


    public void CreateNewPointOfInterest(GameObject GOpointOfInterestPrefab, Vector3 playerPos, Vector3 playerRotation)
    {
        // Calculate the new position
        var poiPosition = playerPos + playerRotation;
        poiPosition.y = 1f;

        PositionStruct posStruct = new PositionStruct(poiPosition.x, poiPosition.z);

        // Initialize the new POI with a unique ID
        GOpointOfInterestPrefab.GetComponent<PointOfInterest>().InitializePOI(_currentID++, posStruct);

        // Add the new POI to the list
        pointOfInterestList.Add(GOpointOfInterestPrefab.GetComponent<PointOfInterest>());

        // Update the EditorWindow with the new POI data
        var editorWindow = FindObjectOfType<EditorWindow>();
        if (editorWindow != null)
        {
            editorWindow.FillData(GOpointOfInterestPrefab.GetComponent<PointOfInterest>().PointOfInterestData);
        }

        Debug.Log($"New POI created with ID {_currentID - 1}");



        //  //PointOfInterestPrefab = pointOfInterestPrefab;
        //  var poiPosition = playerPos + (playerRotation);
        //  poiPosition.y = 1f;
        //  Transform camera = Camera.main.transform;
        ////  PositionStruct posStruct = new PositionStruct(camera.position.x, camera.position.y);
        //  PositionStruct posStruct = new PositionStruct(poiPosition.x,poiPosition.z);
        //  //var poi = Instantiate(GOpointOfInterestPrefab, poiPosition, GOpointOfInterestPrefab.transform.rotation);
        //  GOpointOfInterestPrefab.GetComponent<PointOfInterest>().InitializePOI(_currentID++, posStruct);
        //  pointOfInterestList.Add(GOpointOfInterestPrefab.GetComponent<PointOfInterest>());
        //  print((pointOfInterestList.Count-1) + ":Last Object name" + " " + pointOfInterestList[pointOfInterestList.Count-1].gameObject.name);
        //  Debug.Log(GOpointOfInterestPrefab.transform.eulerAngles);
        //  Debug.Log(GOpointOfInterestPrefab.transform.position);



    }

    public void DeletePointOfInterest(int id)
    {
        var poi = GetPointOfInterestById(id);

        if (poi != null)
        {
            pointOfInterestList.Remove(poi);
            GameObject.Destroy(poi.gameObject);
            Debug.Log($"POI with ID {id} deleted successfully.");

            // Clear or reset the EditorWindow’s POI data
            var editorWindow = FindObjectOfType<EditorWindow>();
            if (editorWindow != null)
            {
                editorWindow.FillData(new PointOfInterestStruct(-1, new PositionStruct(0, 0), "", ""));
            }
        }
        else
        {
            Debug.LogWarning($"POI with ID {id} not found.");
        }

        //////////////////////////////////////2
        //var poi = GetPointOfInterestById(id);

        //if (poi != null) // Check if the POI exists
        //{
        //    pointOfInterestList.Remove(poi);
        //    GameObject.Destroy(poi.gameObject);
        //    Debug.Log($"POI with ID {id} deleted successfully.");
        //}
        //else
        //{
        //    Debug.LogWarning($"POI with ID {id} not found.");
        //}

        ///////////////////////////////////////1
        //var poi = GetPointOfInterestById(id);
        //pointOfInterestList.Remove(poi);
        //GameObject.Destroy(poi.gameObject);
    }

    public void SavePointOfInterest(PointOfInterestStruct poiData)
    {


        ///////////////////////////////////////////////3
        var poi = GetPointOfInterestById(poiData.Id);
        if (poi != null)
        {
            // Update the POI's position to its current transform position
            PositionStruct updatedPosition = new PositionStruct(poi.transform.position.x, poi.transform.position.z);

            // Update the POI data with the new position, title, and description
            poi.PointOfInterestData = new PointOfInterestStruct(
                poiData.Id,
                updatedPosition,
                poiData.Title,
                poiData.Description
            );

            Debug.Log($"POI with ID {poiData.Id} saved. New position: {updatedPosition.x}, {updatedPosition.y}");
        }
        else
        {
            Debug.LogWarning($"POI with ID {poiData.Id} not found. Save failed.");
        }




        //////////////////////////////////////////////////2
        //var poi = GetPointOfInterestById(poiData.Id);

        //if (poi != null) // Check if the POI exists
        //{
        //    poi.PointOfInterestData = poiData;
        //    UpdatePoiDATA();
        //    Debug.Log($"POI with ID {poiData.Id} saved successfully.");
        //}
        //else
        //{
        //    Debug.LogWarning($"POI with ID {poiData.Id} not found.");
        //}


        ///////////////////////////////////////////////1
        //var poi = GetPointOfInterestById(poiData.Id);
        //poi.PointOfInterestData = poiData;

    }


    protected PointOfInterest GetPointOfInterestById(int id)
    {
        return pointOfInterestList.FirstOrDefault(x => x.PointOfInterestData.Id == id);
        //return pointOfInterestList.Where(x => x.PointOfInterestData.Id == id).First();
    }
    public void UpdatePoiDATA()
    {
        foreach(var poi in pointOfInterestList)
        {
            poi._pointOfInterestData = new PointOfInterestStruct(poi._pointOfInterestData.Id, new PositionStruct(poi.transform.position.x, poi.transform.position.y), poi._pointOfInterestData.Title, poi._pointOfInterestData.Description);
        }
    }

    public void LoadScene()
    {
        if (!File.Exists(path))
        {
            Debug.LogWarning($"File not found at path: {path}");
            return;
        }

        using (StreamReader file = File.OpenText(path))
        {
            JsonSerializer serializer = new JsonSerializer();
            List<PointOfInterestStruct> poiList = (List<PointOfInterestStruct>)serializer.Deserialize(file, typeof(List<PointOfInterestStruct>));
            if (poiList == null || poiList.Count == 0)
            {
                Debug.LogWarning("No POIs found in saved data.");
                return;
            }

            foreach (var pointOfInterest in poiList)
            {
                // Instantiate a new PointOfInterest GameObject here instead of using prefab
                GameObject poiObject = new GameObject("PointOfInterest");
                PointOfInterest poi = poiObject.AddComponent<PointOfInterest>();

                poi.PointOfInterestData = pointOfInterest;
                pointOfInterestList.Add(poi);

                // Set the POI's position based on the saved data
                poiObject.transform.position = new Vector3(pointOfInterest.Position.x, 1f, pointOfInterest.Position.y); // Ensure it's above the floor

                Debug.Log($"Loaded POI with ID: {pointOfInterest.Id}, Position: ({pointOfInterest.Position.x}, {pointOfInterest.Position.y})");
            }
        }
    }


    public void SaveScene()
    {
        var poiStructList = new List<PointOfInterestStruct>();
        pointOfInterestList.ForEach(x => poiStructList.Add(x.PointOfInterestData));

        string json = JsonConvert.SerializeObject(poiStructList, Formatting.Indented);
        using (StreamWriter file = new StreamWriter(path))
        {
            file.Write(json);
        }

        Debug.Log("Scene saved successfully.");
    }
}
