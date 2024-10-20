using UnityEngine;
using UnityEngine.UI;
using System;

namespace MVP
{
    // 뷰는 업데이트만 해주기
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