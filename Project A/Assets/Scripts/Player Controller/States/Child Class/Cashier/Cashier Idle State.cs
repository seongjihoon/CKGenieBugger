using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.Managers;
using PathFinding;
using CKProject.Interactable;

namespace CKProject.FSM
{
    /// <summary>
    /// ����: Manager���� üũ�Ǵ� �ֹ� ��� ���� Guest Queue�� ������� �ʴ� ��� ���� �ൿ�� ����
    /// </summary>
    public class CashierIdleState : CashierBaseState
    {

        public override void Enter()
        {
            base.Enter();

        }

        public override void ExcuteFixedUpdate()
        {
            base.ExcuteFixedUpdate();

        }

        public override void ExcuteUpdate()
        {
            base.ExcuteUpdate();

        }

        public override void Exit()
        {
            base.Exit();

        }

        private void Serving()
        {
            cashierFSM.Target = cashierFSM.Guest.GetComponent<Unit>().Chair.Table;
        }

        private void GetOrder()
        {
            cashierFSM.Guest = GuestManager.Instance.GetWaitingOrderGuest().transform;
            cashierFSM.Target = cashierFSM.Guest.GetComponent<Unit>().Chair.Table;
        }
        
        private void GetFood()
        {
            cashierFSM.NowOrder = GuestManager.Instance.GetWaitingFoodGuest();
            cashierFSM.Guest = cashierFSM.NowOrder.OrderTarget.transform;
            // ���� Kitchen���� �̵��ؼ� ������ ��������
            if (cashierFSM.NowOrder.FoodType == EFoodType.ALL)
                cashierFSM.Target = cashierFSM.CounterTop[Random.Range(0, cashierFSM.CounterTop.Count)];
            else
                cashierFSM.Target = cashierFSM.CounterTop[(int)cashierFSM.NowOrder.FoodType - 1];

        }

        [VisibleEnum(typeof(EStateType))]
        public void CheckGuest(int eType)
        {
            if(enabled)
            {
                // ������ ��� �ִ� ���¸� ������ �����ؾ��� ��󿡰� �켱 �����ϱ�
                if (cashierFSM.GetOnFood != null)
                {
                    Serving();
                    cashierFSM.ChangeState((EStateType)eType);
                }
                // �ֹ� ��� ���� �մԺ��� �ֹ� �ޱ�
                else if (GuestManager.Instance.GetGuestList.WaitingOrderGuest.Count > 0)
                {
                    GetOrder();
                    cashierFSM.ChangeState((EStateType)eType);
                }
                // �ֹ� ��� ���� �մ��� ������ ������ ���� ��󿡰� ������ �����ϱ�
                else if (GuestManager.Instance.OrderQueue.Count > 0)
                {
                    GetFood();
                    cashierFSM.ChangeState((EStateType)eType);
                }
            }
        }

        //private bool CheckWaitingOrderGuest()
        //{
        //    // �ֹ� ��� ���� �մԺ��� �ֹ� �ޱ�
        //    if(GuestManager.Instance.GetGuestList.WaitingOrderGuest.Count > 0)
        //    {
        //        return true;
        //    }
        //    // �ֹ� ��� ���� �մ��� ������ ���� �����ϱ�
        //    return false;
        //}

        //private bool CheckWatingFoodGuest()
        //{

        //    if (GuestManager.Instance.GetGuestList.WaitingOrderGuest.Count > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

    }
}