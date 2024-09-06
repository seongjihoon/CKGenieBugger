using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.EditorUtils;

namespace CKProject.Interactable
{
    public enum EFoodType
    {
        Bugger,
        Pizza,
        Chicken
    }
    public class Kitchen : MonoBehaviour, IInteract
    {
        #region public parameters
        // 키친에서 이걸 관리할 필요가 없어보이는데?
        public FoodScriptableObject FoodSO;

        #endregion

        #region private Parameters
        [SerializeField, ReadOnly]
        private GameObject[] objectPools;

        [SerializeField, ReadOnly]
        private EFoodType foodType;

        #endregion

        #region Interface Method
        /// <summary>
        /// 어떤 상호작용을 삽입해볼까?
        /// 주방이니 일단 제작하는 것을 목표로 해야겠지?
        /// </summary>
        public void Interaction()
        {
            EditorUtility.DebugLogScript("생성") ;
        }
        #endregion
    }

}