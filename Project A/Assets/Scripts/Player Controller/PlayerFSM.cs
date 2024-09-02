using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace CKProject
{
    [SerializeField]
    public enum StateType
    {
        Idle = 1,
        Move = 2,
        Attack = 3,
        
    }

    [CustomEditor(typeof(PlayerFSM))]
    public class PlayerFSMEditor : Editor
    {
        private PlayerFSM fsmEditor;
        private void OnEnable()
        {
            fsmEditor = (PlayerFSM)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();


            if (GUILayout.Button("상태 추가", GUILayout.Width(120), GUILayout.Height(30)))
            {
                fsmEditor.AddState();
            }

            //if (GUILayout.Button("제거", GUILayout.Width(120), GUILayout.Height(30)))
            //{
            //    fsmEditor.SubState();
            //}

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

        }
    }

    public class PlayerFSM : FSMController<StateType>
    {

        // Start is called before the first frame update
        void Start()
        {
            base.Start();
        }

        public void AddState()
        {
            // 모든 상태 적용 하자.
            int i = 0;
            BaseState<StateType>[] childState = transform.GetComponents<BaseState<StateType>>(); 
            
            ClearStates();

            for (; i < childState.Length; i++)
            {
                if (childState[i] != null)
                {
                    AddState(childState[i].StateType, childState[i]);
                }
            }

        }

        public void SubState()
        {   

        }

        [VisibleEnum(typeof(StateType))]
        public void ChangeState(int nextState)
        {
            ChangeState((StateType)nextState);
        }

        public override void ChangeState(StateType updateStateType)
        {
            base.ChangeState(updateStateType);
        }
    }

}