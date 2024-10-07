using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.Managers;
using CKProject.FSM;
using PathFinding;

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
            if(enabled)
            {
                if(GuestFSM.GetOut) // Ư�� ������ FSM���� üũ�ϵ��� ����
                {
                    GuestFSM.target = GuestManager.Instance.HidePoint;
                    // ���� Ǯ�� �־������
                    GuestManager.Instance.ReturnEmptyChair(GuestFSM.Chair.gameObject);
                    GuestFSM.ChangeState((EGuestStateType)stateType);
                    //GuestFSM.MoveStart();
                }
            }
        }
    }
}
