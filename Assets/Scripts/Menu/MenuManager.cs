using UnityEngine;
using UnityEngine.InputSystem;

namespace Menu
{
    public class MenuManager : MonoBehaviour
    {
        public MainMenu menu;
        public TrapsMenu trapsMenu;
        public PlatformsMenu platformsMenu;
        
        public InputActionReference openMainMenuRef;
        private InputAction _openMainMenu;

        public static MenuManager Instance;

        private void Start()
        {
            Instance = this;
            
            menu.Show(false);
            platformsMenu.Show(false);
            //trapsMenu.Show(false);
        }

        private void Awake()
        {
            _openMainMenu = openMainMenuRef.action;
            _openMainMenu.Enable();
            _openMainMenu.performed += OnOpenMainMenu;
        }
        
        private void OnDestroy()
        {
            _openMainMenu.performed -= OnOpenMainMenu;
        }
        
        private void OnOpenMainMenu(InputAction.CallbackContext obj)
        {
           menu.Show(obj.ReadValueAsButton()); // todo: or true
           platformsMenu.Show(false);
           // trapsMenu.Show(false);
        }

        public void OpenPlatformsMenu()
        {
            menu.Show(false);
            platformsMenu.Show(true);
           // trapsMenu.Show(false);
        }

        public void OpenTrapsMenu()
        {
            menu.Show(false);
            platformsMenu.Show(false);
           // trapsMenu.Show(true);
        } 
    }
}