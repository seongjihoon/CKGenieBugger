using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 3자리 마다 A ~ Z 단위로 해서 총 78자리의 수를 표현할 수 있게 만들 예정.
    public int[] Money;
    public int index;
    public int[] getMoney;
    public Text MymoneyText;
    private string[] unitMoney = new string[] { "\0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X" , "Y", "Z",
        "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ" , "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY" , "AZ",
        "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ" , "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY" , "BZ",
        "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ" , "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY" , "CZ",};
    public int size;
    private void Start()
    {
        Money = new int[size];
        getMoney = new int[size];
    }

    private void Update()
    {

        Theorem();
        MymoneyToString();
    }

    public void UpMoney()
    {
        for (int i = 0; i < size; i++)
        {
            Money[i] += getMoney[i];
        }
    }

    private void Theorem()
    {
        // 내 자산의 현재 상태값을 알 수 있도록 하는 코드
        for(int i = 0; i < size; i++)
        {
            if (Money[i] > 0)
            {
                index = i;
            }
        }

        // index 값 만큼 돈 단위를 정리하는 반복문을 돌린다.
        for(int i = 0; i <= index; i++)
        {
            // 만약, i번째 배열에 돈이 1000 이상이라면
            // 거기서 1000을 빼고 윗 배열에 1을 더해준다.
            if (Money[i] >= 1000)
            {
                Money[i] -= 1000;
                Money[i + 1] += 1; 
            }

            // 만약, i번째 배열의 값이 음수라면
            if (Money[i] < 0)
            {
                // 만약, i의 값이 나의 현재 자산의 값보다 작으면
                // 윗 배열에서 1을 빼고 음수인 i번재 배열에 1000을 더한다.
                if(index > 1 )
                {
                    Money[i + 1] -= 1;
                    Money[i] += 1000;
                }
            }
        }
    }

    private string MymoneyToString()
    {
        // 배열에 있는 값을 플레이어가 볼 수 있는 재화의 형태로 표현.
        float a = Money[index];
        // 만약, index가 0보다 크다면 소수점이 나온다는 것.
        if(index > 0 )
        {
            float b = Money[index - 1];
            a += b / 1000;
        }

        if(index == 0)
        {
            a += 0;
        }

        // 자료형에서 65부터 A를 표현하기 때문에 쓰는 코드
        string unit = string.Empty;
        if(index > 0)
            unit += (char)(65 + index -1);
        //if (index > 0)
        //{
        //    int temp = index;
        //    while(temp > 0)
        //    {
        //        if(temp > 25)
        //        {
        //            unit += (char)(65 + (temp - 1) % 26 );
        //        }
        //        else
        //        {
        //            unit += (char)(65 + temp -1 );
        //        }
        //        temp -= 26;
        //    }
        //    //unit = (65 + index - 1).ToString();

        //}


        string p;
        p = (float)(Math.Truncate(a * 100) / 100) + unit.ToString();
        MymoneyText.text = p;

        return p;

        
    }

    
}
