//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using PathFinding;
//using CKProject.SingleTon;
//using CKProject.Interactable;
//using System.Linq;
//using System;

//using Random = System.Random;
//using Grid = PathFinding.Grid;


//namespace CKProject.Managers
//{
//    public class GuestManager1 : SingleTon<GuestManager1>
//    {
//        //Dictionary<int, GameObject[,]> chairs;
//        [Header("생성 관련 변수")] public bool SpawnTrigger;

//        [Tooltip("생성 주기")] public float SpawnTimer = 0;
//        [Tooltip("최대 생성 갯수")] public int MaxSpawnCount = 0;
//        private float currentTimer = 0;
//        //[Tooltip("현재 어떤 손님을 생성했는지 체크")] private int currentPoolsCount = 0;


//        [Space(20f)] public Transform spawnPoint;
//        [Space(20f)] public Vector3 SpawnPoint;

//        [SerializeField] private GameObject guestPrefab;

//        [SerializeField, Space(10)] private Grid gridFieldInfo;

//        [SerializeField] private GameObject[] targetChairList;

//        [SerializeField] private Table1[] targetTableList;
//        [SerializeField] private List<Table1> useTableList;
//        private Stack<Table1> tableStack = new Stack<Table1>();

//        private int currentChairCount = 0;

//        //private List<Unit> guestPools;
//        private List<Unit> useGuestList = new List<Unit>();
//        // 테스트 코드 
//        private Stack<Unit> guestStack = new Stack<Unit>();
//        //private List<Unit>

//        private int chairCount = 0;

//        private Queue<Table1> tableQueue = new Queue<Table1>();

//        private void Awake()
//        {
//            CreateInstance(this);
//        }

//        // Start is called before the first frame update
//        private void Start()
//        {
//            // 최대 생성 개수 (2개) / 임시 생성 타임 (3초)
//            SpawnTimer = SpawnTimer <= 0 ? 3f : SpawnTimer;
//            MaxSpawnCount = MaxSpawnCount <= 0 ? 2 : MaxSpawnCount;
//            CreateGuest2();
//            // 빈 테이블을 체크
//            SetStackEmptyTable();

//        }

//        // Update is called once per frame
//        private void Update()
//        {
//            if(SpawnTrigger)
//            {
//                currentTimer += Time.deltaTime;
//                if(currentTimer > SpawnTimer)
//                {
//                    SpawnGuest3();
//                    currentTimer = 0;
//                }
//            }
//        }

//        #region Private Methods
//        private void SetQueueEmptyTable()
//        {
//            var random = new Random();
//            foreach(var table in targetTableList)
//            {
//                tableQueue.Enqueue(table);
//            }
//             tableQueue = (Queue<Table1>)tableQueue.OrderBy(_ => random.Next());
//        }

//        private void SetStackEmptyTable()
//        {
//            foreach(var table in targetTableList)
//            {
//                tableStack.Push(table);
//                table.Initialized();
//            }
//        }

//        /// <summary>
//        /// 손님을 생성하고 위치를 지정
//        /// 손님이 나가면 UseGuestList에 Remove하고 GuestStack에 Push하기
//        /// 테이블 또한 마찬가지
//        /// </summary>
//        private void SpawnGuest3()
//        {
//            int SpawnCount = UnityEngine.Random.Range(0, MaxSpawnCount);
//            int i = 0;
//            Unit unitTemp = null;
//            Table1 tableTemp = null;
//            if (tableStack.Count > 0)
//            {
//                tableTemp = tableStack.Pop();
//                tableTemp.IsEmptyTable = false;
//                useTableList.Add(tableTemp);
//                for (; i < SpawnCount + 1; i++)
//                {
//                    unitTemp = guestStack.Pop();
//                    useGuestList.Add(unitTemp);
//                    unitTemp.gameObject.SetActive(true);
//                    unitTemp.transform.position = new Vector3(spawnPoint.transform.position.x, 0f, spawnPoint.transform.position.z);
//                    unitTemp.SetTarget(tableTemp.GetChairTransform().gameObject, gridFieldInfo);
//                }
//            }
//        }

//        private void TableSet()
//        {
//            foreach(var table in targetTableList)
//            {
//                table.Initialized();
//            }
//        }

//        private void UpandCount()
//        {
//            currentChairCount++;
//            if (currentChairCount >= targetChairList.Length)
//                currentChairCount = 0;
//        }

//        private void CreateGuest2()
//        {
//            chairCount = targetChairList.Length;
//            for(int i = 0; i  < chairCount; i++)
//            {
//                guestStack.Push(Instantiate(guestPrefab).GetComponent<Unit>());
//                guestStack.Peek().gameObject.SetActive(false);
//                guestStack.Peek().transform.parent = transform;

//            }
//        }

//        private void OnDrawGizmos()
//        {
//            Gizmos.color = Color.green;
//            Gizmos.DrawWireCube(transform.position + SpawnPoint, Vector3.one);
//        }
//        #endregion

//        #region Public Methods
//        public void TableClear(Table1 table)
//        {
//            useTableList.Remove(table);
//            tableStack.Push(table);
//        }

//        public void OutGuest(Unit unit)
//        {
//            useGuestList.Remove(unit);
//            guestStack.Push(unit);
//            unit.gameObject.SetActive(false);
//        }

//        public bool TablingCheck(Table1 table)
//        {
//            return useTableList.Contains(table);
//        }
//        #endregion

//        #region Legarcy
//        private void SpawnGuest2()
//        {
//            int SpawnCount = UnityEngine.Random.Range(0, MaxSpawnCount);
//            int i = 0;
//            Unit temp = null;
//            if (guestStack.Count > 0)
//            {
//                for (; i < SpawnCount + 1; i++)
//                {
//                    temp = guestStack.Pop();
//                    useGuestList.Add(temp);
//                    temp.gameObject.SetActive(true);
//                    temp.transform.position = new Vector3(spawnPoint.transform.position.x, 0f, spawnPoint.transform.position.z);

//                    temp.RequestPathGuest(targetChairList[currentChairCount], gridFieldInfo);
//                    UpandCount();
//                    TableSet();

//                }
//            }
//        }

//        #endregion
//    }

//}