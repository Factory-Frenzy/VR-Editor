using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public abstract class Menu : MonoBehaviour
    {
        public Button backButton;
        public Button closeButton;

        private void Start()
        {
            Show(false);

            if (backButton != null)
            {
                backButton.onClick.AddListener(GoBack);
            }

            if (closeButton != null)
            {
                closeButton.onClick.AddListener(CloseMenu);
            }
        }

        private void CloseMenu()
        {
            Show(false);
        }

        private void GoBack()
        {
            throw new System.NotImplementedException();
        }

        public void Show(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}