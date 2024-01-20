using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenu : Menu
    {
        public Button platformsButton;
        public Button trapsButton;
        public Button saveButton;
        public Button checkpointButton;
        
        public SpawnAndInteract spawnAndInteract;
        public GameObject checkpointGameObject;

        protected override void Start()
        {
            base.Start();
            platformsButton.onClick.AddListener(OpenPlatformsMenu);
            trapsButton.onClick.AddListener(OpenTrapsMenu);
            saveButton.onClick.AddListener(OpenSaveMenu);
            checkpointButton.onClick.AddListener(SpawnCheckpoint);
        }

        private void SpawnCheckpoint()
        {
            spawnAndInteract.SpawnAndInteractPrefabs(checkpointGameObject);
            Show(false);
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