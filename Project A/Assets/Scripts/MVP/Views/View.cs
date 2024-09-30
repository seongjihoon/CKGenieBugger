using UnityEngine;
using UnityEngine.UI;
using System;

namespace MVP
{
    // 뷰는 업데이트만 해주기
    public class View : MonoBehaviour
    {
        #region Example
        //[SerializeField, Header("UI Slider")] private Slider hpSlider;
        //[SerializeField] private Slider mpSlider;

        //[SerializeField, Header("UI Text")] private Text hpText;
        //[SerializeField] private Text mpText;

        //public void Initialized(Model model)
        //{
        //    //model.PropertyChanged += OnModelPropertyChanged;
        //    model.hpChanged += SetHp;
        //    model.mpChanged += SetMp;
        //}

        //public void SetHp(int hp)
        //{
        //    hpSlider.value = hp;
        //    hpText.text = hp.ToString();
        //}
        //public void SetMp(int mp)
        //{
        //    mpSlider.value = mp;
        //    mpText.text = mp.ToString();
        //}
        #endregion

        [Header("Money Text")]
        public Text MyMoneyText;

        public void MyMoneyToString(int[] Money, int index, string[] unit)
        {
            float a = Money[index];

            if (index > 0)
            {
                float b = Money[index - 1];
                a += b / 1000;
            }

            if (index == 0)
            {
                a += 0;
            }

            string p;
            p = (float)(Math.Truncate(a * 100) / 100) + unit[index];
            MyMoneyText.text = p;

        }
    }
}