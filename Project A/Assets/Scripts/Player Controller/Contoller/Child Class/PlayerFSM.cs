using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;
using CKProject.Managers;
using CKProject.TriggerSystem;
using CKProject.Interactable;


namespace CKProject.FSM
{
    [SerializeField]
    public enum EStateType
    {
        Idle = 1,
        Move = 2,
        Interact = 3,
        Throw = 4,
        Standby = 5,
        
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
        // 세 가지 키 타입.
        public InputAction moveAction;
        public InputAction interactAction;

        public CustomTrigger CustomCollision;
        public bool Check = false;
        public GameObject FoodObject;
        [HideInInspector]
        public new Rigidbody rigidbody;
        public Vector3 velocity = Vector3.zero;

        // Start is called before the first frame update    
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        void Start()
        {
            base.Start();

            moveAction = InputSystem.actions["Move"];
            interactAction = InputSystem.actions["Interact"];
        }

        private void Update()
        {
            base.Update();
            CustomCollision = TriggerManager.Instance.CheckTriggerZone(transform);
            rigidbody.velocity = velocity;
            //Debug.Log($"{rigidbody.velocity}");
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

        public void GetFoodObject()
        {
            FoodObject = CustomCollision.GetComponent<Kitchen>().Interaction();
            if (FoodObject != null)
            {
                FoodObject.transform.parent = this.transform;
                FoodObject.transform.position = transform.position + Vector3.up * 1.0f;
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