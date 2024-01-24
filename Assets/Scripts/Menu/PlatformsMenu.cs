using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class PlatformsMenu : Menu
    {
        public GameObject classicPlatformObject;
        public GameObject classic2PlatformObject;
        public GameObject classic3PlatformObject;
        public GameObject movePlatformObject;
        public GameObject trampolinePlatformObject;
        public GameObject startPlatformObj;
        public GameObject endPlatformObj;
        
        public Button classicPlatformButton;
        public Button classic2PlatformButton;
        public Button classic3PlatformButton;
        public Button movePlatformButton;
        public Button trampolinePlatformButton;
        public Button startPlatformButton;
        public Button endPlatformButton;

        public SpawnAndInteract spawnAndInteract;
        
        protected override void Start()
        {
            base.Start();
            classicPlatformButton.onClick.AddListener(OnSpawnClassic80Platform);
            classic2PlatformButton.onClick.AddListener(OnSpawnClassic360Platform);
            classic3PlatformButton.onClick.AddListener(OnSpawnClassic520Platform);
            movePlatformButton.onClick.AddListener(OnSpawnMovePlatform);
            trampolinePlatformButton.onClick.AddListener(OnSpawnTrampolinePlatform);
            startPlatformButton.onClick.AddListener(OnSpawnStartPlatform);
            endPlatformButton.onClick.AddListener(OnSpawnEndPlatform);
        }
        
        protected override void GoBack()
        {
            MenuManager.Instance().OpenMainMenu();
            Show(false);
        }

        private void OnSpawnClassic80Platform()
        {
            spawnAndInteract.SpawnAndInteractPrefabs(classicPlatformObject);
            Show(false);
        }
        
        private void OnSpawnClassic360Platform()
        {
            spawnAndInteract.SpawnAndInteractPrefabs(classic2PlatformObject);
            Show(false);
        }
        
        private void OnSpawnClassic520Platform()
        {
            spawnAndInteract.SpawnAndInteractPrefabs(classic3PlatformObject);
            Show(false);
        }
        
        private void OnSpawnMovePlatform()
        {
            spawnAndInteract.SpawnAndInteractPrefabs(movePlatformObject);
            Show(false);
        }
        
        private void OnSpawnTrampolinePlatform()
        {
            spawnAndInteract.SpawnAndInteractPrefabs(trampolinePlatformObject);
            Show(false);
        }
        
        private void OnSpawnStartPlatform()
        {
            GameObject exist = GameObject.Find(startPlatformObj.name + "(Clone)");
            if (exist) return;
            spawnAndInteract.SpawnAndInteractPrefabs(startPlatformObj);
            Show(false);
        }
        
        private void OnSpawnEndPlatform()
        {
            GameObject exist = GameObject.Find(endPlatformObj.name + "(Clone)");
            if (exist) return;
            spawnAndInteract.SpawnAndInteractPrefabs(endPlatformObj);
            Show(false);
        }
    }
}