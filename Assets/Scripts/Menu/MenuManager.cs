using UnityEngine;
using UnityEngine.InputSystem;

namespace Menu
{
    public class MenuManager : MonoBehaviour
    {
        public MainMenu menu;
        public PlatformsMenu platformsMenu;
        public TrapsMenu trapsMenu;
        public SaveMenu saveMenu;
        
        public InputActionReference openMainMenuRef;
        private InputAction _openMainMenu;

        private static MenuManager _instance;

        public static MenuManager Instance()
        {
            return _instance;
        }

        private void Start()
        {
            _instance = this;
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
           if (menu.IsOpen()) return;
           menu.Show(obj.ReadValueAsButton()); // todo: or true
           platformsMenu.Show(false);
           trapsMenu.Show(false);
           saveMenu.Show(false);
        }

        public void OpenPlatformsMenu()
        {
            menu.Show(false);
            platformsMenu.Show(true);
            trapsMenu.Show(false);
            saveMenu.Show(false);
        }

        public void OpenTrapsMenu()
        {
            menu.Show(false);
            platformsMenu.Show(false);
            trapsMenu.Show(true);
            saveMenu.Show(false);
        } 
        
        public void OpenSaveMenu()
        {
            menu.Show(false);
            platformsMenu.Show(false);
            trapsMenu.Show(false);
            saveMenu.Show(true);
        } 
    }
}