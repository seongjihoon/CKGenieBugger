using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CKProject.TriggerSystem
{
    public abstract class CustomTrigger : MonoBehaviour
    {
        [Header("Events"), SerializeField]
        private UnityEvent InteractEvent;
        public UnityEvent EnterEvent;
        //[SerializeField]
        //private List<Transform> HitedTransforms;

        public bool isTrigger = false;
        public Color PersonalColor;
        public Vector3 Offset;

        private List<Transform> enterTransforms = new List<Transform>();

        public abstract bool OnCollision(Transform t);
        public abstract bool OnCollision(Vector3 v);

        public virtual void PlayInteractionEvent()
        {
            InteractEvent?.Invoke();
            
        }

        public virtual bool IsEnter(Transform t)
        {
            return enterTransforms.Contains(t) ? true: false;
        }

        //public virtual void PlayEnterEvent()
        //{

        //}

        public virtual void PlayEnterEvents(Transform t)
        {
            if (!enterTransforms.Contains(t))
            {
                enterTransforms.Add(t);
                EnterEvent?.Invoke();
            }
        }

        public virtual void PlayExitEvents(Transform t)
        {
            enterTransforms.Remove(t);
        }
    }
}
