using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.FSM
{
    public class CashierBaseState : BaseState<EStateType>
    {
        protected CashierFSM cashierFSM;


        private void Awake()
        {
            FsmController = GetComponent<CashierFSM>();
            cashierFSM = FsmController as CashierFSM;
        }
    }
}