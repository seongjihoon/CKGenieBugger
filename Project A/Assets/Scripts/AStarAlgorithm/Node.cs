using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System;

namespace PathFinding
{
    public enum NODETYPE
    {
        UnWalkable,
        Walkable,
        MoveTarget,

    }
    public class Node : IHeapItem<Node>
    {
        int heapIndex;

        public bool walkable;
        public bool target; 
        public Vector3 worldPosition;
        public int gridX;
        public int gridY;
        public int movementPenalty;

        public int gCost;
        public int hCost;
        public Node parent;


        public Node(bool _walkable,bool _target, Vector3 _worldPos, int _gridX, int _gridY, int _penalty)
        {
            walkable = _walkable;
            target = _target;
            worldPosition = _worldPos;
            gridX = _gridX;
            gridY = _gridY;
            movementPenalty = _penalty;
        }

        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }

        public int HeapIndex
        {
            get
            {
                return heapIndex;
            }
            set
            {
                heapIndex = value;
            }
        }

        public int CompareTo(Node other)
        {
            int compare = fCost.CompareTo(other.fCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(other.hCost);
            }

            return -compare;
        }

    }
}