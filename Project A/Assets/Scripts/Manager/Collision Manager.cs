using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.CustomSystem;
using System.Diagnostics.CodeAnalysis;

namespace CKProject.Managers
{
    public class CollisionManager : SingleTon.SingleTon<CollisionManager>
    {
        public CustomCollision[] CustomCollision;

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
            CustomCollision = FindObjectsOfType<CustomCollision>();
        }

        public CustomCollision CheckCollision(Transform t)
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
    }

}