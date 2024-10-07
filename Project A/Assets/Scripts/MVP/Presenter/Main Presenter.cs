using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CKProject.UI
{
    public class MainPresenter : MonoBehaviour
    {
        public MainModel Model;
        public MainView View;

        private void MoneyUpdate(ref int[] money)
        {
            Theorem();
            MyMoneyToString(money, Model.Index, Model.unitMoney);
        }

        private void Theorem()
        {
            for (int i = 0; i < Model.Size; i++)
            {
                if (Model.Money[i] > 0)
                    Model.Index = i;
            }

            for (int i = 0; i <= Model.Index; i++)
            {
                if (Model.Money[i] >= 1000)
                {
                    Model.Money[i] -= 1000;
                    Model.Money[i + 1] += 1;
                }

                if (Model.Money[i] < 0)
                {
                    if (Model.Index > 1)
                    {
                        Model.Money[i + 1] -= 1;
                        Model.Money[i] += 1000;
                    }
                }
            }
        }

        public void AddMoney(int money)
        {
            int index = 0;
            int memo = 0;
            while (money > 1000)
            {
                memo = money % 1000;
                Model.Money[index] += memo;
                index++;
                money /= 1000;
            }
            Model.Money[index] += memo;
            Theorem();
            MyMoneyToString(Model.Money, Model.Index, Model.unitMoney);
        }

        public void AddMoney(int[] getMoney)
        {
            for (int i = 0; i < getMoney.Length; i++)
            {
                Model.Money[i] += getMoney[i];
            }
            MoneyUpdate(ref Model.Money);
        }

        public void SubMoney(int[] useMoney)
        {
            for (int i = 0; i < useMoney.Length; i++)
            {
                Model.Money[i] -= useMoney[i];
            }
            MoneyUpdate(ref Model.Money);
        }

        public void MyMoneyToString(int[] Money, int index, string[] unit)
        {
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
            View.UpdateMoneyText(p);
        }

        public void SetUpgradePanel()
        {
            View.SetUpgradePanel();
        }
        public void SetSettingPanel()
        {
            View.SetSettingPanel();
        }

        public void SetShopPanel()
        {
            View.SetShopPanel();
        }
    }
}