using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using CKProject.SingleTon;
using CKProject.Interactable;

namespace CKProject.Managers
{
    public class GameManager : SingleTon<GameManager>
    {
        [ReadOnly] public int Stage = 1;
        [ReadOnly] public int OpenFood = 0;


        private void Awake()
        {
            CreateInstance(this);
            OpenFood = Stage;
            DontDestroyOnLoad(this.gameObject);
        }

        public void KitchenOpen()
        {
            if(Stage > OpenFood && (EFoodType)OpenFood < EFoodType.ALL)
            {
                OpenFood++;
            }
        }


    }

}