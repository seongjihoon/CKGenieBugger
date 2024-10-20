using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.UI;
using CKProject.Managers;

namespace MVP.Mission
{
    public class MissionView : MonoBehaviour
    {
        public GameObject Contents;
        public MissionPanel[] Missions;
        public MissionPanel MissionPrefab;


        // MissionManager에서 현 스테이지의 미션을 확인하고 미션 개수만큼 오브젝트 풀을 생성.
        // 
        public void Initialized(int Count)
        {
            Missions = new MissionPanel[Count];
            for(int i =0; i < Count; i++)
            {
                Missions[i] = Instantiate(MissionPrefab);
                Missions[i].transform.parent = Contents.transform;
                Missions[i].gameObject.SetActive(false);
                //Missions[i].InitPanel(MissionManager.Instance.);
            }
            InitPanels();
        }

        public void InitPanels()
        {
            MissionManager.MissionData[] MissionDatas = MissionManager.Instance.GetStageMission();
            for (int i= 0; i < MissionDatas.Length; i++)
            {
                Missions[i].gameObject.SetActive(true);
                Missions[i].InitPanel(MissionDatas[i], FoodManager.Instance.UseKitchens[MissionDatas[i].Get_Type - 1], MissionDatas[i]);
            }
        }






    }
}