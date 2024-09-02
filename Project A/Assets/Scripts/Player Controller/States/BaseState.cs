using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

namespace CKProject
{

    public abstract class BaseState<T> : MonoBehaviour
    {
        [SerializeField]
        protected T stateType;
        public T StateType { get { return stateType; } }

        public UnityEvent enterStateEvent;
        public UnityEvent executeUpdateStateEvent;
        public UnityEvent executeFixedUpdateStateEvent;
        public UnityEvent exitStateEvent;

        public bool bEndState = false;

        public void DebugScript(string str)
        {
            Debug.Log($"{str}");
        }

        public virtual void Enter()
        {
            this.enabled = true;
            enterStateEvent?.Invoke();
        }

        public virtual void Exit()
        {
            exitStateEvent?.Invoke();
            this.enabled = false;
        }

        public virtual void ExcuteUpdate()
        {
            executeUpdateStateEvent?.Invoke();
        }

        public virtual void ExcuteFixedUpdate()
        {
            executeFixedUpdateStateEvent?.Invoke();
        }
        public void KeyDownEvent()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bEndState = true;
            }
        }

    }

}