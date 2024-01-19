using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEditor;
using Formatting = Newtonsoft.Json.Formatting;

public class ExportJson : MonoBehaviour
{
    public static void ExportMapObjects(String fileName)
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

        // Convertit la liste en format JSON avec les paramètres pour exclure Vector3.normalized
        JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };

        string jsonMapData = JsonConvert.SerializeObject(mapObjectDataList, jsonSettings);

        // Chemin complet du fichier
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        // Écrit le JSON dans le fichier
        File.WriteAllText(filePath, jsonMapData);

        Debug.Log("Exportation terminée. Données enregistrées dans " + filePath);
    }

    [Serializable]
    public class MapObjectData
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;
    }
}
