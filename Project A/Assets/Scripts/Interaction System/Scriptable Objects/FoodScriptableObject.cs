using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CKProject.Interactable
{
    [CreateAssetMenu(fileName = "Food", menuName = "ScriptableObjects/InteractableObjects", order = 1)]
    public class FoodScriptableObject : ScriptableObject
    {
        // 음식 오브젝트를 생성하려면 Prefab이 필요함.
        public GameObject Prefab;
        public EFoodType foodType;
        public int Price;
        [Space(10), InspectorName("준비 시간")]
        public float ReadyTime;
        [Space(10), InspectorName("제작 시간")]
        public float SpawnTime;

        [Space(10), InspectorName("날아가는 속도")]
        public float ThrowSpeed;

    }
}
