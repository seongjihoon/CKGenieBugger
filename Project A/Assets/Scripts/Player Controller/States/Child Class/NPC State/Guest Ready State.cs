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
        // �޼��� ���� �ӽ�
        [VisibleEnum(typeof(EGuestStateType))] 
        public void GetOutRestaurant(int stateType)
        {
            // ���� ������ ��� �س�����...
            // Ư�� ������ �����Ǹ� �����ϴ� �޼���� �� ����.
            if(enabled && GuestFSM.GuestState == EGuestStateType.Ready)
            {
                if(GuestFSM.GetOut) // Ư�� ������ FSM���� üũ�ϵ��� ����
                {
                    GuestFSM.target = GuestManager.Instance.HidePoint;
                    // ���� Ǯ�� �־������
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
