using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;   

namespace CKProject.FSM
{

    public class PlayerIdleState : PlayerBaseState
    {
        #region Private Values

        private float currentTime = 0;
        private PlayerFSM playerFSM;

        #endregion

        #region Public Methods

        // 초기화
        public override void Enter()
        {
            playerFSM = FsmController as PlayerFSM;
            
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }
        

        /// <summary>
        /// 물리 초기화
        /// </summary>
        public void ResetPhysics()
        {

        }


        /// <summary>
        /// 키 입력에 따른 리턴을 부가하는 함수
        /// 
        /// </summary>
        [VisibleEnum(typeof(EStateType))]
        public override void KeyAction(int inputKey)
        {
            if (playerFSM != null)
            {
                switch ((EStateType)inputKey)
                {
                    case EStateType.Idle:
                        break;
                    case EStateType.Move:
                        InputMoveKey((EStateType)inputKey);
                        break;
                    case EStateType.Interact:
                        InputInteractKey((EStateType)inputKey);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Private Methods

        private void InputMoveKey(EStateType stateType)
        {
            //if (Mathf.Abs(playerFSM.moveAction.ReadValue<Vector2>().x) > 0.2f
            //    || Mathf.Abs(playerFSM.moveAction.ReadValue<Vector2>().y) > 0.2f)
            //if(playerFSM.moveAction.WasPressedThisFrame())
            if(playerFSM.moveAction.IsPressed())
            {
                FsmController.ChangeState(stateType);
            }
        }

        private void InputInteractKey(EStateType stateType)
        {
            if(playerFSM.interactAction.WasPressedThisFrame())
            {
                Debug.Log($"Press Interaction(Idle)");
                //FsmController.ChangeState(stateType);
            }
        }
        #endregion



    }


}