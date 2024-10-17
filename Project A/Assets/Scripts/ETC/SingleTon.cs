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

        protected void CreateInstance(T singleTon)
        {
            if(instance == null)
            {
                instance = singleTon;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}