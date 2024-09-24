using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using CKProject.Managers;
using System;
using Cysharp.Threading.Tasks;

namespace CKProject.Interactable
{
    public class Chair : MonoBehaviour
    {
        [Header("Food Type")]
        public EFoodType FoodType = EFoodType.None;
        [Header("Guest")]
        public Unit Guest;
        public bool Using = false;

        public async UniTaskVoid EscapeGuest()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            Guest.GetOut = true;
            FoodType = EFoodType.None;
            Using = false;
            Guest = null;
            Debug.Log("°Ô½ºÆ® ÅðÀå");
        }
    }
}