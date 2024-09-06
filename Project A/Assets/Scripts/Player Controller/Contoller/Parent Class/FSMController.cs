using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEditor;


namespace CKProject.FSM
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
            if (!stateTable.ContainsKey(stateType))
                stateTable.Add(stateType, state);
        }

        public virtual void ClearStates()
        {
            stateTable.Clear();
        }

        public virtual void SubState(T1 stateType)
        {
            if(stateTable.ContainsKey(stateType))
                stateTable.Remove(stateType);
        }

        public virtual void ChangeState(T1 updateStateType)
        {
            previousStateType = currentStateType;
            stateTable[currentStateType].Exit();
            currentStateType = updateStateType;
            stateTable[currentStateType].Enter();
            changeStateAction?.Invoke(currentStateType);
        }

        protected virtual void Start()
        {
            stateTable[currentStateType]?.Enter();
            changeStateAction?.Invoke(currentStateType);
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
