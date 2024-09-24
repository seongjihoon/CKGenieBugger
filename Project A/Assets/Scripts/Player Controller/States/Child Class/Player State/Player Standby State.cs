using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.FSM
{
    public class PlayerStandbyState : PlayerBaseState
    {
        private const float readyTimer = 0.5f;

        private float currentTimer = 0;
        public override void Enter()
        {
            //playerFSM.rigidbody.velocity = Vector3.zero;
            currentTimer = 0;
            base.Enter();
        }

        [VisibleEnum(typeof(EStateType))]
        public void NextState(int stateType)
        {
            currentTimer += Time.deltaTime;
            if(currentTimer > readyTimer)   
                playerFSM.ChangeState((EStateType)stateType);
        }

    }

}