using CKProject.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.UI;
using UpgradeFoodData = CKProject.Managers.FoodManager.UpgradeFoodData;
using CKProject.Managers;

namespace MVP.Upgrade
{
    public class UpgradeView : MonoBehaviour
    {
        // 보여질 데이터
        public CustomButton[] Button;

        //private void Awake()
        //{
        //    foreach(var button in Button)
        //    {
        //        button.MyMoneyToString()
        //    }
        //}

        public void Initialized()
        {
            EFoodType foodType = EFoodType.HotDog;
            //UpgradeFoodData Upgrade = FoodManager.Instance.GetFoodLevelData(foodType);
            foreach (var button in Button)
            {
                UpgradeFoodData Upgrade = FoodManager.Instance.GetFoodLevelData(foodType++);
                button.MyMoneyToString(Upgrade.Upgrade_Cost, Upgrade.UpgradeIndex, MoneyData.unitMoney);
            }
        }

        public void UpdateUpgradeUIText(EFoodType foodType, UpgradeFoodData updateData)
        {
            Button[(int)foodType - 1].MyMoneyToString(updateData.Upgrade_Cost, updateData.UpgradeIndex, MoneyData.unitMoney);
        }
    }
}
