using System.Collections;
using System.Threading;
using UnityEngine;
using CKProject.Interactable;
using CKProject.TriggerSystem;
using CKProject.Managers;


namespace PathFinding
{
    // 이걸 State로 가르기
    public class Unit : MonoBehaviour
    {
        //Thread Research;
        public Transform target;
        public Grid grid;
        public Vector3[] path;
        public bool chasing = false;
        public bool finding = false;

        private float speed = 2;
        private int targetIndex;
        private CustomTrigger hitedTrigger;


        private void Start()
        {
            //PathRequestManager.RequestPath(new PathRequest( this.transform.position, target.position, OnPathFound));
            grid = GameObject.Find("PathFinding").GetComponent<Grid>();
        }

        private void Update()
        {
            //if (!chasing && !finding)
            //    PathRequestManager.RequestPath(new PathRequest(this.transform.position, target.position, OnPathFound));
            hitedTrigger = TriggerManager.Instance.CheckCollision(transform);
            if(hitedTrigger != null)
            {
                
            }
        }


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
                chasing = true;
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

        IEnumerator FollowPath()
        {
            int count = 0;
            Vector3 currentWaypoint = path[0] + Vector3.forward * 0.5f;
            while (true)
            {
                count++;
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex] + Vector3.forward * 0.5f;
                }
                if (count > 100000)
                {
                    transform.GetComponent<Unit>().enabled = false;
                    yield break;
                }
                if (CheckTargetPosition())
                {
                    chasing = false;
                    break;
                }

                currentWaypoint.y = transform.position.y;

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;
            }
        }

        public void OnDrawGizmos()
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