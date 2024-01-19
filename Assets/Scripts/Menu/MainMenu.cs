using UnityEngine.UI;

namespace Menu
{
    public class MainMenu : Menu
    {
        public Button platformsButton;
        public Button trapsButton;

        protected override void Start()
        {
            base.Start();
            platformsButton.onClick.AddListener(OpenPlatformsMenu);
            trapsButton.onClick.AddListener(OpenTrapsMenu);
        }
        
        private void OpenPlatformsMenu()
        {
            print("OpenPlatformsMenu");
            MenuManager.Instance().OpenPlatformsMenu();
        }
        
        private void OpenTrapsMenu()
        {
            print("OpenTrapsMenu");
            MenuManager.Instance().OpenTrapsMenu();
        }
    }
}