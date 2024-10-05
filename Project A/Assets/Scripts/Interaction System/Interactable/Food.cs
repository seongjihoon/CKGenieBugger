using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using CKProject.TriggerSystem;
using CKProject.Managers;
using MVP;

namespace CKProject.Interactable
{
    public class Food : MonoBehaviour
    {
        [SerializeField]
        private CustomTrigger customCollision;
        private FoodScriptableObject foodSO;
        public bool Throwing = false;

        public int[] getMoney;

        public FoodScriptableObject GetFoodSo
        {
            get { return foodSO; }
        }

        //public Task Unitask { get; private set; }

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
                customCollision = TriggerManager.Instance.CheckTriggerZone(transform);
                if(customCollision != null)
                {
                    Throwing = false;
                    AddMoney();
                }
            }
        }
        
        private void AddMoney()
        {
            if(customCollision.GetComponent<Table1>().EnterFood(this))
            {
                GameObject.Find("UI Manager").GetComponent<Controller>().AddMoney(foodSO.Price);
            }
            
        }
    }
}