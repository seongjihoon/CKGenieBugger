using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace pathFinding
{
    public class Pathfinding : MonoBehaviour
    {
        //PathRequestManager requestManager;

        //public Transform seeker, target;

        Grid grid;
        void Awake()
        {
            //requestManager = GetComponent<PathRequestManager>();
            grid = GetComponent<Grid>();
        }

        private void Update()
        {
        }

        public bool IsMovementPoint(Vector3 point)
        {
            Node targetNode = grid.NodeFromWorldPoint(point);
            return targetNode.walkable ? true : false;
            //return false;
        }


        //public void StartFindPath(Vector3 startPos, Vector3 targetPos)
        //{
        //    StartCoroutine(FindPath(startPos, targetPos));

        //}

        public void FindPath(PathRequest request, Action<PathResult> callback)
        {
            Vector3[] wayPoint = new Vector3[0];
            bool pathSuccess = false;

            Node startNode = grid.NodeFromWorldPoint(request.pathStart);
            Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);

            //if (startNode == null || targetNode == null)
            //    yield return null;


            if (startNode.walkable && targetNode.walkable)
            {
                Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
                HashSet<Node> closeSet = new HashSet<Node>();

                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    Node currentNode = openSet.RemoveFirst();

                    closeSet.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        pathSuccess = true;
                        break;
                    }

                    //grid.OpenListAdd(currentNode, targetNode, ref openSet, ref closeSet);

                    foreach (Node neighbour in grid.GetNeighbours(currentNode))
                    {
                        if (!neighbour.walkable || closeSet.Contains(neighbour))
                            continue;
                        
                        var checkX = neighbour.gridX;
                        var checkY = neighbour.gridY;

                        if (checkX  >= 100 && checkX < 0 && checkY >= 100 && checkY < 0) continue;

                        #region Debug
                        //if (!grid[checkX - 1, neighbour.gridY].walkable || !grid[neighbour.gridX, checkY - 1].walkable) continue;
                        //if (!grid[checkX - 1, neighbour.gridY].walkable || !grid[neighbour.gridX, checkY].walkable) continue;
                        //if (!grid[checkX - 1, neighbour.gridY].walkable || !grid[neighbour.gridX, checkY + 1].walkable) continue;
                        //if (!grid[checkX, neighbour.gridY].walkable || !grid[neighbour.gridX, checkY - 1].walkable) continue;
                        //if (!grid[checkX, neighbour.gridY].walkable || !grid[neighbour.gridX, checkY + 1].walkable) continue;
                        //if (!grid[checkX + 1, neighbour.gridY].walkable || !grid[neighbour.gridX, checkY - 1].walkable) continue;
                        //if (!grid[checkX + 1, neighbour.gridY].walkable || !grid[neighbour.gridX, checkY].walkable) continue;
                        //if (!grid[checkX + 1, neighbour.gridY].walkable || !grid[neighbour.gridX, checkY + 1].walkable) continue;
                        #endregion

                        // �밢�� �̵� ��, �ش� �̵� ���� ex) -1, -1 ��ġ�� ��� (0, -1), (-1, 0)��ġ �� �� �����ִ��� üũ �ؾ���.
                        int gCostDistance = GetDistance(currentNode, neighbour);
                        int newMovementCostToNeighbour = currentNode.gCost + gCostDistance + neighbour.movementPenalty;
                        if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                        {
                            neighbour.gCost = newMovementCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour, targetNode);
                            neighbour.parent = currentNode;

                            if (!openSet.Contains(neighbour))
                                openSet.Add(neighbour);
                            else
                                openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
            //yield return null;
            if(pathSuccess)
            {
                wayPoint = RetracePath(startNode, targetNode);
                pathSuccess = wayPoint.Length > 0;
            }
            //requestManager.FinishedProcessingPath();
            callback(new PathResult(wayPoint, pathSuccess, request.callback));
        }

        Vector3[] RetracePath(Node startNode, Node targetNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = targetNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            Vector3[] wayPoints = SimplifyPath(path);
            Array.Reverse(wayPoints);

            return wayPoints;
        }

        Vector3[] SimplifyPath(List<Node> path)
        {
            List<Vector3> wayPoints = new List<Vector3>();
            Vector2 directionOld = Vector2.zero;

            for(int i = 1; i <  path.Count; i++)
            {
                Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
                if(directionNew != directionOld)
                    wayPoints.Add(path[i].worldPosition);
                directionOld = directionNew;
            }
            return wayPoints.ToArray();
        }

        int GetDistance(Node nodeA, Node nodeB)
        {
            int disX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int disY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (disX > disY)
                return 14 * disY + 10 * (disX - disY);
            return 14 * disX + 10 * (disY - disX);
        }

    }

}