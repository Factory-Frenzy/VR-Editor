using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace Teleportation
{
    public class TeleportationManager : MonoBehaviour
    {
        [SerializeField] private TeleportationProvider provider;

        [SerializeField] private InputActionReference activate;

        [SerializeField] private InputActionReference cancel;

        [SerializeField] private XRRayInteractor teleportRay;

        private bool isActive;

        XRInteractorLineVisual lineVisual;
        
        [FormerlySerializedAs("grabManager")] [SerializeField] private InteractionManager interactionManager;

        void Start()
        {
            teleportRay.enabled = false;

            activate.action.Enable();
            activate.action.performed += OnTeleportActivate; // thumbstick pressed
            activate.action.canceled += OnTeleportRequested; // = z released

            cancel.action.Enable();
            cancel.action.performed += OnTeleportCancel;

            try
            {
                lineVisual = teleportRay.GetComponent<XRInteractorLineVisual>();
            }
            catch (Exception e)
            {
                Debug.LogError("Error : " + e.Message);
            }
        }


        private void OnTeleportRequested(InputAction.CallbackContext context)
        {
            if (!isActive)
                return;

            if (interactionManager.IsGrab())
            {
                return;
            }

            RaycastHit hit;

            if (!teleportRay.TryGetCurrent3DRaycastHit(out hit))
            {
                teleportRay.enabled = false;
                isActive = false;
                return;
            }

            var interactable = hit.collider.GetComponentInParent<BaseTeleportationInteractable>();
            var t = interactable.GetAttachTransform(teleportRay);

            TeleportRequest request = new TeleportRequest()
            {
                destinationPosition = hit.point,
            };

            if (interactable is TeleportationAnchor)
            {
                request = new TeleportRequest()
                {
                    destinationPosition = t.position,
                    destinationRotation = t.rotation,
                    matchOrientation = interactable.matchOrientation
                };
            }

            provider.QueueTeleportRequest(request);
            setActiveTeleport(false);
        }

        private void setActiveTeleport(bool active)
        {
            teleportRay.enabled = lineVisual.enabled = isActive = active;
        }

        private void OnTeleportActivate(InputAction.CallbackContext ctx)
        {
            if (interactionManager.IsGrab()) return;
            setActiveTeleport(true);
        }

        private void OnTeleportCancel(InputAction.CallbackContext ctx)
        {
            if (interactionManager.IsGrab()) return;
            setActiveTeleport(false);
        }
    }
}