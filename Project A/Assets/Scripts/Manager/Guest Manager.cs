using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using CKProject.SingleTon;

using Grid = PathFinding.Grid;


namespace CKProject.Managers
{
    public class GuestManager : SingleTon<GuestManager>
    {
        //Dictionary<int, GameObject[,]> chairs;
        [Header("생성 관련 변수")] public bool SpawnTrigger;

        [Tooltip("생성 주기")] public float SpawnTimer = 0;
        [Tooltip("최대 생성 갯수")] public int MaxSpawnCount = 0;
        private float currentTimer = 0;
        [Tooltip("현재 어떤 손님을 생성했는지 체크")] private int currentPoolsCount = 0;


        [SerializeField, Space(20f)] private Transform spawnPoint;

        [SerializeField] private GameObject guestPrefab;

        [SerializeField, Space(10)] private Grid gridFieldInfo;

        [SerializeField] private GameObject[] targetChairList;
        private int currentChairCount = 0;

        private Unit[] guestPools;

        private int chairCount = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            // 최대 생성 개수 (2개) / 임시 생성 타임 (3초)
            SpawnTimer = SpawnTimer <= 0 ? 3f : SpawnTimer;
            MaxSpawnCount = MaxSpawnCount <= 0 ? 2 : MaxSpawnCount;
            CreateGeust();
        }

        // Update is called once per frame
        private void Update()
        {
            if(SpawnTrigger)
            {
                currentTimer += Time.deltaTime;
                if(currentTimer > SpawnTimer)
                {
                    SpawnGuest();
                    currentTimer = 0;
                }
            }
        }

        /// <summary>
        /// 손님을 생성하고 위치를 지정
        /// </summary>
        private void SpawnGuest()
        {
            // 생성은 최대 2번
            //int SpawnCount = Random.Range(1, MaxSpawnCount);
            int i = 0;
            for(; i < 1; i++)
            {
                guestPools[currentPoolsCount].gameObject.SetActive(true);
                guestPools[currentPoolsCount].transform.position = new Vector3(spawnPoint.transform.position.x, 0f, spawnPoint.transform.position.z);
                guestPools[currentPoolsCount].RequestPathGuest(targetChairList[currentChairCount], gridFieldInfo);
                UpandCount();
            }
        }

        private void UpandCount()
        {
            currentPoolsCount++;
            currentChairCount++;
            if (currentChairCount >= targetChairList.Length)
                currentChairCount = 0;
            if (currentPoolsCount >= guestPools.Length)
                currentPoolsCount = 0;
        }

        private void CreateGeust()
        {
            chairCount = targetChairList.Length;
            guestPools = new Unit[chairCount];
            for(int i = 0; i < chairCount; i++)
            {
                try
                {
                    guestPools[i] = Instantiate(guestPrefab).GetComponent<Unit>();
                }
                catch
                {
                    Debug.LogError($"this Entity haven't Unit Component.\nPlz Add Unit Component");
                }
                finally
                {
                    guestPools[i].gameObject.SetActive(false);
                    guestPools[i].transform.parent = transform;
                }
            }
        }
    }

}