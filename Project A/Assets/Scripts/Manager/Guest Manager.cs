using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using CKProject.SingleTon;

namespace CKProject.Managers
{
    public class GuestManager : SingleTon<GuestManager>
    {
        [SerializeField]
        private Transform spawnPoint;

        [SerializeField]
        private GameObject guestPrefab;

        [SerializeField, ReadOnly]
        private Unit[] guestPools;

        [SerializeField]
        private PathFinding.Grid gridFieldInfo;

        [SerializeField]
        private GameObject[] targetChairList;

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

        [SerializeField] Queue<string> guestQueue;
        public string[] guestArray;

        // Start is called before the first frame update
        private void Start()
        {
            CreateGeust();
        }

        // Update is called once per frame
        private void Update()
        {

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
                }
            }
        }
    }

}