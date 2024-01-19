using TMPro;
using UnityEngine.UI;

namespace Menu
{
    public class SaveMenu : Menu
    {
        public Button saveButton;
        public TMP_InputField inputField;

        protected override void Start()
        {
            base.Start();
            saveButton.onClick.AddListener(OnSave);
        }

        private void OnSave()
        {
            string fileName = inputField.text + ".json";
            ExportJson.ExportMapObjects(fileName);
            Show(false);
        }
    }
}