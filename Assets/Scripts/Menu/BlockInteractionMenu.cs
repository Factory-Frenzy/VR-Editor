using DefaultNamespace;
using MapObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class BlockInteractionMenu : Menu
    {
        public Button deleteButton;
        public Button lockUnlockButton;

        public TMP_Text textLockUnlock;
        
        // Speed
        public Slider speedSlider;
        public TMP_Text speedValue;
        private bool displaySlider;

        private GameObject _currentGameObject;

        public GameObject speedSection;
        private MovePlatform _platformMovement;

        protected override void Start()
        {
            base.Start();
            deleteButton.onClick.AddListener(OnDelete);
            lockUnlockButton.onClick.AddListener(OnLockUnlock);
            speedSlider.onValueChanged.AddListener(delegate {OnSpeedChange(); });
        }

        private void OnSpeedChange()
        {
            if (!speedSlider) return;
                
            speedValue.text = speedSlider.value.ToString();
            _platformMovement.Speed = speedSlider.value;
        }

        private void OnDelete()
        {
            if (_currentGameObject)
            {
                Destroy(_currentGameObject);
            }
            Show(false);
        }

        private void OnLockUnlock()
        {
            LockManager.SetObjectLock(_currentGameObject, !LockManager.IsObjectLocked(_currentGameObject));
            Show(false);
        }

        public void SetGameObject(GameObject currentGameObject)
        {
            _currentGameObject = currentGameObject;
            textLockUnlock.text = LockManager.IsObjectLocked(currentGameObject) ? "Déverrouiller" : "Verrouiller";
            
            if (currentGameObject.name.StartsWith("Platform Move 520"))
            {
                _platformMovement = _currentGameObject.GetComponentInChildren<MovePlatform>();
                if (!_platformMovement) return;
                speedValue.text = _platformMovement.Speed.ToString();
                speedSlider.value = _platformMovement.Speed;
                displaySlider = true;
                speedSection.SetActive(true);
            }
            else
            {
                displaySlider = false;
                speedSection.SetActive(false);
            }
        }
    }
}