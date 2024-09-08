using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace CKProject.CustomSystem
{
    public class SphereCollision : CustomCollision
    {
        [SerializeField]
        private float Radius = 0f;

        public override bool OnCollision(Transform t)
        {
            if (t == null)
                Debug.LogError($"존재하지 않는 트랜스폼");
            if (Vector3.Distance(t.position, transform.position + Offset) <= Radius)
                return true;

            return false;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(Offset + transform.position, Radius);
        }
    }
}
