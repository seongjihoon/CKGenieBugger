using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CKProject.Managers;
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

        private void Update()
        {
            // �� ������ �ذ��Ϸ� �Ѵ�.
            if (GuestManager.Instance.TablingCheck(this)
                && CheckChairs())
            {
                GuestManager.Instance.TableClear(this);
            }
        }

        private bool CheckChairs()
        {
            foreach(var chair in Chairs)
            {
                if (chair.Using) return false; 
            }
            IsEmptyTable = true;
            return true;
        }


        public void Initialized()
        {
            foreach(var chair in Chairs)
            {
                chair.FoodType = EFoodType.None;
            }
            Shuffle();
        }

        public Transform GetChairTransform()
        {
            Chair temp = null;
            if (currentChair >= Chairs.Length)
            {
                currentChair = 0;
                Shuffle();
            }
            temp = Chairs[GetChair];
            temp.Using = true;
            return temp.transform;
        }

        public EFoodType SetFoodType()
        {
            return EFoodType.ALL;
            //return (EFoodType)UnityEngine.Random.Range((int)EFoodType.Bugger, (int)EFoodType.ALL);
        }

        public void EnterGuest(PathFinding.Unit unit)
        {
            // ���� ����
            // �Խ�Ʈ�� ���� ��� ���ڿ� ���� �����͸� �־���
            Chair chair = unit.target.GetComponent<Chair>();
            if(chair != null)
            {
                chair.FoodType = SetFoodType();
                chair.Guest = unit;
            }
        }

        public void EnterFood(Food food)
        {
            try
            {
                foreach (var chair in Chairs)
                {
                    if (chair.Guest == null) continue;

                    if (chair.FoodType == food.GetFoodSo.foodType || chair.FoodType == EFoodType.ALL)
                    {
                        chair.FoodType = EFoodType.Eatting;
                        // ���� n�� �� Ż�� ����
                        chair.EscapeGuest().Forget();
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

        private void EscapeUnit()
        {

        }

        private void Shuffle()
        {
            Random random = new Random();
            ChairNum = ChairNum.OrderBy(_ => random.Next()).ToList();
        }
    }
}