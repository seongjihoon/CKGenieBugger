using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.FSM
{
    public class PlayerStandbyState : PlayerBaseState
    {
        [HideInInspector, ReadOnly]
        public PlayerFSM playerFSM;

        private const float readyTimer = 0.5f;

        private float currentTimer = 0;
        public override void Enter()
        {
            playerFSM = FsmController as PlayerFSM;
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