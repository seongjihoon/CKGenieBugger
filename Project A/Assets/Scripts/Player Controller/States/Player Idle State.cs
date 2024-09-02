using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;   

namespace CKProject
{

    public class PlayerIdleState : BaseState<StateType>
    {
        #region Private Values

        private float currentTime = 0;

        #endregion


        #region Events

        public override void Enter()
        {
            bEndState = false;
            base.Enter();
        }

        public override void Exit()
        {
            bEndState = false;
            base.Exit();
        }

        // 기다렸다가 GuestQueue가 0이 아닐 경우 다음 일 실행
        public void CheckGuest()
        {

        }


        #endregion


    }


}