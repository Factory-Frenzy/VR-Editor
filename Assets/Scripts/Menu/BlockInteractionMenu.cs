using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class BlockInteractionMenu : Menu
    {
        public Button deleteButton;
        public Button lockUnlockButton;

        private GameObject _currentGameObject;

        protected override void Start()
        {
            base.Start();
            deleteButton.onClick.AddListener(OnDelete);
            lockUnlockButton.onClick.AddListener(OnLockUnlock);
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
        }
    }
}