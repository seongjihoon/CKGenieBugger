using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using CKProject.SingleTon;
using CKProject.Interactable;
using CKProject.UI;
using CKProject.AttributeEditor;
using UnityEngine.SceneManagement;

namespace CKProject.Managers
{
    // 현재 게임에서 사용 중인 데이터 
    // 미션은 여기서 관리 해야할까?
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public int Stage = 1;
        [ReadOnly] public int OpenFood = 0;

        [HideInInspector] public bool UpgradeDitryFlag = false;

        public TriggerManager TriggerManager;
        public MainPresenter MainUI;

        public MoneyData moneyData;
        //public int[] Money = null;
        public int Size;
        //public int Money_Index = 0;

        private void Awake()
        {
            //CreateInstance(this);
            if(Instance == null)
               Instance = this;
            OpenFood = Stage;
            moneyData.Money = new int[Size];
            TriggerManager = GameObject.Find("Custom Trigger Manager").GetComponent<TriggerManager>();
            MainUI = GameObject.Find("Main UI Manager").GetComponent<MainPresenter>();
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
            //moneyData.Theorem();
            MainUI.UpdateMoney();
        }
        public void SubMoney(int[] useMoney)
        {
            for (int i = 0; i < useMoney.Length; i++)
            {
                moneyData.Money[i] -= useMoney[i];
            }
            //moneyData.Theorem();
            MainUI.UpdateMoney();
        }

        // 업데이트 해줘야함
        public bool CompareMoney(int[] upgrade_Cost, int upgrade_Index)
        {
            if(upgrade_Index < moneyData.Index || (upgrade_Index == moneyData.Index  && upgrade_Cost[upgrade_Index] <= moneyData.Money[upgrade_Index]))
                return true;
            return false;
        }

        public void ChangedButton()
        {
            MainUI.LevelUpChangeButton();
        }

        public void NextStage()
        {
            Stage++;
            SceneLoader.Instance.LoadScene("Game Scene 2");
            OpenFood = Stage;
            moneyData.Money = new int[Size];
            FoodManager.Instance.KitchenSet();
            FoodManager.Instance.InitializedFoodData();
            //GuestManager.Instance.Reset();

        }




    }

    [System.Serializable]
    public struct MoneyData
    {
        [ArrayElementTitle()]
        public int[] Money;

        public int Index;
        [HideInInspector]

        public static string[] unitMoney = new string[] { "\0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X" , "Y", "Z",
        "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ" , "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY" , "AZ",
        "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ" , "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY" , "BZ",
        "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ" , "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY" , "CZ",};

        public MoneyData Theorem()
        {
            for (int i = 0; i < Money.Length; i++)
            {
                if (Money[i] > 0)
                    Index = i;
            }

            for (int i = 0; i <= Index; i++)
            {
                if (Money[i] >= 1000)
                {
                    Money[i] -= 1000;
                    Money[i + 1] += 1;
                }

                if (Money[i] < 0)
                {
                    if (Index >= 1)
                    {
                        Money[i + 1] -= 1;
                        Money[i] += 1000;
                    }
                }
            }

            for (int i = 0; i < Money.Length; i++)
            {
                if (Money[i] > 0)
                    Index = i;
            }
            return this;
        }
    }
}