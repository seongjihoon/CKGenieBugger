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

        [VisibleEnum(typeof(EGuestStateType))]
        public void CheckTriggerArea(int stateType)
        {
            customTrigger = TriggerManager.Instance.CheckTriggerZone(transform);
            if (customTrigger != null)
            {
                customTrigger.GetComponent<Table>()?.EnterGuest(GuestFSM);
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
                }
            }
        }
    }
}