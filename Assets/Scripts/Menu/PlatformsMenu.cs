using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class PlatformsMenu : Menu
    {
        public GameObject classicPlatformObject;
        public GameObject startPlatformObj;
        public GameObject endPlatformObj;
        
        public Button classicPlatformButton;
        public Button startPlatformButton;
        public Button endPlatformButton;

        public SpawnAndInteract spawnAndInteract;
        
        protected override void Start()
        {
            base.Start();
            classicPlatformButton.onClick.AddListener(OnSpawnClassicPlatform);
            startPlatformButton.onClick.AddListener(OnSpawnStartPlatformPlatform);
            endPlatformButton.onClick.AddListener(OnSpawnEndPlatform);
        }

        private void OnSpawnClassicPlatform()
        {
            spawnAndInteract.SpawnAndInteractPrefabs(classicPlatformObject);
            Show(false);
        }
        
        private void OnSpawnStartPlatformPlatform()
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