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

        public int Price;
        public float SpawnTime;


    }
}
