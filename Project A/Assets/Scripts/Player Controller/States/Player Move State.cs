using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject
{
    public class PlayerMoveState : BaseState<StateType>
    {
        // � �ż��带 �־�� �ұ�?
        // �̵��� üũ�ϴ� �ż���
        // �̵��� �Ϸ�Ǹ� ���� �ൿ�� �� �޼���

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