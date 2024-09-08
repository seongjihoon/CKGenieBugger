using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.EditorUtils;
using UnityEditor.Timeline.Actions;

namespace CKProject.Interactable
{
    public enum EFoodType
    {
        Bugger,
        Pizza,
        Chicken
    }

    public enum EKitchenType
    {
        Ready,
        Making,
        Complet
    }
    public class Kitchen : MonoBehaviour, IInteract
    {
        #region public parameters
        // 키친에서 이걸 관리할 필요가 없어보이는데?
        public FoodScriptableObject FoodSO;

        public bool foodReady = false;
        

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
        #endregion

        #region Interface Method
        /// <summary>
        /// 어떤 상호작용을 삽입해볼까?
        /// 주방이니 일단 제작하는 것을 목표로 해야겠지?
        /// </summary>
        public void Interaction()
        {
            switch (kitchenType)
            {
                case EKitchenType.Ready:
                    kitchenType = EKitchenType.Making;
                    break;
                case EKitchenType.Making:
                    // 이 땐 작동 안하기
                    break;
                case EKitchenType.Complet:
                    // 음식 전달
                    break;
                default:
                    break;
            }
        }
        #endregion


        #region private methods
        private void Start()
        {
            kitchenType = EKitchenType.Ready;
        }

        private void Update()
        {
            if (kitchenType == EKitchenType.Making)
            {
                cookTimer += Time.deltaTime;
                if(cookTimer > FoodSO.SpawnTime)
                {
                    kitchenType = EKitchenType.Complet;
#if UNITY_EDITOR
                    Debug.Log($"완성");
#endif
                }
            }
        }
        #endregion

        #region public methods

        #endregion
    }

}