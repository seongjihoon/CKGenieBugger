using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[CustomEditor(typeof(BaseState<Enum>))]
public class MyStateEditor : Editor
{
    private BaseState<Enum> baseState;

    private void OnEnable()
    {
        baseState = (BaseState<Enum>)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (baseState == null)
        {
            OnComponentRemoved();
        }
    }

    private void OnComponentRemoved()
    {
        Debug.Log($"MyComponent has been Removed!");
    }
}

public abstract class BaseState<T> : MonoBehaviour
{
    [SerializeField]
    protected T stateType;
    public T StateType { get { return stateType; } }

    public UnityEvent enterStateEvent;
    public UnityEvent executeUpdateStateEvent;
    public UnityEvent executeFixedUpdateStateEvent;
    public UnityEvent exitStateEvent;

    public void ExcuteUpdate()
    {

    }

    internal void ExcuteFixedUpdate()
    {
        throw new NotImplementedException();
    }
}
