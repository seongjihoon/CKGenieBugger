using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System;
using CKProject.CustomSystem;
using CKProject.Managers;

namespace CKProject.Interactable
{
    public class Food : MonoBehaviour
    {
        [SerializeField]
        private CustomCollision customCollision;
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
                customCollision = CollisionManager.Instance.CheckCollision(transform);
                if(customCollision != null)
                {
                    // 음식 제거
                    gameObject.SetActive(false);
                }
            }
        }
    }
}