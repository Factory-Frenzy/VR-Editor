using System;
using System.Collections.Generic;
using System.IO;
using MapObject;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Formatting = Newtonsoft.Json.Formatting;
using Object = System.Object;

public class ExportJson : MonoBehaviour
{
    [SerializeField] public bool http;
    
    [SerializeField] 
    public string httpUploadPath = "http://10.191.92.139:3000/upload";

    private readonly JsonSerializerSettings jsonSettings = new()
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        Formatting = Formatting.Indented
    };
    
    public void SaveMap(String mapName)
    {
        // Récupère tous les GameObjects avec le tag "MapObject"
        GameObject[] mapObjects = GameObject.FindGameObjectsWithTag("MapObject");

        // Liste pour stocker les informations sur les objets
        List<MapObjectData> mapObjectDataList = new List<MapObjectData>();

        // Récupère les informations pour chaque objet
        foreach (GameObject mapObject in mapObjects)
        {
            MapObjectData mapObjectData = new MapObjectData
            {
                prefabName = mapObject.name.Replace("(Clone)", ""),
                type = ObjectType.Undefined,
                dynamic = false,
                speed = null,
                position = mapObject.transform.position,
                rotation = mapObject.transform.rotation,
            };
            if (mapObjectData.prefabName == "Platform Move 520")
            {
                MovePlatform movePlatform = mapObject.GetComponentInChildren<MovePlatform>();
                
                print("movePlatform " + mapObject.name);
                
                mapObjectData.dynamic = true;
                mapObjectData.speed = movePlatform.Speed;
                mapObjectData.endpoints = new ObjectEndpoints
                {
                    a = movePlatform.EndPointA.position,
                    b = movePlatform.EndPointB.position,
                };
            }

            if (mapObjectData.prefabName.Contains("Platform"))
            {
                mapObjectData.type = ObjectType.Platform;
            }
            if (mapObjectData.prefabName.Contains("Trap"))
            {
                mapObjectData.type = ObjectType.Trap;
            }

            mapObjectDataList.Add(mapObjectData);
        }

        Map map = new Map
        {
            name = mapName,
            objectData = mapObjectDataList,
        };

        string fileName = mapName + ".json";
        //string fileJson = CreateJson(map);

        if (http)
        {
            SaveWithHttp(fileName, map);
        }
        else
        {
            SaveToDisk(fileName, map);
        }
    }

    private string CreateJson(Object obj)
    {
        return JsonConvert.SerializeObject(obj, jsonSettings);
    }

    public bool CheckMapExist(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
        return File.Exists(filePath);
    }

    private void SaveToDisk(string fileName, Map map)
    {
        // Chemin complet du fichier
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        string json = CreateJson(map);
        
        // Écrit le JSON dans le fichier
        File.WriteAllText(filePath, json);

        Debug.Log("Exportation terminée. Données enregistrées dans " + filePath);
    }

    private void SaveWithHttp(string fileName, Map map)
    {
        
        // Création d'un objet contenant les données à envoyer
        var requestData = new
        {
            fileName,
            fileContent = map,
        };
        string json = CreateJson(requestData);

        // Envoi des données au serveur
        UnityWebRequest request = UnityWebRequest.Post(httpUploadPath, json, "application/json");
        //request.SetRequestHeader("Content-Type", "application/json");
        
        // Envoi de la requête
        request.SendWebRequest();

        while (!request.isDone)
        {
            // Attendez que la requête soit terminée
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Erreur lors de l'envoi des données au serveur : " + request.error);
        }
        else
        {
            Debug.Log("Exportation terminée. Données envoyées au serveur.");
        }
    }

    [Serializable]
    public class MapObjectData
    {
        public string prefabName;
        public ObjectType type;
        public bool dynamic;
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public float? speed;
        public Vector3 position;
        public Quaternion rotation;
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public ObjectEndpoints endpoints;
    }

    [SerializeField]
    public class ObjectEndpoints
    {
        public Vector3 a;
        public Vector3 b;
    }
    
    [Serializable]
    public class Map
    {
        public string name;
        public List<MapObjectData> objectData;
    }
    
    public enum ObjectType
    {
        Undefined = 0,
        Platform = 1,
        Trap = 2,
    }
}
