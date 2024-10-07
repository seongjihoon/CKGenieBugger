using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CKProject.UI
{
    public class MainView : MonoBehaviour
    {
        [Header("UI")]
        public GameObject UpgradePanel;

        public GameObject SettingPanel;
        public GameObject ShopPanel;


        [Tooltip("Legarcy")]
        public Text MoneyText;
        

        private void Start()
        {
            UpgradePanel.SetActive(false);
            SettingPanel.SetActive(false);
        }

        public void SetUpgradePanel()
        {
            UpgradePanel.SetActive(!UpgradePanel.activeSelf);
        }

        public void SetSettingPanel()
        {
            SettingPanel.SetActive(!SettingPanel.activeSelf);
        }

        public void SetShopPanel()
        {
            ShopPanel.SetActive(!ShopPanel.activeSelf);
        }


        public void UpdateMoneyText(string text)
        {
            MoneyText.text = text;
        }
    }
}
