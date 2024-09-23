using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CKProject.CustomSystem
{
    public abstract class CustomCollision : MonoBehaviour
    {
        [Header("Events"), SerializeField]
        private UnityEvent InteractEvent;
        public UnityEvent EnterEvent;
        //[SerializeField]
        //private List<Transform> HitedTransforms;

        public Color PersonalColor;
        public Vector3 Offset;

        private List<Transform> enterTransforms;

        public abstract bool OnCollision(Transform t);

        public virtual void PlayInteractionEvent()
        {
            InteractEvent?.Invoke();
            
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
