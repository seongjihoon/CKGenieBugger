using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.TriggerSystem;
using System.Diagnostics.CodeAnalysis;

namespace CKProject.Managers
{
    public class TriggerManager : MonoBehaviour
    {
        public CustomTrigger[] CustomCollision;

        private void Awake()
        {
            //CreateInstance(this);
            //DontDestroyOnLoad(this);
        }

        private void Start()
        {
            //Initialize();
            //CustomCollision = FindObjectsOfType<BoxTrigger>();
        }

        public void Initialize()
        {
            CustomCollision = FindObjectsOfType<CustomTrigger>();
            foreach (var i in CustomCollision)
            {
                Debug.Log($"{i}");
            }
        }

        public CustomTrigger CheckTriggerZone(Transform t)
        {
            try
            {
                foreach (var c in CustomCollision)
                {
                    if (c.OnCollision(t))
                    {
                        return c;
                    }
                }
                return null;
            }
            catch
            {
                Initialize();

                foreach (var c in CustomCollision)
                {
                    if (c.OnCollision(t))
                    {
                        return c;
                    }
                }
                return null;
            }
            finally
            {
            }
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