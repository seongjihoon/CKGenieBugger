using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CKProject.FSM
{

    public abstract class BaseState<T> : MonoBehaviour where T : Enum
    {
        [HideInInspector] public FSMController<T> FsmController;

        [SerializeField]
        protected T stateType;
        public T StateType { get { return stateType; } }

        public UnityEvent EnterStateEvent;
        public UnityEvent ExecuteUpdateStateEvent;
        public UnityEvent ExecuteFixedUpdateStateEvent;
        public UnityEvent ExitStateEvent;

        [HideInInspector]
        public bool bEndState = false;

        public void DebugScript(string str)
        {
            //Debug.Log($"{str}");
        }

        public virtual void Enter()
        {
            this.enabled = true;
            EnterStateEvent?.Invoke();
        }

        public virtual void Exit()
        {
            ExitStateEvent?.Invoke();
            this.enabled = false;
        }

        public virtual void ExcuteUpdate()
        {
            ExecuteUpdateStateEvent?.Invoke();
        }

        public virtual void ExcuteFixedUpdate()
        {
            ExecuteFixedUpdateStateEvent?.Invoke();
        }

        //protected void ChangeState(T stateType)
        //{
        //    FsmController.ChangeState(stateType);
        //}
    }

}