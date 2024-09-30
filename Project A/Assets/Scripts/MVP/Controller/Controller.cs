using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVP
{
    /// <summary>
    /// 입력에 따른 기능과 그에 따른 결과를 Model과 View에 전달하는 기능을 함.
    /// </summary>
    public class Controller : MonoBehaviour
    {
        #region Example
        //[SerializeField] Model model;
        //[SerializeField] View view;

        //private void Start()
        //{
        //    view.Initialized(model);
        //}

        //private void ChangeState(bool isPlus, ref int state, int maxState, System.Action<int> update)
        //{
        //    int changeState = isPlus ? 10 : -10;
        //    state = Mathf.Clamp(state + changeState, 0, maxState);
        //    update(state);
        //}

        //public void ClickHp(bool isPlus)
        //{
        //    ChangeState(isPlus, ref model.hp, 100, view.SetHp);
        //}

        //public void ClickMp(bool isPlus)
        //{
        //    ChangeState(isPlus, ref model.mp, 100, view.SetMp);
        //}
        #endregion
        public Model Model;
        public View View;

        private void MoneyUpdate(ref int[] money)
        {
            Theorem();
            View.MyMoneyToString(money, Model.index, Model.unitMoney);
        }

        private void Theorem()
        {
            for (int i = 0; i < Model.Size; i++)
            {
                if (Model.Money[i] > 0)
                    Model.index = i;
            }

            for (int i = 0; i <= Model.index; i++)
            {
                if (Model.Money[i] >= 1000)
                {
                    Model.Money[i] -= 1000;
                    Model.Money[i + 1] += 1;
                }

                if (Model.Money[i] < 0)
                {
                    if (Model.index > 1)
                    {
                        Model.Money[i + 1] -= 1;
                        Model.Money[i] += 1000;
                    }
                }
            }
        }

        public void AddMoney(int money)
        {
            int index = 0;
            int memo = 0;
            while (money > 1000)
            {
                memo = money % 1000;
                Model.Money[index] += memo;
                index++;
                money /= 1000;
            }
            Model.Money[index] += memo;
            Theorem();
            View.MyMoneyToString(Model.Money, Model.index, Model.unitMoney);
        }

        public void AddMoney(int[] getMoney)
        {
            for (int i = 0; i < getMoney.Length; i++)
            {
                Model.Money[i] += getMoney[i];
            }
            MoneyUpdate(ref Model.Money);
        }

        public void SubMoney(int[] useMoney)
        {
            for (int i = 0; i < useMoney.Length; i++)
            {
                Model.Money[i] -= useMoney[i];
            }
            MoneyUpdate(ref Model.Money);
        }
    }

}