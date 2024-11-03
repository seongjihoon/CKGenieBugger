//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;
//using CKProject.Managers;
//using Random = System.Random;


//namespace CKProject.Interactable
//{
//    public class Table1 : MonoBehaviour
//    {
//        // 만들어야 할 기능
//        // 손님과의 첫 충돌 시 캐릭터가 의자에 앉아 음식을 주문하는 기능
//        // 음식의 경우 일단 아무거나 줘도 문제 없게 구현할 예정.
//        public bool IsEmptyTable;
//        public Chair1[] Chairs;
//        private List<int> ChairNum = new List<int> { 0, 1};

//        private int currentChair = 0;
//        private int GetChair
//        {
//            get
//            {
//                return ChairNum[currentChair++];
//            }
//        }

//        private void Update()
//        {
//            // 이 문제를 해결하려 한다.
//            if (GuestManager1.Instance.TablingCheck(this)
//                && CheckChairs())
//            {
//                GuestManager1.Instance.TableClear(this);
//            }
//        }

//        private bool CheckChairs()
//        {
//            foreach(var chair in Chairs)
//            {
//                if (chair.Using) return false; 
//            }
//            IsEmptyTable = true;
//            return true;
//        }


//        public void Initialized()
//        {
//            foreach(var chair in Chairs)
//            {
//                chair.FoodType = EFoodType.None;
//            }
//            Shuffle();
//        }

//        public Transform GetChairTransform()
//        {
//            Chair1 temp = null;
//            if (currentChair >= Chairs.Length)
//            {
//                currentChair = 0;
//                Shuffle();
//            }
//            temp = Chairs[GetChair];
//            temp.Using = true;
//            return temp.transform;
//        }

//        public EFoodType SetFoodType()
//        {
//            return EFoodType.ALL;
//            //return (EFoodType)UnityEngine.Random.Range((int)EFoodType.Bugger, (int)EFoodType.ALL);
//        }

//        public void EnterGuest(PathFinding.Unit unit)
//        {
//            // 드디어 들어옴
//            // 게스트가 들어올 경우 의자에 음식 데이터를 넣어줌
//            Chair1 chair = unit.target.GetComponent<Chair1>();
//            if(chair != null)
//            {
//                chair.FoodType = SetFoodType();
//                chair.Guest = unit;
//            }
//        }

//        public bool EnterFood(Food food)
//        {
//            try
//            {
//                foreach (var chair in Chairs)
//                {
//                    if (chair.Guest == null) continue;

//                    if (chair.FoodType == food.GetFoodSo.foodType || chair.FoodType == EFoodType.ALL)
//                    {
//                        chair.FoodType = EFoodType.None;
//                        // 이후 n초 후 탈출 선언
//                        chair.EscapeGuest().Forget();
//                        Debug.Log("음식 확인");
//                        return true;
//                    }
//                }
//                Debug.Log("음식 불일치");
//                return false;
//            }
//            catch
//            {
//                return false;
//            }
//            finally
//            {
//                food.gameObject.SetActive(false);
//            }
//        }

//        private void EscapeUnit()
//        {

//        }

//        private void Shuffle()
//        {
//            Random random = new Random();
//            ChairNum = ChairNum.OrderBy(_ => random.Next()).ToList();
//        }
//    }
//}