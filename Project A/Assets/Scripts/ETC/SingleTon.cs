using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace CKProject.SingleTon
{
    //public abstract class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
    //{
    //    private static T instance;

    //    public static T Instance
    //    {
    //        get {
    //            if (instance == null)
    //            {
    //                instance = FindObjectOfType(typeof(T).Name);
    //            }
    //            return instance;

    //        }
    //        set { instance = value; }
    //    }



    //    protected void CreateInstance(T singleTon)
    //    {
    //        if(instance == null)
    //        {
    //            instance = singleTon;
    //            Debug.Log("Init: " + gameObject.name);
    //        }
    //        else
    //        {
    //            //Destroy(this);
    //        }
    //    }
    //}
}