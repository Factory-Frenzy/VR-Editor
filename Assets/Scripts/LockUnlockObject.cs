using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace DefaultNamespace
{
    public class LockUnlockObject : MonoBehaviour
    {
        private XRGrabInteractable _grabInteractable;

        private bool _isLocked;

        public bool IsLocked
        {
            get => _isLocked;
            set
            {
                _isLocked = value;

                // Activer ou désactiver le XRGrabInteractable en fonction de l'état de verrouillage
                if (_grabInteractable != null)
                {
                    _grabInteractable.enabled = !_isLocked;
                }
            }
        }

        void Start()
        {
            // Assurez-vous que le GameObject a un composant XRGrabInteractable
            _grabInteractable = GetComponent<XRGrabInteractable>();

            if (_grabInteractable == null)
            {
                Debug.LogError("Le GameObject doit avoir un composant XRGrabInteractable pour utiliser LockUnlockObject.");
            }
            else
            {
                // Initialisez l'état de verrouillage au démarrage
                IsLocked = _isLocked;
            }
        }
    }
}