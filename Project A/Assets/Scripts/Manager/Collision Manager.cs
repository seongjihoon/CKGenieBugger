using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.TriggerSystem;
using System.Diagnostics.CodeAnalysis;

namespace CKProject.Managers
{
    public class TriggerManager : SingleTon.SingleTon<TriggerManager>
    {
        public CustomTrigger[] CustomCollision;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            CustomCollision = FindObjectsOfType<CustomTrigger>();
        }

        public CustomTrigger CheckTriggerZone(Transform t)
        {
            foreach(var c in CustomCollision) 
            {
                if (c.OnCollision(t))
                {
                    return c;
                }
            }
            return null;
        }

        public CustomTrigger CheckTriggerZone(Vector3 vec)
        {
            foreach (var c in CustomCollision)
            {
                if (c.OnCollision(vec))
                {
                    Debug.Log($"{c.name}");
                    return c;
                }
            }
            return null;
        }
    }

}