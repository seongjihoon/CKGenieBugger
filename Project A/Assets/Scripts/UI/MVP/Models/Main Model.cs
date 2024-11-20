using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.AttributeEditor;
using CKProject.Managers;

namespace CKProject.UI
{

    public class MainModel : MonoBehaviour
    {
        public MoneyData MoneyData { get { return GameManager.Instance.moneyData; } set { GameManager.Instance.moneyData = value; } }
        public int[] Money { get { return GameManager.Instance.moneyData.Money; } set { GameManager.Instance.moneyData.Money = value; } }
        public int Index { get { return GameManager.Instance.moneyData.Index; } set { GameManager.Instance.moneyData.Index = value; } }

        public string[] unitMoney { get { return MoneyData.unitMoney; } }
        private string userName;
        public string UserName { get { return userName; } private set { userName = value; } }
        private string userID;
        public string UserID { get { return userID; } private set { userID = value; } }

        public int Crystals = 0;

        private void Start()
        {
            GameManager.Instance.moneyData.Money = new int[GameManager.Instance.Size];
        }

        public void UserInfoSet(string userName, string userID)
        {
            UserName = userName;
            UserID = userID;
        }
    }

}
