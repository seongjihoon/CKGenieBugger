using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 3�ڸ� ���� A ~ Z ������ �ؼ� �� 78�ڸ��� ���� ǥ���� �� �ְ� ���� ����.
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
        // �� �ڻ��� ���� ���°��� �� �� �ֵ��� �ϴ� �ڵ�
        for(int i = 0; i < size; i++)
        {
            if (Money[i] > 0)
            {
                index = i;
            }
        }

        // index �� ��ŭ �� ������ �����ϴ� �ݺ����� ������.
        for(int i = 0; i <= index; i++)
        {
            // ����, i��° �迭�� ���� 1000 �̻��̶��
            // �ű⼭ 1000�� ���� �� �迭�� 1�� �����ش�.
            if (Money[i] >= 1000)
            {
                Money[i] -= 1000;
                Money[i + 1] += 1; 
            }

            // ����, i��° �迭�� ���� �������
            if (Money[i] < 0)
            {
                // ����, i�� ���� ���� ���� �ڻ��� ������ ������
                // �� �迭���� 1�� ���� ������ i���� �迭�� 1000�� ���Ѵ�.
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
        // �迭�� �ִ� ���� �÷��̾ �� �� �ִ� ��ȭ�� ���·� ǥ��.
        float a = Money[index];
        // ����, index�� 0���� ũ�ٸ� �Ҽ����� ���´ٴ� ��.
        if(index > 0 )
        {
            float b = Money[index - 1];
            a += b / 1000;
        }

        if(index == 0)
        {
            a += 0;
        }

        // �ڷ������� 65���� A�� ǥ���ϱ� ������ ���� �ڵ�
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
