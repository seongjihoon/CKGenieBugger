using UnityEngine;
using PathFinding;
using Grid = PathFinding.Grid;
using System.Collections.Generic;
using CKProject.Interactable;

namespace CKProject.Managers
{

    public class GuestManager : MonoBehaviour
    {
        public static GuestManager Instance;


        public bool SpawnTrigger = false;
        public float SpawnInterval = 0;
        public int MaxSpawnCount = 0;

        [Space(10f)]
        public Transform SpawnPoint;
        public Transform HidePoint;
        public GameObject GuestPrefab;

        public Grid GridInfo;
        public GameObject[] TargetChairList;
        public Queue<OrderData> OrderQueue = new Queue<OrderData>();

        private List<GameObject> useChairList = new List<GameObject>();
        private Stack<GameObject> emptyChairStack = new Stack<GameObject>();
        

        private GuestList GuestList;

        public  GuestList GetGuestList 
        {
            get { return GuestList; }
        }

        private float currentTimer = 0;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            //CreateInstance(this);
            //DontDestroyOnLoad(this);

        }


        // Start is called before the first frame update
        void Start()
        {
            Intialized();
            currentTimer = SpawnInterval;
        }

        // Update is called once per frame
        void Update()
        {
            if (SpawnTrigger)
            {
                currentTimer += Time.deltaTime;
                if(currentTimer > SpawnInterval)
                {
                    SpawnGuest();
                    currentTimer = 0;
                }
            }
        }
        private void SpawnGuest()
        {
            GameObject tempChair = null;
            GameObject tempUnit = null;
            // ���� ��������� ���� ���ڸ� Ÿ������ ����
            if(emptyChairStack.Count > 0)
            {
                tempChair = emptyChairStack.Pop();
                useChairList.Add(tempChair);

                tempUnit = GuestList.GuestPools.Pop();
                // ���� �̵� ���̱� ������ ��� ������ ����.
                tempUnit.SetActive(true);
                tempUnit.transform.position = SpawnPoint.position;
                tempUnit.GetComponent<Unit>().SetTarget(tempChair, GridInfo);
            }
        }

        private void Intialized()
        {
            SetEmptyChairs();
            SetGuest();
        }

        private void SetEmptyChairs()
        {
            foreach (var chair in TargetChairList)
            {
                emptyChairStack.Push(chair);
            }
        }

        private void SetGuest()
        {
            GuestList = new GuestList();
            GuestList.Init();
            GameObject tempGuest = null;
            for (int i = 0; i < MaxSpawnCount; i++)
            {
                tempGuest = Instantiate(GuestPrefab, transform);
                tempGuest.SetActive(false);
                GuestList.GuestPools.Push(tempGuest);
            }
        }


        #region Public Methods
        public void OutGuest(Unit unit)
        {
            useChairList.Remove(unit.target.gameObject);
            GuestList.GuestPools.Push(unit.gameObject);
            unit.gameObject.SetActive(false);
        }

        public void SetWaitingOrder(GameObject guest)
        {
            GuestList.WaitingOrderGuest.Enqueue(guest);
        }
        
        public void SetWaitingFood(Unit guest)
        {
            // ���� ����
            // ���� ������ ������ ���� ȣ��
            OrderData orderData;
            orderData.FoodType = guest.SetFood();
            orderData.OrderTarget = guest.gameObject;
            OrderQueue.Enqueue(orderData);

            //GuestList.WaitingFoodGuest.Enqueue(guest.gameObject);

        }

        public GameObject GetWaitingOrderGuest()
        {
            return GuestList.WaitingOrderGuest.Dequeue();
        }

        public OrderData GetWaitingFoodGuest()
        {
            return OrderQueue.Dequeue();
            //return GuestList.WaitingOrderGuest.Dequeue();
        }

        public void ReturnEmptyChair(GameObject chair)
        {
            emptyChairStack.Push(chair);
        }

        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            //Gizmos.DrawWireCube(SpawnPoint.position, Vector3.one);
        }
    }

    /// <summary>
    /// ���������� ���.
    /// </summary>
    public struct GuestList
    {
        // ����� ������ Guest Pool
        public Stack<GameObject> GuestPools;

        //public Stack<GameObject> GetGuestPools {  get { return GuestPools; } private set { GuestPools = value; } }

        // �̵� ���� Guest

        // �ֹ� ��� ���� Guest
        public Queue<GameObject> WaitingOrderGuest;

        // ���� ���� ��� ���� Guest 
        //public Queue<GameObject> WaitingFoodGuest;

        // ������ Guest

        public void Init()
        {
            GuestPools = new Stack<GameObject>();
            WaitingOrderGuest = new Queue<GameObject>();
            //WaitingFoodGuest = new Queue<GameObject>();
        }
    }


    public struct OrderData
    {
        public EFoodType FoodType;
        public GameObject OrderTarget;
    }
}