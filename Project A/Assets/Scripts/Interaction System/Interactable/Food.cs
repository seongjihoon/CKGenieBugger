using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System;

namespace CKProject.Interactable
{
    public class Food : MonoBehaviour
    {
        private FoodScriptableObject foodSO;
        public bool Throwing = false;

        public Task Unitask { get; private set; }

        public void Init(FoodScriptableObject foodSO)
        {
            this.foodSO = foodSO;
        }

        public void OnEnable()
        {
            Throwing = false;
        }

        public async UniTaskVoid Throw()
        {
            Throwing = true;
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            gameObject.SetActive(false);
        }



        private void Update()
        {
            if(Throwing)
            {
                transform.Translate(transform.forward * Time.deltaTime * foodSO.ThrowSpeed);
            }
        }
    }
}