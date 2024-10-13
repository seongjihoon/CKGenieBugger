using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.FSM
{
    public class PlayerBaseState : BaseState<EStateType>
    {
        //public PlayerFSM fsmController;

        [HideInInspector] public PlayerFSM playerFSM;
        private void Awake()
        {
            FsmController = GetComponent<PlayerFSM>();
            playerFSM = FsmController as PlayerFSM;
        }

        [VisibleEnum(typeof(EStateType))]
        public virtual void KeyAction(int inputKey)
        {

        }


    }

}