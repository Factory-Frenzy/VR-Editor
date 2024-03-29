using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class TrapsMenu : Menu
    {
        public GameObject bumperObject;
        public GameObject lancerObject;
        public GameObject fanObject;
        
        public Button bumperButton;
        public Button lancerButton;
        public Button fanButton;

        public SpawnAndInteract spawnAndInteract;

        protected override void Start()
        {
            base.Start();
            bumperButton.onClick.AddListener(OnSpawnBumper);
            lancerButton.onClick.AddListener(OnSpawnLancer);
            fanButton.onClick.AddListener(OnSpawnFan);
        }
        
        protected override void GoBack()
        {
            MenuManager.Instance().OpenMainMenu();
            Show(false);
        }
        
        private void OnSpawnBumper()
        {
            spawnAndInteract.SpawnAndInteractPrefabs(bumperObject);
            Show(false);
        }
        
        private void OnSpawnLancer()
        {
            spawnAndInteract.SpawnAndInteractPrefabs(lancerObject);
            Show(false);
        }
        
        private void OnSpawnFan()
        {
            spawnAndInteract.SpawnAndInteractPrefabs(fanObject);
            Show(false);
        }
    }
}