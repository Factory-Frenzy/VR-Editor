using UnityEngine.UI;

namespace Menu
{
    public class MainMenu : Menu
    {
        public Button platformsButton;
        public Button trapsButton;
        public Button saveButton;

        protected override void Start()
        {
            base.Start();
            platformsButton.onClick.AddListener(OpenPlatformsMenu);
            trapsButton.onClick.AddListener(OpenTrapsMenu);
            saveButton.onClick.AddListener(OpenSaveMenu);
        }
        
        private void OpenPlatformsMenu()
        {
            MenuManager.Instance().OpenPlatformsMenu();
        }
        
        private void OpenTrapsMenu()
        {
            MenuManager.Instance().OpenTrapsMenu();
        }
        
        private void OpenSaveMenu()
        {
            MenuManager.Instance().OpenSaveMenu();
        }
    }
}