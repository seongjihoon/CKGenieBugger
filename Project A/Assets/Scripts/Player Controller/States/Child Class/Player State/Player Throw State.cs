using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.Interactable;

namespace CKProject.FSM
{
    /// <summary>
    /// 이 State에서는 키 입력을 필요로 하지 않음.
    /// </summary>
    public class PlayerThrowState : PlayerBaseState
    {
        #region public Parameters

        private const float ThrowDelayTime = 0.5f;

        #endregion

        #region private Parameters

        private float currentTimer = 0;
        #endregion

        public override void Enter()
        {
            playerFSM = FsmController as PlayerFSM;
            //playerFSM.rigidbody.velocity = Vector3.zero;
            currentTimer = 0;
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        [VisibleEnum(typeof(EStateType))]
        public void ThrowObject(int stateType)
        {
            // 상호 작용 키를 누르면 바로 작동
            currentTimer += Time.deltaTime;
            if(currentTimer > ThrowDelayTime)
            {
                playerFSM.FoodObject.transform.parent = null;
                //playerFSM.FoodObject.SetActive(false);
                //playerFSM.FoodObject.GetComponent<Food>().Throw().Forget();
                playerFSM.FoodObject = null;
                playerFSM.ChangeState((EStateType) stateType);
            }
        }

    }

}