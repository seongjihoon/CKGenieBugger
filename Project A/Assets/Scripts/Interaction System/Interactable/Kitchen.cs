using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.EditorUtils;
using UnityEditor.Timeline.Actions;

namespace CKProject.Interactable
{
    public enum EFoodType
    {
        None,
        Bugger,
        Pizza,
        Chicken,
        ALL,
        //Eatting,
    }

    public enum EKitchenType
    {
        Ready,
        Making,
        Complet,
        Clean,
    }
    public class Kitchen : MonoBehaviour, IInteract
    {
        #region public parameters
        // 키친에서 이걸 관리할 필요가 없어보이는데?
        public FoodScriptableObject FoodSO;

        public bool FoodReady = false;

        public Vector3 SpawnOffset = Vector3.zero;

        public GameObject CooldownPanel;
        
        public const int ObjectPoolCount = 10;

        public EKitchenType GetKitchenType
        {
            get { return kitchenType; }
        }

        #endregion

        #region private Parameters
        [SerializeField, ReadOnly]
        private GameObject[] objectPools;

        [SerializeField, ReadOnly]
        private EFoodType foodType;

        [SerializeField, ReadOnly]
        private EKitchenType kitchenType = EKitchenType.Ready;

        [SerializeField, ReadOnly]
        private float cookTimer = 0;

        [SerializeField, ReadOnly]
        private int curCount = 0;

        #endregion

        #region Interface Method
        /// <summary>
        /// 어떤 상호작용을 삽입해볼까?
        /// 주방이니 일단 제작하는 것을 목표로 해야겠지?
        /// </summary>
        public GameObject Interaction()
        {
            switch (kitchenType)
            {
                case EKitchenType.Ready:
                    kitchenType = EKitchenType.Making;
                    CooldownPanel.SetActive(true);
                    break;
                case EKitchenType.Making:
                    // 이 땐 작동 안하기
                    break;
                case EKitchenType.Complet:
                    // 제작
                    kitchenType = EKitchenType.Clean;
                    CooldownPanel.SetActive(true);
                    return objectPools[curCount];

                case EKitchenType.Clean:
                    break;
                default:
                    break;
            }
            return null;
        }

        #endregion


        #region private methods
        private void Start()
        {
            kitchenType = EKitchenType.Ready;
            objectPools = new GameObject[ObjectPoolCount];
            CooldownPanel.SetActive(false);
            int i = 0;
            for(; i < ObjectPoolCount; i++)
            {
                objectPools[i] = Instantiate(FoodSO.Prefab);
                objectPools[i].GetComponent<Food>()?.Init(FoodSO);
                objectPools[i].SetActive(false);
            }
        }

        private void MakingFood()
        {
            cookTimer += Time.deltaTime;
            ShowCoolDown(FoodSO.SpawnTime);
            if (cookTimer > FoodSO.SpawnTime)
            {
                kitchenType = EKitchenType.Complet;
                cookTimer = 0;
                SpawnObject();
#if UNITY_EDITOR
                //Debug.Log($"완성");
#endif
            }
        }

        private void SpawnObject()
        {
            objectPools[curCount].SetActive(true);
            CooldownPanel.SetActive(false);
            objectPools[curCount].transform.position = transform.position + SpawnOffset;
        }

        private void CleaningKitchen()
        {
            cookTimer += Time.deltaTime;
            ShowCoolDown(FoodSO.ReadyTime);
            if (cookTimer > FoodSO.ReadyTime)
            {
                CleanObjects();
                cookTimer = 0;
#if UNITY_EDITOR
                //Debug.Log($"제작 준비 완료");
#endif
            }
        }

        private void CleanObjects()
        {
            CooldownPanel.SetActive(false);
            kitchenType = EKitchenType.Ready;
            curCount++;
            if (curCount >= ObjectPoolCount)
                curCount = 0;
        }

        private void ShowCoolDown(float timeScale)
        {
            float cook = cookTimer / timeScale; 
            CooldownPanel.GetComponent<SpriteRenderer>().material.SetFloat("_Cooldown", cook);
        }

        private void Update()
        {
            if (kitchenType == EKitchenType.Making)
            {
                MakingFood();
            }
            else if(kitchenType == EKitchenType.Clean)
            {
                CleaningKitchen();
            }
        }
        #endregion

        #region public methods
        
        #endregion
    }

}