using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static CKProject.Managers.MissionManager;

namespace CKProject.UI
{
    public class CustomButton : MonoBehaviour
    {
        public Button button;
        public Text upgradeMoneyText;

        public void InitButtonClickEvent(UnityAction action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }
        public void InitButtonClickEventParam_MissionData(UnityAction<MissionData> action, MissionData param)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => action(param));
        }

        public string GetMoneyToString(int[] Money, int index, string[] unit)
        {
            if (Money == null)
            {
                return "MAX";
            }
            float a = Money[index];

            if (index > 0)
            {
                float b = Money[index - 1];
                a += b / 1000;
            }

            if (index == 0)
            {
                a += 0;
            }

            string p;
            p = (float)(Math.Truncate(a * 100) / 100) + unit[index];

            return p;
        }

        public void MyMoneyToString(int[] Money, int index, string[] unit)
        {
            if(Money == null)
            {
                upgradeMoneyText.text = "MAX";
                return;
            }
            float a = Money[index];

            if (index > 0)
            {
                float b = Money[index - 1];
                a += b / 1000;
            }

            if (index == 0)
            {
                a += 0;
            }

            string p;
            p = (float)(Math.Truncate(a * 100) / 100) + unit[index];

            upgradeMoneyText.text = p;
        }

    }
}