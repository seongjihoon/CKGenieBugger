using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using CKProject.SingleTon;
using CKProject.Interactable;
using CKProject.UI;

namespace CKProject.Managers
{
    // 현재 게임에서 사용 중인 데이터 
    public class GameManager : SingleTon<GameManager>
    {
        [ReadOnly] public int Stage = 1;
        [ReadOnly] public int OpenFood = 0;


        public MainPresenter MainUI;

        public MoneyData moneyData;
        //public int[] Money = null;
        public int Size;
        //public int Money_Index = 0;

        private void Awake()
        {
            CreateInstance(this);
            OpenFood = Stage;
            moneyData.Money = new int[Size];
            DontDestroyOnLoad(this.gameObject);
        }

        public void KitchenOpen()
        {
            if(Stage > OpenFood && (EFoodType)OpenFood < EFoodType.ALL)
            {
                OpenFood++;
            }
        }

        public void AddMoney(int[] money)
        {
            for (int i = 0; i < money.Length; i++)
            {
                moneyData.Money[i] += money[i];
            }
            moneyData.Theorem();
            MainUI.UpdateMoney();
        }
        public void SubMoney(int[] useMoney)
        {
            for (int i = 0; i < useMoney.Length; i++)
            {
                moneyData.Money[i] -= useMoney[i];
            }
            moneyData.Theorem();
            MainUI.UpdateMoney();
        }

        // 업데이트 해줘야함
        public bool CompareMoney(int[] upgrade_Cost, int upgrade_Index)
        {
            if(upgrade_Index <= moneyData.Index && upgrade_Cost[upgrade_Index] <= moneyData.Money[moneyData.Index]  )
                return true;
            return false;
        }
    }
}