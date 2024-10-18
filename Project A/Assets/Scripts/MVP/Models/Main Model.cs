using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.AttributeEditor;
using CKProject.Managers;

namespace CKProject.UI
{

    public class MainModel : MonoBehaviour
    {
        public int[] Money { get { return GameManager.Instance.moneyData.Money; } set { GameManager.Instance.moneyData.Money = value; } }
        public int Index { get { return GameManager.Instance.moneyData.Index; } set { GameManager.Instance.moneyData.Index = value; } }

        public string[] unitMoney { get { return MoneyData.unitMoney; } }
        

        private void Start()
        {
            GameManager.Instance.moneyData.Money = new int[GameManager.Instance.Size];
        }
    }


    [System.Serializable]
    public struct MoneyData
    {
        [ArrayElementTitle()]
        public int[] Money;

        [HideInInspector]
        public int Index;
        [HideInInspector]

        public static string[] unitMoney = new string[] { "\0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X" , "Y", "Z",
        "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ" , "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY" , "AZ",
        "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ" , "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY" , "BZ",
        "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ" , "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY" , "CZ",};

        public void Theorem()
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
                    if (Index > 1)
                    {
                        Money[i + 1] -= 1;
                        Money[i] += 1000;
                    }
                }
            }
        }
    }
}
