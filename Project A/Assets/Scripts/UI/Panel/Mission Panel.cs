using CKProject.Interactable;
using CKProject.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static CKProject.Managers.MissionManager;

namespace CKProject.UI
{
    public class MissionPanel : MonoBehaviour
    {
        public Image ImageSprite;
        public Text GetText;
        public Text RevenueText;
        public CustomButton CustomButton;
        public MoneyData moneyData;

        public void InitPanel(MissionData missionData, UnityAction action)
        {
            GetText.text= missionData.Get_Text;
            RevenueText.text = missionData.Mission_Text;

            CustomButton.InitButtonClickEvent(action);
            CustomButton.MyMoneyToString(missionData.Mission_Count, missionData.Mission_Count_Index, MoneyData.unitMoney);
        }

        public void InitPanel(MissionData missionData, Kitchen  Kitchen, MissionData param)
        {
            GetText.text = missionData.Get_Text;
            RevenueText.text = missionData.Mission_Text;

            CustomButton.InitButtonClickEvent(Kitchen, param);
            CustomButton.MyMoneyToString(missionData.Mission_Count, missionData.Mission_Count_Index, MoneyData.unitMoney);
        }

    }
}
