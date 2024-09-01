using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CKProject
{

    public class PlayerIdleState : BaseState<StateType>
    {
        private PlayerIdleState()
        {
        }
        
        ~PlayerIdleState()
        {
            Debug.Log("Bye");
        }

        private void OnEnable()
        {
            Debug.Log("OnEnable");
        }

        private void OnDisable()
        {
            Debug.Log("OnDisable");
        }

        private void Awake()
        {
            Debug.Log($"{this.ToString()}의 Awake 시작");
        }

        private void OnDestroy()
        {
            Debug.Log($"Destroy");
        }

        private void OnValidate()
        {
            GetComponent<FSMController<StateType>>()?.AddState(StateType.A, this);
        }
    }
}