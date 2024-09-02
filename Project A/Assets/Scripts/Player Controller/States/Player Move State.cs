using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject
{
    public class PlayerMoveState : BaseState<StateType>
    {
        // 어떤 매서드를 넣어야 할까?
        // 이동을 체크하는 매서드
        // 이동이 완료되면 다음 행동을 할 메서드

        #region Public Values
        //public bool MoveSuccess= false;

        #endregion

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

        public void Movement()
        {

        }

    }

}