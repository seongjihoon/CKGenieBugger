using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;


namespace CKProject
{
    //[Serializable]
    //public class StateDictionary : SerializableDictionary<StateType, BaseState<StateType>> { }

    public abstract class FSMController<T1> : MonoBehaviour where T1 : Enum
    {
        [SerializeField] private T1 previousStateType;
        public T1 PreviousStateType { get { return previousStateType; } }

        [SerializeField] private T1 currentStateType;
        public T1 CurrentStateType { get { return currentStateType; } }

        [SerializeField]
        [ReadOnly] private SerializableDictionary<T1, BaseState<T1>> stateTable = new SerializableDictionary<T1, BaseState<T1>>();

        //[SerializeField]
        //public StateDictionary stateDictionary = new StateDictionary();

        public UnityAction<T1> changeStateAction;
        public UnityAction excuteUpdateAction;
        public UnityAction excuteFixedUpdateAction;

        public BaseState<T1> GetState(T1 stateType)
        {
            if(stateTable.ContainsKey(stateType))
            {
                return stateTable[stateType];
            }
            Debug.LogError($"FSM:: {stateType.ToString()}이 존재하지 않습니다.");
            return null;
        }

        public virtual void AddState(T1 stateType, BaseState<T1> state)
        {
            Debug.Log($"State = {state.ToString()}");
        }

        public virtual void SubState(T1 stateType)
        {

        }

        public virtual void ChangeState(T1 updateStateType)
        {

        }

        protected virtual void Update()
        {
            stateTable[currentStateType]?.ExcuteUpdate();
            excuteUpdateAction?.Invoke();
        }

        protected virtual void FixedUpdate()
        {
            stateTable[currentStateType]?.ExcuteFixedUpdate();
            excuteFixedUpdateAction?.Invoke();
        }




    }
}
