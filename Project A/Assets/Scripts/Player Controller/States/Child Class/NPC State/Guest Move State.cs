using UnityEngine;
using PathFinding;
using CKProject.TriggerSystem;
using CKProject.Managers;
using CKProject.Interactable;

namespace CKProject.FSM
{
    public class GuestMoveState : GuestBaseState
    {
        public void MoveStart()
        {
            GuestFSM.MoveStart();
        }

        private CustomTrigger customTrigger;

        // 음식 받을 대기 중 
        [VisibleEnum(typeof(EGuestStateType))]
        public void CheckTriggerArea(int stateType)
        {
            customTrigger = TriggerManager.Instance.CheckTriggerZone(transform);
            if (customTrigger != null && !GuestFSM.GetOut)
            {
                GuestFSM.Chair = customTrigger.GetComponent<Chair>();
                //customTrigger.GetComponent<Table>()?.EnterGuest(GuestFSM);
                GuestManager.Instance.SetWaitingOrder(gameObject);
                GuestFSM.ChangeState((EGuestStateType)stateType);
            }
        }

        [VisibleEnum(typeof(EGuestStateType))]
        public void CheckDoor(int stateType)
        {
            if (enabled)
            {
                if (GuestFSM.GetPathIndex())
                {
                    GuestFSM.ChangeState((EGuestStateType)stateType);
                    GuestManager.Instance.OutGuest(GuestFSM);
                    //GuestManager1.Instance.OutGuest(GuestFSM);
                }
            }
        }
    }
}