using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.FSM
{
    public class PlayerBaseState : BaseState<EStateType>
    {
        //public PlayerFSM fsmController;

        private void Awake()
        {
            FsmController = GetComponent<PlayerFSM>();
        }

        [VisibleEnum(typeof(EStateType))]
        public virtual void KeyAction(int inputKey)
        {

        }


    }

}