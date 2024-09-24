using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;

namespace CKProject.FSM
{
    public class GuestBaseState : BaseState<EGuestStateType>
    {
        public Unit GuestFSM;
        private void Awake()
        {
            FsmController = GetComponent<Unit>();
            GuestFSM = FsmController as Unit;
        }
    }
}