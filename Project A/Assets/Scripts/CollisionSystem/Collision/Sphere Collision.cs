using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

namespace CKProject.TriggerSystem
{
    public class SphereTrigger : CustomTrigger
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

        public override bool OnCollision(Vector3 v)
        {
            if (Vector3.Distance(v, transform.position + Offset) <= Radius)
                return true;
            return false;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = PersonalColor;
            Gizmos.DrawWireSphere(Offset + transform.position, Radius);
        }
    }
}
