using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CKProject.UI
{
    public class CustomButton : MonoBehaviour
    {
        public Button button;
        public Text upgradeMoneyText;

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