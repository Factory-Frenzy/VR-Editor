using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class SaveMenu : Menu
    {
        public Button saveButton;
        public TMP_InputField inputField;
        public TMP_Text errorText;

        private ExportJson exportJson;

        protected override void Start()
        {
            base.Start();
            saveButton.onClick.AddListener(OnSave);
            
            GameObject obj = GameObject.Find("ExportManager");
            exportJson = obj.GetComponent<ExportJson>();

            CheckFileExist();
        }
        
        protected override void GoBack()
        {
            MenuManager.Instance().OpenMainMenu();
            Show(false);
        }

        private void CheckFileExist()
        {
            if (exportJson && !exportJson.http && exportJson.CheckMapExist(inputField.text))
            {
                errorText.text = "Attention le fichier existe déjà";
            }
            else
            {
                errorText.text = "";
            }
        }

        private void OnSave()
        {
            string mapName = inputField.text;
            exportJson.SaveMap(mapName);
            Show(false);
        }
    }
}