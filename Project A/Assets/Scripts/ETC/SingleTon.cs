using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace CKProject.SingleTon
{
    public abstract class SingleTon<T> : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get { return instance; }
            set { instance = value; }
        }
    }
}