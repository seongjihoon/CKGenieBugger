using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;

namespace CKProject.FSM
{
    public class GuestBaseState : BaseState<EGuestStateType>
    {
        public NPCFSM GuestFSM;
        private void Awake()
        {
            FsmController = GetComponent<NPCFSM>();
            GuestFSM = FsmController as NPCFSM;
        }
    }
}