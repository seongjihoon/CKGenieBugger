using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.SingleTon;

namespace CKProject.Managers
{
    /// <summary>
    /// 역할: CSV에 기록된 데이터들을 불러옴
    /// </summary>
    public class StageManager : SingleTon<StageManager>
    {
        private void Awake()
        {
            CreateInstance(this);
        }

        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }
    }
}