using CKProject.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.UI;
using UpgradeFoodData = CKProject.Managers.FoodManager.UpgradeFoodData;
using CKProject.Managers;
using UnityEngine.UI;
using UnityEngine.Events;

namespace MVP.Upgrade
{
    public class UpgradeView : MonoBehaviour
    {
        // 보여질 데이터
        public CustomButton CustomButton;
        public Text LevelText;
        public Text NameText;
        public Text RevenueText;
        public Text CreateTimeText;
        //private void Awake()
        //{
        //    foreach(var button in Button)
        //    {
        //        button.MyMoneyToString()
        //    }
        //}

        public void Initialized(GameObject kitchen)
        {
            Kitchen targetKitchen = kitchen.GetComponent<Kitchen>();
            EFoodType foodType = targetKitchen.FoodSO.foodType;
            //UpgradeFoodData Upgrade = FoodManager.Instance.GetFoodLevelData(foodType);
            // 스테이지에 따라서 출력할 UI Button 달리하기.
            CustomButton.InitButtonClickEvent(targetKitchen.Upgrade);
            UpgradeFoodData Upgrade = FoodManager.Instance.GetFoodLevelData(foodType++);

            LevelText.text = Upgrade.Level.ToString();
            NameText.text = targetKitchen.FoodName();
            CreateTimeText.text = Upgrade.CreateTime.ToString();
            RevenueText.text = CustomButton.GetMoneyToString(Upgrade.Revenue, Upgrade.RevenueIndex, MoneyData.unitMoney);

            CustomButton.MyMoneyToString(Upgrade.Upgrade_Cost, Upgrade.UpgradeIndex, MoneyData.unitMoney);

            //for (int i =0; i < gameManager.Stage; i++)
            //{
            //    //UpgradeFoodData Upgrade = fmanager.GetFoodLevelData(foodType++);
            //    //Button[i].gameObject.SetActive(true);
            //    //Button[i].MyMoneyToString(Upgrade.Upgrade_Cost, Upgrade.UpgradeIndex, MoneyData.unitMoney);
            //}
        }

        public void UpdateUpgradeUIText(EFoodType foodType, UpgradeFoodData updateData)
        {
            //Button[(int)foodType - 1].MyMoneyToString(updateData.Upgrade_Cost, updateData.UpgradeIndex, MoneyData.unitMoney);
        }

    }
}
