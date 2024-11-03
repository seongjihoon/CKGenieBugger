using CKProject.Interactable;
using CKProject.Managers;
using CKProject.TriggerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.FSM
{
    public class CashierMakingState : CashierBaseState
    {
        public override void Enter()
        {
            base.Enter();
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
        public void MakingCheck(int eType)
        {
            customTrigger = GameManager.Instance.TriggerManager.CheckTriggerZone(transform);
            Kitchen kitchen = customTrigger.GetComponent<Kitchen>();
            if(customTrigger != null && kitchen.GetKitchenType == EKitchenType.Complet)
            {
                cashierFSM.GetOnFood = kitchen.Interaction();
                cashierFSM.ChangeState((EStateType)eType);
            }
        }
    }
}