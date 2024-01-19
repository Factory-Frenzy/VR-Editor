using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnAndInteract : MonoBehaviour
{
    public void SpawnAndInteractPrefabs(GameObject prefabToSpawn)
    {
        GameObject player = GameObject.FindGameObjectWithTag("MainCamera");
        
        Transform transform = player.transform;
        
        // Récupére la position devant le joueur
        Vector3 spawnPosition = transform.position + transform.forward * 8f;
        
        // Créer une rotation à partir de l'angle normalisé devant le joueur
        float angle = Mathf.Round(transform.eulerAngles.y / 90) * 90;
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        
        print("spawnPosition " + spawnPosition);
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, rotation);

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
        SetLayerRecursively(spawnedObject, LayerMask.NameToLayer("Grabbable"));
        
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