using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.InputSystem;
using Formatting = Newtonsoft.Json.Formatting;

public class ExportJson : MonoBehaviour
{
    [SerializeField] private string fileName = "map.json";
    
    public InputActionReference saveRef;
    private InputAction _save;

    private void Start()
    {
        _save = saveRef.action;
        _save.Enable();
        _save.performed += OnSave;
    }

    private void OnSave(InputAction.CallbackContext obj)
    {
        // todo: use save menu
        print("OnSave");
        ExportMapObjects();
    }

    void ExportMapObjects()
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
                name = mapObject.name,
                position = mapObject.transform.position
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

    [System.Serializable]
    public class MapObjectData
    {
        public string name;
        public Vector3 position;
    }
}