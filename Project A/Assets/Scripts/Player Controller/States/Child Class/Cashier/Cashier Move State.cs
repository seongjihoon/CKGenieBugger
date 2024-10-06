using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using Grid = PathFinding.Grid;
using CKProject.Managers;
using CKProject.TriggerSystem;
using CKProject.Interactable;

namespace CKProject.FSM
{
    public class CashierMoveState : CashierBaseState
    {
        public override void Enter()
        {
            base.Enter();
            PathRequestManager.RequestPath(new PathRequest(transform.position, cashierFSM.Target.position, OnPathFound));
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void ExcuteFixedUpdate()
        {
            base.ExcuteFixedUpdate();
        }

        public override void ExcuteUpdate()
        {
            base.ExcuteUpdate();
        }

        private CustomTrigger customTrigger;

        [VisibleEnum(typeof(EStateType))]
        public void CheckTriggerArea(int stateType)
        {
            if(cashierFSM.NowOrder.FoodType == EFoodType.None)
            {
                customTrigger = TriggerManager.Instance.CheckTriggerZone(transform);
                if (CustomTriggerCheck())
                {
                    GuestManager.Instance.SetWaitingFood(cashierFSM.Guest.GetComponent<Unit>());
                    StopCoroutine("FollowPath");

                    // 주문 완료
                    cashierFSM.ChangeState((EStateType)stateType);
                }
            }
        }

        [VisibleEnum(typeof(EStateType))]
        public void CheckKitchenArea(int stateType)
        {
            if(cashierFSM.NowOrder.FoodType != EFoodType.None && cashierFSM.GetOnFood == null)
            {
                customTrigger = TriggerManager.Instance.CheckTriggerZone(transform);
                if (CustomTriggerCheck())
                {
                    // 도착했으면 음식 만들기 시작 
                    StopCoroutine("FollowPath");

                    // 제작 시작
                    customTrigger.GetComponent<Kitchen>()?.Interaction();
                    cashierFSM.ChangeState((EStateType)stateType);
                }
            }
        }

        [VisibleEnum(typeof(EStateType))]
        public void DeliveryFood(int stateType)
        {
            if(cashierFSM.GetOnFood != null)
            {
                customTrigger = TriggerManager.Instance.CheckTriggerZone(transform);
                if (CustomTriggerCheck())
                {
                    StopCoroutine("FollowPath");
                    cashierFSM.DeliveryFood();
                    cashierFSM.ChangeState((EStateType)stateType);
                }
            }
        }

        private bool CustomTriggerCheck()
        {
            if (customTrigger != null && customTrigger.transform == cashierFSM.Target)
                return true;
            return false;
        }

        #region PathFinding

        public void RequestPathGuest(GameObject chair, Grid gridInfo)
        {
            cashierFSM.Grid = gridInfo;
            cashierFSM.Target = chair.transform;
            PathRequestManager.RequestPath(new PathRequest(transform.position, cashierFSM.Target.position, OnPathFound));
        }

        public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
        {
            if (pathSuccessful)
            {
                //chasing = true;
                cashierFSM.TargetIndex = 0;
                cashierFSM.Path = newPath;
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }


        // 목표물의 이동이 감지되면 체크를 함.
        // Rect로 칸 당 범위를 생성하고 생성된 범위를 체크하는 형식
        private bool CheckTargetPosition()
        {
            // 타겟 노드
            Vector3 targetPos = cashierFSM.Grid.NodeFromWorldPoint(cashierFSM.Target.position).worldPosition;
            // 최종 목표
            Vector3 FinTarget = cashierFSM.Path[cashierFSM.Path.Length - 1];

            float distance = Mathf.Abs(Vector3.Distance(targetPos, FinTarget));

            // 1.5 * n
            if (distance > 1.5f * 4)
            {
                return true;
            }

            return false;
        }

        private bool GetPathIndex()
        {
            return cashierFSM.TargetIndex >= cashierFSM.Path.Length;
        }

        private IEnumerator FollowPath()
        {
            int count = 0;
            Vector3 currentWaypoint = cashierFSM.Path[0];
            while (true)
            {
                count++;
                if (transform.position == currentWaypoint)
                {
                    cashierFSM.TargetIndex++;
                    if (GetPathIndex())
                    {
                        yield break;
                    }
                    currentWaypoint = cashierFSM.Path[cashierFSM.TargetIndex];
                }
                if (count > 100000)
                {
                    transform.GetComponent<Unit>().enabled = false;
                    yield break;
                }
                //if (CheckTargetPosition())
                //{
                //    chasing = false;
                //    break;
                //}

                currentWaypoint.y = transform.position.y;

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, cashierFSM.Speed * Time.deltaTime);
                yield return null;
            }
        }
        #endregion
    }
}