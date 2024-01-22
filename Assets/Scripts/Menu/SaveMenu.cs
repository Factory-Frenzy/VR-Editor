using TMPro;
using UnityEngine;
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
        
        protected override void GoBack()
        {
            MenuManager.Instance().OpenMainMenu();
            Show(false);
        }

        private void OnSave()
        {
            string mapName = inputField.text;
            GameObject obj = GameObject.Find("ExportManager");
            ExportJson exportJson = obj.GetComponent<ExportJson>();
            exportJson.SaveMap(mapName);
            Show(false);
        }
    }
}