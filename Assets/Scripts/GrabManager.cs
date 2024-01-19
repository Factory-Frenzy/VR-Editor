using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabManager : MonoBehaviour
{
    private bool _isGrab;
    
    public InputActionReference joystickRef;
    private InputAction _joystick;


    public void OnGrabEnter(SelectEnterEventArgs args)
    {
        _isGrab = true;
    }

    public void OnGrabExit(SelectExitEventArgs args)
    {
        _isGrab = false;
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
    }

    private void OnJoyStickMove(InputAction.CallbackContext context)
    {
        print("OnJoyStickMove: " + context.ReadValue<Vector2>());
        XRRayInteractor xrRayInteractor = GetComponent<XRRayInteractor>();
        RaycastHit hit;
        if (!xrRayInteractor.TryGetCurrent3DRaycastHit(out hit)) return;
        
        // Obtenir les données du joystick
        Vector2 joystickValue = context.ReadValue<Vector2>();
        
        var interactable = hit.collider.GetComponentInParent<XRBaseInteractable>();
        if (interactable != null)
        {

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
        }
    }
}