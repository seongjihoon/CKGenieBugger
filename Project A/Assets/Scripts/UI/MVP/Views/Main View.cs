using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MVP.Upgrade;
using UnityEngine.Events;
using System;
using MVP.Mission;

namespace CKProject.UI
{
    public class MainView : MonoBehaviour
    {
        [Header("UI")]
        public UpgradePresenter UpgradePanel;
        public MissionPresenter MissionPanel;
        public GameObject NextStageButton;
        public GameObject MissionButton;
        public GameObject SettingPanel;
        public GameObject ShopPanel;
        public Text UserInfo;


        [Header("Legarcy")]
        public Text MoneyText;
        

        private void Start()
        {
            UpgradePanel.gameObject.SetActive(false);
            MissionPanel.Initialized();
            MissionPanel.gameObject.SetActive(false);
            MissionButton.SetActive(true);
            NextStageButton.SetActive(false);
            SettingPanel.SetActive(false);
            ShopPanel.SetActive(false);
        }

        public void SetUpgradePanel()
        {
            UpgradePanel.gameObject.SetActive(!UpgradePanel.gameObject.activeSelf);
        }

        public void SetUpgradePanel(GameObject kitchen)
        {
            UpgradePanel.gameObject.SetActive(!UpgradePanel.gameObject.activeSelf);
            UpgradePanel.UpdateConponent(kitchen);
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

        public void SetMissionPanel()
        {
            MissionPanel.gameObject.SetActive(!MissionPanel.gameObject.activeSelf);
            UpgradePanel.gameObject.SetActive(false);
        }

        public void ChangeButton()
        {
            MissionButton.SetActive(false);
            NextStageButton.SetActive(true);
        }

        public void ShowUserInfo(string name, string id )
        {
            MoneyText.text = "UserName: " + name + "\nUserId" + id;
         }
    }
}
