using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.InputSystem;


namespace CKProject.FSM
{
    [SerializeField]
    public enum EStateType
    {
        Idle = 1,
        Move = 2,
        Interact = 3,
        
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

    public class PlayerFSM : FSMController<EStateType>
    {
        //private DefaultInputActions defaultPlayerActions;
        // 세 가지 키 타입.
        public InputAction moveAction;
        public InputAction interactAction;

        // Start is called before the first frame update    
        void Start()
        {
            base.Start();
            //defaultPlayerActions = new DefaultInputActions();

            moveAction = InputSystem.actions["Move"];
            interactAction = InputSystem.actions["Interact"];

        }

        public void AddState()
        {
            // 모든 상태 적용 하자.
            int i = 0;
            BaseState<EStateType>[] childState = transform.GetComponents<BaseState<EStateType>>(); 
            
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

        [VisibleEnum(typeof(EStateType))]
        public void ChangeState(int nextState)
        {
            ChangeState((EStateType)nextState);
        }

        public override void ChangeState(EStateType updateStateType)
        {
            base.ChangeState(updateStateType);
        }
    }

}