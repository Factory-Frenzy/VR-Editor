using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPrefabButton : MonoBehaviour
{
    public GameObject prefabToShow; // Faites glisser et déposez votre Prefab ici.
    public Button button; 
    
    void Start()
    {
        // Vérifiez si le Prefab et le composant Text existent.
        if (prefabToShow != null && button != null)
        {
            print("icii ,?");
            // Instanciez le Prefab et obtenez son rendu en tant que sprite ou texture, par exemple.
            SpriteRenderer prefabRenderer = prefabToShow.GetComponent<SpriteRenderer>();
            if (prefabRenderer != null)
            {
                print("et la ?");
                // Utilisez le sprite du Prefab comme image pour le bouton.
              //  buttonText.text = ""; // Assurez-vous que le texte est vide si vous utilisez une image.
                Image buttonImage = button.GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.sprite = prefabRenderer.sprite;
                }
            }
            else
            {
                // Si le Prefab n'a pas de rendu SpriteRenderer, vous devrez adapter cette logique en fonction de votre Prefab.
                Debug.LogWarning("Le Prefab n'a pas de SpriteRenderer.");
            }
        }
        else
        {
            Debug.LogWarning("Veuillez assigner le Prefab et le composant Text dans l'éditeur Unity.");
        }
    }
}
