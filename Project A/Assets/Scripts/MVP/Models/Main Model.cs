using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.AttributeEditor;

namespace CKProject.UI
{

    public class MainModel : MonoBehaviour
    {
        public MoneyData MoneyData;

        public int[] Money { get { return MoneyData.Money; } set { MoneyData.Money = value; } }
        public int Index { get { return MoneyData.Index; } set { MoneyData.Index = value; } }

        public string[] unitMoney { get { return unitMoney; } }
        
            
        public int Size;

        private void Start()
        {
            MoneyData.Money = new int[Size];
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

    }
}
