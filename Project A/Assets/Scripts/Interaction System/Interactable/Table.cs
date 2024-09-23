using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Random = System.Random;


namespace CKProject.Interactable
{
    public class Table : MonoBehaviour
    {
        // 만들어야 할 기능
        // 손님과의 첫 충돌 시 캐릭터가 의자에 앉아 음식을 주문하는 기능
        // 음식의 경우 일단 아무거나 줘도 문제 없게 구현할 예정.
        public bool IsEmptyTable;
        public Chair[] Chairs;
        private List<int> ChairNum = new List<int> { 0, 1};

        private int currentChair = 0;
        private int GetChair
        {
            get
            {
                return ChairNum[currentChair++];
            }
        }

        public void Initialized()
        {
            Shuffle();
        }

        public Transform GetChairTransform()
        {
            if (currentChair >= Chairs.Length)
            {
                currentChair = 0;
                Shuffle();
            }
            return Chairs[GetChair].transform;
        }

        public void EnterGuest(PathFinding.Unit unit)
        {

        }

        public void EnterFood(Food food)
        {
            try
            {
                foreach (var chair in Chairs)
                {
                    if (chair.Guest != null) continue;

                    if (chair.FoodType == food.GetFoodSo.foodType || chair.FoodType == EFoodType.ALL)
                    {
                        Debug.Log("음식 확인");
                        return;
                    }
                }
                Debug.Log("음식 불일치");
                return;
            }
            catch
            {

            }
            finally
            {
                food.gameObject.SetActive(false);
            }

        }

        private void Shuffle()
        {
            Random random = new Random();
            ChairNum = ChairNum.OrderBy(_ => random.Next()).ToList();
        }
    }
}