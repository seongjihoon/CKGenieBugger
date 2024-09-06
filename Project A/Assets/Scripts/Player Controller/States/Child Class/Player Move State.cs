using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.FSM
{
    public class PlayerMoveState : PlayerBaseState
    {
        // 어떤 매서드를 넣어야 할까?
        // 이동을 체크하는 매서드
        // 이동이 완료되면 다음 행동을 할 메서드

        #region Public Values
        private PlayerFSM playerFSM;

        #endregion

        public override void Enter()
        {
            playerFSM = FsmController as PlayerFSM;
            base.Enter();
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void KeyAction(int inputKey)
        {
            //base.KeyInput(inputKey);
            switch ((EStateType)inputKey)
            {
                case EStateType.Idle:
                    KeyUp((EStateType)inputKey);
                    break;
                case EStateType.Move:

                    break;
                case EStateType.Interact:
                    InputInteractKey((EStateType)inputKey);
                    break;
                default:
                    break;
            }
        }

        #region public Methods
        public void Move()
        {
            transform.Translate(ConvertVecYToZ(playerFSM.moveAction.ReadValue<Vector2>()) * 0.05f);
        }


        #endregion

        #region private Methods
        private Vector3 ConvertVecYToZ(Vector2 origin)
        {
            return Vector3.right * origin.x + Vector3.forward * origin.y;
        }

        /// <summary>
        /// 이동을 담당하는 함수
        /// </summary>
        /// <param name="stateType"></param>
        private void KeyUp(EStateType stateType)
        {
            //if(Mathf.Abs( playerFSM.moveAction.ReadValue<Vector2>().x) < 0.2f
            //    && Mathf.Abs(playerFSM.moveAction.ReadValue<Vector2>().y) < 0.2f)
            if(!playerFSM.moveAction.IsPressed())
            {
                FsmController.ChangeState(stateType);
            }
        }

        /// <summary>
        /// 상호작용을 담당하는 함수
        /// </summary>
        /// <param name="stateType"></param>
        private void InputInteractKey(EStateType stateType)
        {
            if(playerFSM.interactAction.WasPressedThisFrame())
            {
                Debug.Log($"Press Interaction(move)");
                //FsmController.ChangeState(stateType);
            }
        }

        #endregion


    }

}