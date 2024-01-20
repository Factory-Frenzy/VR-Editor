using DefaultNamespace;
using Menu;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionManager : MonoBehaviour
{
    private bool _isGrab;
    
    public InputActionReference joystickRef;
    private InputAction _joystick;
    
    public InputActionReference selectRef;
    private InputAction _select;

    public SoundManager soundManager;

    public void OnGrabEnter(SelectEnterEventArgs args)
    {
        _isGrab = true;
        soundManager.VibrateController(ControllerHand.Right, 0.5f, 0.25f);
    }

    public void OnGrabExit(SelectExitEventArgs args)
    {
        _isGrab = false;
        soundManager.VibrateController(ControllerHand.Right, 0.5f, 0.25f);
    }

    public bool IsGrab()
    {
        return _isGrab;
    }

    private void Awake()
    {
        _joystick = joystickRef.action;
        _joystick.Enable();
        _joystick.performed += OnJoyStickMove;
        
        _select = selectRef.action;
        _select.Enable();
        _select.performed += OnSelect;
    }
    
    private XRBaseInteractable GetCurrentInteractable()
    {
        XRRayInteractor xrRayInteractor = GetComponent<XRRayInteractor>();
        RaycastHit hit;
        if (!xrRayInteractor.TryGetCurrent3DRaycastHit(out hit)) return null;
        
        XRBaseInteractable interactable = hit.collider.GetComponentInParent<XRBaseInteractable>();

        return interactable;
    }

    private void OnSelect(InputAction.CallbackContext obj)
    {
        print("OnSelect");

        var interactable = GetCurrentInteractable();
        if (!interactable) return;

        print("inter " + interactable.name);
        
        MenuManager.Instance().OpenBlockInteractionMenu(interactable.gameObject);
    }

    private void OnJoyStickMove(InputAction.CallbackContext context)
    {
        print("OnJoyStickMove: " + context.ReadValue<Vector2>());
        
        var interactable = GetCurrentInteractable();
        if (!interactable) return;
        
        if (interactable != null)
        {
            // Obtenir les données du joystick
            Vector2 joystickValue = context.ReadValue<Vector2>();

            // Calculer l'angle en radians en fonction des données du joystick
            float angle = Mathf.Atan2(joystickValue.y, joystickValue.x);

            // Convertir l'angle en degrés
            float angleInDegrees = angle * Mathf.Rad2Deg;

            // Définir les angles correspondants aux quatre directions
            float[] directionAngles = { 180f, 0f, 90f, -90f };

            // Trouver l'angle le plus proche parmi les quatre directions
            float closestAngle = directionAngles[0];
            float minDifference = Mathf.Abs(angleInDegrees - directionAngles[0]);

            foreach (float directionAngle in directionAngles)
            {
                float difference = Mathf.Abs(angleInDegrees - directionAngle);

                if (difference < minDifference)
                {
                    minDifference = difference;
                    closestAngle = directionAngle;
                }
            }

            // Faire pivoter le GameObject en grab dans la direction correspondante
            interactable.transform.rotation = Quaternion.Euler(0f, closestAngle, 0f);
            soundManager.VibrateController(ControllerHand.Right, 0.5f, 0.25f);
        }
    }
}