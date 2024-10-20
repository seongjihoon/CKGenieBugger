using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.Managers;

namespace MVP.Mission
{
    public class MissionPresenter : MonoBehaviour
    {
        public MissionModel Model;
        public MissionView View;


        // 어떨 때 사용해야할까?
        // 일단 Awake로 생성을 시작
        // Awake에서 생성 갯수를 파악하여 최대 갯수만큼 생성

        public void Initialized()
        {
            View.Initialized(MissionManager.Instance.MaxPoolCount);
            // 생성된

        }



    }
}
