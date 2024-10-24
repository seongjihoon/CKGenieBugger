using UnityEngine;
using System.Collections.Generic;
using PathFinding;
using System.Collections;
using CKProject.Managers;
using Grid = PathFinding.Grid;
using CKProject.Interactable;
using CKProject.UI;

namespace CKProject.FSM
{
    public class CashierFSM : FSMController<EStateType>
    {
        [HideInInspector] public Transform Target;
        [HideInInspector] public Transform Guest;
        [HideInInspector] public Grid Grid;
        public Vector3[] Path;
        [HideInInspector] public OrderData NowOrder;
        

        public List<Transform> CounterTop = new List<Transform>();



        public float Speed = 2;
        [HideInInspector] public GameObject GetOnFood;
        [HideInInspector] public int TargetIndex;

        protected override void Start()
        {
            base.Start();
            // �Է��� �ȹ��� ����.
            // ������ AI�� ���

        }

        protected override void Update()
        {
            base.Update();

        }

        public void AddState()
        {
            // ��� ���� ���� ����.
            int i = 0;
            BaseState<EStateType>[] childState = transform.GetComponents<BaseState<EStateType>>();

            ClearStates();

            for (; i < childState.Length; i++)
            {
                if (childState[i] != null)
                {
                    AddState(childState[i].StateType, childState[i]);
                }
            }
        }

        public void DeliveryFood()
        {
            NowOrder.FoodType = EFoodType.None;
            NowOrder.OrderTarget.GetComponent<Unit>().GetOut = true;
            GameManager.Instance.AddMoney(FoodManager.Instance.GetFoodLevelData(GetOnFood.GetComponent<Food>().GetFoodSo.foodType).Revenue);
            GetOnFood.SetActive(false);
            GetOnFood = null; 
            //GuestManager.Instance.ReturnEmptyChair(Target.Chair.gameObject);
            // ������ ���������� Guest�� ����.
        }



        private void OnDrawGizmos()
        {
            if (Path != null)
            {
                for (int i = TargetIndex; i < Path.Length; i++)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(Path[i], Vector3.one);

                    if (i == TargetIndex)
                    {
                        Gizmos.DrawLine(transform.position, Path[i]);
                    }
                    else
                    {
                        Gizmos.DrawLine(Path[i - 1], Path[i]);
                    }
                }
            }
        }

    }
}