using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.CustomSystem
{
    public class BoxCollision : CustomCollision
    {
        public Vector3 Size = Vector3.zero;
        public override bool OnCollision(Transform t)
        {
            

            return false;
        }


        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + Offset, Size);
        }
    }

}