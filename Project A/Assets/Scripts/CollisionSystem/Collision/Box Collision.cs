using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.CustomSystem
{

    public class BoxCollision : CustomCollision
    {
        public Vector3 Size = Vector3.zero;
        [SerializeField] private Rect localScale;

        public override bool OnCollision(Transform t)
        {
            if (CheckXYSize(t) && CheckZSize(t))
                return true;
            return false;
        }

        private bool CheckZSize(Transform t)
        {
            if (transform.position.z + Size.z * .5f >= t.position.z 
                && transform.position.z - Size.z * 0.5f <= t.position.z)
                return true;
            return false;
        }

        private bool CheckXYSize(Transform t)
        {
            if(t.position.x <= localScale.x && t.position.x >= localScale.width &&
                t.position.y <= localScale.y && t.position.y >= localScale.height)
            {
                return true;
            }
            return false;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = PersonalColor;
            Gizmos.DrawWireCube(transform.position + Offset, Size);

            localScale.Set((transform.position.x + Offset.x + Size.x * 0.5f),
                (transform.position.y + Offset.y + Size.y * 0.5f), 
                (transform.position.x - Offset.x - Size.x * 0.5f), 
                (transform.position.y - Offset.y - Size.y * 0.5f));


        }
    }

}