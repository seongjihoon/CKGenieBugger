using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using CKProject.Interactable;

namespace CKProject.FSM
{

    public class PlayerIdleState : PlayerBaseState
    {
        #region Private Values

        //private float currentTime = 0;

        #endregion

        #region Public Methods

        // 초기화
        public override void Enter()
        {
            //playerFSM.rigidbody.velocity = Vector3.zero;

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
                        {
                            if (playerFSM.FoodObject == null)
                                InputInteractKey((EStateType)inputKey);
                            else
                                InputThrowInteractKey((EStateType)inputKey);
                        }
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
            if(playerFSM.moveAction.IsPressed())
            {
                FsmController.ChangeState(stateType);
            }
        }

        private void InputInteractKey(EStateType stateType)
        {
            if(playerFSM.interactAction.WasPressedThisFrame())
            {
                if(playerFSM.CustomCollision != null )
                {
                    playerFSM.CustomCollision.PlayInteractionEvent();
                    //playerFSM.FoodObject = playerFSM.CustomCollision.GetComponent<Kitchen>().Interaction();
                    //if (playerFSM.FoodObject != null)
                    //    GetFoodObject();
                }
            }
        }

        //private void GetFoodObject()
        //{
        //    playerFSM.FoodObject.transform.parent = this.transform;
        //    playerFSM.FoodObject.transform.position = transform.position + Vector3.up * 1.0f;
        //}

        private void InputThrowInteractKey(EStateType stateType)
        { 
            if(playerFSM.interactAction.WasPressedThisFrame())
            {
                playerFSM.ChangeState(EStateType.Interact);
            }
        }

        #endregion



    }


}