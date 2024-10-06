using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.Managers;
using PathFinding;
using CKProject.Interactable;

namespace CKProject.FSM
{
    /// <summary>
    /// 로직: Manager에서 체크되는 주문 대기 중인 Guest Queue가 비어있지 않는 경우 다음 행동을 실행
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
            // 이제 Kitchen으로 이동해서 음식을 가져오자
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
                // 음식을 들고 있는 상태면 음식을 전달해야할 대상에게 우선 전달하기
                if (cashierFSM.GetOnFood != null)
                {
                    Serving();
                    cashierFSM.ChangeState((EStateType)eType);
                }
                // 주문 대기 중인 손님부터 주문 받기
                else if (GuestManager.Instance.GetGuestList.WaitingOrderGuest.Count > 0)
                {
                    GetOrder();
                    cashierFSM.ChangeState((EStateType)eType);
                }
                // 주문 대기 중인 손님이 없으면 음식을 받을 대상에게 음식을 전달하기
                else if (GuestManager.Instance.OrderQueue.Count > 0)
                {
                    GetFood();
                    cashierFSM.ChangeState((EStateType)eType);
                }
            }
        }

        //private bool CheckWaitingOrderGuest()
        //{
        //    // 주문 대기 중인 손님부터 주문 받기
        //    if(GuestManager.Instance.GetGuestList.WaitingOrderGuest.Count > 0)
        //    {
        //        return true;
        //    }
        //    // 주문 대기 중인 손님이 없으면 음식 전달하기
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