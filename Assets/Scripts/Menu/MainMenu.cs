using UnityEngine.UI;

namespace Menu
{
    public class MainMenu : Menu
    {
        public Button platformsButton;
        public Button trapsButton;
        
        private void Start()
        {
            // base.Start();
            platformsButton.onClick.AddListener(OpenPlatformsMenu);
            trapsButton.onClick.AddListener(OpenTrapsMenu);
        }
        
        private void OpenPlatformsMenu()
        {
            MenuManager.Instance.OpenPlatformsMenu();
        }
        
        private void OpenTrapsMenu()
        {
            MenuManager.Instance.OpenTrapsMenu();

        }
    }
}