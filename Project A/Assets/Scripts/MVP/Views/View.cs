using UnityEngine;
using UnityEngine.UI;
using System;

namespace MVP
{
    // ��� ������Ʈ�� ���ֱ�
    public class View : MonoBehaviour
    {

        [Header("Money Text")]
        public Text MyMoneyText;

        public void UpdateMoneyText(string p)
        {
            MyMoneyText.text = p;

        }
    }
}