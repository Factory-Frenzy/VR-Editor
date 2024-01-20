using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace DefaultNamespace
{
    public class LockUnlockObject : MonoBehaviour
    {
        private XRGrabInteractable grabInteractable;

        private bool isLocked = false;

        public bool IsLocked
        {
            get { return isLocked; }
            set
            {
                isLocked = value;

                // Activer ou désactiver le XRGrabInteractable en fonction de l'état de verrouillage
                if (grabInteractable != null)
                {
                    grabInteractable.enabled = !isLocked;
                }
            }
        }

        void Start()
        {
            // Assurez-vous que le GameObject a un composant XRGrabInteractable
            grabInteractable = GetComponent<XRGrabInteractable>();

            if (grabInteractable == null)
            {
                Debug.LogError("Le GameObject doit avoir un composant XRGrabInteractable pour utiliser LockUnlockObject.");
            }
            else
            {
                // Initialisez l'état de verrouillage au démarrage
                IsLocked = isLocked;
            }
        }
    }
}