using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Formatting = Newtonsoft.Json.Formatting;

public class ExportJson : MonoBehaviour
{
    [SerializeField] 
    public string uploadPath = "http://10.191.92.139:3000/upload";
    
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
                name = mapObject.name.Replace("(Clone)", ""),
                position = mapObject.transform.position,
                rotation = mapObject.transform.rotation
            };

            mapObjectDataList.Add(mapObjectData);
        }

        Map map = new Map
        {
            name = mapName,
            objectData = mapObjectDataList,
        };
     
        UploadMap(mapName + ".json", map);
    }

    private void UploadMap(string fileName, Map map)
    {
        // Création d'un objet contenant les données à envoyer
        var requestData = new
        {
            fileName,
            fileContent = map,
        };

        //var data = JsonUtility.ToJson(requestData);
        JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };
        var data = JsonConvert.SerializeObject(requestData, jsonSettings);

        // Envoi des données au serveur
        UnityWebRequest request = UnityWebRequest.Post(uploadPath, data, "application/json");
        request.SetRequestHeader("Content-Type", "application/json");
        
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
        public string name;
        public Vector3 position;
        public Quaternion rotation;
    }
    
    [Serializable]
    public class Map
    {
        public string name;
        public List<MapObjectData> objectData;
    }
}
