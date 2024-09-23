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
        // ������ �� ���
        // �մ԰��� ù �浹 �� ĳ���Ͱ� ���ڿ� �ɾ� ������ �ֹ��ϴ� ���
        // ������ ��� �ϴ� �ƹ��ų� �൵ ���� ���� ������ ����.
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
                        Debug.Log("���� Ȯ��");
                        return;
                    }
                }
                Debug.Log("���� ����ġ");
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