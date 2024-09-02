using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class SingleTon<T> : MonoBehaviour
{
    private T instance;
    
    public T Instance { get { return instance; } set { instance = value; } }
}
