using System.Collections;
using UnityEngine;
using CKProject.Interactable;
using CKProject.TriggerSystem;
using CKProject.Managers;
using CKProject.FSM;
using UnityEditor;
using System.Collections.Generic;

namespace PathFinding
{

    [SerializeField]
    public enum EGuestStateType
    {
        Idle = 1,
        Move = 2,
        Ready = 3,
        Out = 4,
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(Unit))]
    public class UnitFSMEditor : Editor
    {
        private Unit fsmEditor;
        private void OnEnable()
        {
            fsmEditor = (Unit)target;
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
#endif

    // 이걸 State로 가르기
    public class Unit : FSMController<EGuestStateType>
    {
        //Thread Research;
        public Transform target;
        public Grid grid;
        public Vector3[] path;
        public bool GetOut;
        public Chair Chair;

        //public List<EFoodType> Order = new List<EFoodType>();
        public EFoodType Order;
        public EGuestStateType GuestState;
        private float speed = 2;
        private int targetIndex;
        [SerializeField] private CustomTrigger hitedTrigger;


        protected override void Start()
        {
            base.Start();
            //PathRequestManager.RequestPath(new PathRequest( this.transform.position, target.position, OnPathFound));
            grid = GameObject.Find("Path Manager").GetComponent<Grid>();
            GuestState = EGuestStateType.Idle;
        }

        protected override void Update()
        {
            base.Update();
        }

        #region State Region 

        public void AddState()
        {
            // 모든 상태 적용 하자.
            int i = 0;
            BaseState<EGuestStateType>[] childState = transform.GetComponents<BaseState<EGuestStateType>>();

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

        [VisibleEnum(typeof(EGuestStateType))]
        public void ChangeState(int nextState)
        {
            ChangeState((EGuestStateType)nextState);
        }

        public override void ChangeState(EGuestStateType updateStateType)
        {
            base.ChangeState(updateStateType);
        }

        public void SetTarget(GameObject chair, Grid gridInfo)
        {
            grid = gridInfo;
            target = chair.transform;
            GetOut = false;
        }

        public EFoodType SetFood()
        {
            //Order.Add((EFoodType)Random.Range(0, GameManager.Instance.OpenFood));
            //return Order[Order.Count -1];
            Order = (EFoodType)Random.Range(1, GameManager.Instance.OpenFood + 1);
            //Order = EFoodType.ALL;
            return Order;
        }

        public void MoveStart()
        {
            PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
        }
        #endregion


        public void RequestPathGuest(GameObject chair, Grid gridInfo)
        {
            grid = gridInfo;
            target = chair.transform;
            PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
        }

        public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
        {
            if (pathSuccessful)
            {
                //chasing = true;
                targetIndex = 0;
                path = newPath;
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }


        // 목표물의 이동이 감지되면 체크를 함.
        // Rect로 칸 당 범위를 생성하고 생성된 범위를 체크하는 형식
        public bool CheckTargetPosition()
        {
            // 타겟 노드
            Vector3 targetPos = grid.NodeFromWorldPoint(target.position).worldPosition;
            // 최종 목표
            Vector3 FinTarget = path[path.Length - 1];

            float distance = Mathf.Abs(Vector3.Distance(targetPos, FinTarget));

            // 1.5 * n
            if (distance > 1.5f * 4)
            {
                return true;
            }

            return false;
        }

        public bool GetPathIndex()
        {
            return targetIndex >= path.Length - 1;
        }

        private IEnumerator FollowPath()
        {
            int count = 0;
            Vector3 currentWaypoint = path[0];
            transform.LookAt(currentWaypoint);
            while (true)
            {
                count++;
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (GetPathIndex())
                    {
                        transform.LookAt(path[targetIndex]);
                        transform.position += transform.forward * 0.75f;
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                    //transform.Rotate((transform.position - currentWaypoint).normalized);
                    transform.LookAt(currentWaypoint);
                }
                if (count > 100000)
                {
                    transform.GetComponent<Unit>().enabled = false;
                    yield break;
                }
                //if (CheckTargetPosition())
                //{
                //    chasing = false;
                //    break;
                //}

                currentWaypoint.y = transform.position.y;

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                
                yield return null;
            }
        }

        private void OnEnable()
        {
            //Order.Clear();
            Order = EFoodType.None;
        }
        private void OnDrawGizmos()
        {
            if (path != null)
            {
                for (int i = targetIndex; i < path.Length; i++)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(path[i], Vector3.one);

                    if (i == targetIndex)
                    {
                        Gizmos.DrawLine(transform.position, path[i]);
                    }
                    else
                    {
                        Gizmos.DrawLine(path[i - 1], path[i]);
                    }
                }
            }
        }
    }

}