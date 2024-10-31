using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;

namespace CKProject.FSM
{
    public class GuestIdleState : GuestBaseState
    {

        [VisibleEnum(typeof(EGuestStateType))]
        public void IsGetPath(int stateType)
        {
            if (GuestFSM.target != null)
            {
                GuestFSM.GuestState = (EGuestStateType)stateType;
                GuestFSM.ChangeState((EGuestStateType)stateType);
            }
        }
    }
}