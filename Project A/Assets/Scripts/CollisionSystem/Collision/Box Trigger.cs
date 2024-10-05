using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.TriggerSystem
{

    public class BoxTrigger : CustomTrigger
    {
        public Vector3 Size = Vector3.zero;
        [SerializeField] private Rect localScale;

        public override bool OnCollision(Transform t)
        {
            if (CheckXYSize(t.position) && CheckZSize(t.position))
            {
                PlayEnterEvents(t);
                return true;
            }
            return false;
        }

        public override bool OnCollision(Vector3 v)
        {
            if (CheckXYSize(v) && CheckZSize(v))
            {
                //PlayEnterEvents(v);
                return true;
            }
            return false;
        }

        private bool CheckZSize(Vector3 v)
        {
            if (transform.position.z + Offset.z + Size.z * .5f >= v.z 
                && transform.position.z + Offset.z - Size.z * 0.5f <= v.z)
                return true;
            return false;
        }

        private bool CheckXYSize(Vector3 v)
        {
            if(v.x <= localScale.x && v.x >= localScale.width &&
                v.y <= localScale.y && v.y >= localScale.height)
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
                (transform.position.x + Offset.x - Size.x * 0.5f), 
                (transform.position.y + Offset.y - Size.y * 0.5f));


        }
    }

}