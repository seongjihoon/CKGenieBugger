using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.Managers;
using CKProject.FSM;
using PathFinding;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using System;

namespace CKProject.FSM
{
    public class GuestReadyState : GuestBaseState
    {
        // 메서드 명은 임시
        [VisibleEnum(typeof(EGuestStateType))] 
        public void GetOutRestaurant(int stateType)
        {
            // 내부 구현은 어떻게 해놓을까...
            // 특정 조건이 만족되면 퇴장하는 메서드로 할 예정.
            if(enabled && GuestFSM.GuestState == EGuestStateType.Ready)
            {
                if(GuestFSM.GetOut) // 특수 조건을 FSM에서 체크하도록 설계
                {
                    GuestFSM.target = GuestManager.Instance.HidePoint;
                    // 의자 풀에 넣어줘야함
                    GuestManager.Instance.ReturnEmptyChair(GuestFSM.Chair.gameObject);

                    GuestFSM.GuestState = (EGuestStateType)stateType;
                    GuestFSM.Animator.SetTrigger("Sit End");
                    StandUpAnim((EGuestStateType)stateType).Forget();
                }
            }
        }

        async UniTaskVoid StandUpAnim(EGuestStateType stateType)
        {
            //GuestFSM.Animator.SetTrigger("Sit End");
            //Debug.Log("AA");
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            GuestFSM.ChangeState(stateType);
        }
    }
}
