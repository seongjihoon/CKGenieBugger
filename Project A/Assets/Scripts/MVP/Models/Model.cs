using UnityEngine;
using System;
using System.ComponentModel;

[System.Serializable]
public struct MoneyStruct
{
    public enum UNIT
    {
        NONE,
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
        AA, AB, AC, AD, AE, AF, AG, AH, AI, AJ, AK, AL, AM, AN, AO, AP, AQ, AR, AS, AT, AU, AV, AW, AX, AY, AZ,
        BA, BB, BC, BD, BE, BF, BG, BH, BI, BJ, BK, BL, BM, BN, BO, BP, BQ, BR, BS, BT, BU, BV, BW, BX, BY, BZ,
        CA, CB, CC, CD, CE, CF, CG, CH, CI, CJ, CK, CL, CM, CN, CO, CP, CQ, CR, CS, CT, CU, CV, CW, CX, CY, CZ,
    }
    [SerializeField]
    UNIT UnitMoney;

    [SerializeField]
    int Money;
}

public class Model : MonoBehaviour
{
    [ArrayElementTitle("")]
    public int[] Money;
    [HideInInspector]
    public int index;

    //public MoneyStruct[] MoneyStruct;
        [HideInInspector] 

    public string[] unitMoney = new string[] { "\0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X" , "Y", "Z",
        "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ" , "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY" , "AZ",
        "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ" , "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY" , "BZ",
        "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ" , "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY" , "CZ",};
    
    public int Size;

    private void Start()
    {
        Money = new int[Size];
    }

    #region Legarcy & Example

    //public Action<int> hpChanged;
    //public Action<int> mpChanged;
    //public int hp;
    //public int mp;

    //public Action<int> hpChanged;
    //public Action<int> mpChanged;

    //public int hp;
    //public int mp;
    //public int HP
    //{
    //    get => hp;

    //    set
    //    {
    //        if (hp != value)
    //        {
    //            hp = value;
    //            hpChanged?.Invoke(hp);
    //        }
    //    }
    //}

    //public int MP
    //{
    //    get => mp;
    //    set
    //    {
    //        if (mp != value)
    //        {
    //            mp = value;
    //            mpChanged?.Invoke(mp);
    //        }
    //    }
    //}

    #endregion


}
