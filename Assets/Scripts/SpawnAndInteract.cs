using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class SpawnAndInteract : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn;

    void Update()
    {
        // todo: remove test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnAndInteractPrefabs();
        }
    }

    void SpawnAndInteractPrefabs()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 playerPosition = player.transform.position;
        playerPosition.y += 5;
        print("spawn at: " + playerPosition);

        foreach (GameObject prefabToSpawn in prefabsToSpawn)
        {
            GameObject spawnedObject = Instantiate(prefabToSpawn, playerPosition, Quaternion.identity);

            spawnedObject.tag = "MapObject";
            XRGrabInteractable xrGrabInteractable = spawnedObject.AddComponent<XRGrabInteractable>();
            xrGrabInteractable.trackRotation = false;

            /*
            Rigidbody rigidbody = spawnedObject.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
            }
            */
            
            spawnedObject.AddComponent<TeleportationArea>();
            
           // SetLayerRecursively(spawnedObject, LayerMask.NameToLayer("Teleport"));


            spawnedObject.SetActive(true);
        }
        
        // Fonction récursive pour définir le layer de l'objet et de ses enfants
        void SetLayerRecursively(GameObject obj, int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
    }
}