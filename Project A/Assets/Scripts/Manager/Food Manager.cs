using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.SingleTon;
using CKProject.Interactable;
using CKProject.EditorUtils;
using static CKProject.Managers.MissionManager;

namespace CKProject.Managers
{
    public class FoodManager : SingleTon<FoodManager>
    {
        public enum DataCurrent
        {
            Index = 0,
            Stage_Code,
            Food_Code,
            Food_Level,
            Upgrade_Cost,
            Revenue,
            Time,
        }

        #region struct
        [System.Serializable]
        public struct Stage_Food
        {
            public int Stage;
            public int FoodCode;

            public Stage_Food(int stage, int code)
            {
                Stage = stage;
                FoodCode = code;
            }
        }

        [System.Serializable]
        public struct UpgradeFoodData
        {
            public int Level;
            public int[] Upgrade_Cost;
            public int[] Revenue;
            public int UpgradeIndex;
            public int RevenueIndex;
            public float CreateTime;
            
            public UpgradeFoodData(int Dump = 0)
            {
                Level = 0;
                Upgrade_Cost = null;
                UpgradeIndex = 0;
                RevenueIndex = 0;
                Revenue = null;
                CreateTime = 0;
            }

            public static UpgradeFoodData Initialize(string[] str)
            {
                UpgradeFoodData data = new UpgradeFoodData();

                data.Level = int.Parse(str[3]);
                if (str[4] != "Max" && str[4] != "MAX")
                {
                    data.Upgrade_Cost = StringToIntArray(str[4], ref data.UpgradeIndex);  // memory dump가 일어날 수 있음
                }
                else
                    Debug.Log("AA");
                    //data.Upgrade_Cost = ;
                data.Revenue = StringToIntArray(str[5], ref data.RevenueIndex);
                data.CreateTime = float.Parse(str[6]);
                return data;
            }

            public static int[] StringToIntArray(string str, ref int index)
            {
                List<int> arr = new List<int>();
                int money = 0, count = 0;

                while (count + 3 < str.Length)
                {
                    money = int.Parse(str.Substring(count, 3));
                    arr.Add(money);
                    count += 3;
                }
                money = int.Parse(str.Substring(count, str.Length - count));
                arr.Add(money);
                return Theorem(arr.ToArray(), ref index);
            }

            private static int[] Theorem(int[] money,ref int index)
            {
                for (int i = 0; i < money.Length; i++)
                {
                    if (money[i] > 0)
                        index = i;
                }

                for (int i = 0; i <= index; i++)
                {
                    if (money[i] >= 1000)
                    {
                        money[i] -= 1000;
                        money[i + 1] += 1;
                    }

                    if (money[i] < 0)
                    {
                        if (index > 1)
                        {
                            money[i + 1] -= 1;
                            money[i] += 1000;
                        }
                    }
                }
                return money;
            }
        }

        [System.Serializable]
        public struct MissionRevenue
        {
            [ReadOnly] int defaultCreateTime;
            [ReadOnly] float CreateTime;
            [ReadOnly] float defaultRevenue;
            [ReadOnly] float Revenue;

            public static MissionRevenue Init()
            {
                MissionRevenue mission = new MissionRevenue();
                mission.defaultCreateTime = 1;
                mission.CreateTime = 1;
                mission.defaultRevenue = 1;
                mission.Revenue = 1;
                return mission;
            }

            public void Upgrade(MissionData missionData)
            {
                switch (missionData.Target_Type)
                {
                    case 1:
                        CreateTime *= missionData.Get_Count;
                        break;
                    case 2:
                        Revenue *= missionData.Get_Count;
                        break;
                    default:
                        break;
                }
            }

        }
        #endregion


        #region SerilaizeField
        [System.Serializable]
        public class LevelFoodData : SerializableDictionary<Stage_Food, UpgradeFoodData[]>
        {

        };

        [System.Serializable]
        public class RevenueFoodData : SerializableDictionary<EFoodType, MissionRevenue> { };
        #endregion

        #region Food Data Table
        public LevelFoodData LevelFoodDatas = new LevelFoodData();

        public RevenueFoodData RevenueDatas = new RevenueFoodData();
        #endregion

        #region Ingame Food Data
        public FoodScriptableObject[] FoodDataArray;
        public Kitchen[] UseKitchens;
        public SerializableDictionary<EFoodType, int> FoodLevelTable = new SerializableDictionary<EFoodType, int>();
        public int FoodMaxLevel = 0;
        #endregion
        // 우리는 음식의 레벨데이터만 가지고 있으면 된다.

        private void Awake()
        {
            CreateInstance(this);
            CallFoodDataTables();
            InitializedFoodData();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            for (int i = 0; i < GameManager.Instance.Stage; i++)
            {
                UseKitchens[i].gameObject.SetActive(true);
            }
        }

        // 매 스테이지마다 초기화 해줘야함.
        private void InitializedFoodData()
        {
            try
            {
                FoodLevelTable.Clear();
                for (int i = 0; i < FoodDataArray.Length; i++)
                {
                    FoodLevelTable.Add(FoodDataArray[i].foodType, 1);
                }
                foreach(var obj in UseKitchens)
                {
                    obj.gameObject.SetActive(false);
                    RevenueDatas.Add(obj.FoodSO.foodType, MissionRevenue.Init());
                }
            }
            catch
            {
#if UNITY_EDITOR
                Debug.Log("Food Data Array가 비어있습니다.");
#endif
            }
        }

        private void CallFoodDataTables()
        {
            TextAsset tex = (TextAsset)Resources.Load("FoodLevelTable");
            string file = tex.text;
            bool endOfFile = false;
            string[] data_values = file.Split("\n");

            List<UpgradeFoodData> foodDatas = new List<UpgradeFoodData>();
            // 일단 두번 체크
            int count = 0;

            try
            {
                while(!endOfFile)
                {
                    foodDatas.Clear();
                    if (count < 2)
                    {
                        count += 2;
                        continue;
                    }
                    var data_value = data_values[count++].Split(',');

                    if (data_value == null)
                    {
                        endOfFile = true;
                        break;
                    }
                    if (data_value[0] == "")
                    {
                        endOfFile = true;
                        break;
                    }
                    while (data_value[4] != "MAX" && data_value[4] != "Max")
                    {
                        foodDatas.Add(UpgradeFoodData.Initialize(data_value));
                        data_value = data_values[count++].Split(',');
                    }
                    foodDatas.Add(UpgradeFoodData.Initialize(data_value));
                    //FoodMaxLevel = int.Parse(data_value[3]);

                    Stage_Food stage_Food = new Stage_Food(int.Parse(data_value[1]), int.Parse(data_value[2]));

                    LevelFoodDatas.Add(stage_Food, foodDatas.ToArray());
                }
            }
            catch
            {
                Debug.Log("파일 없음");
            }
        }

        public void MissionComplate(MissionData missionData, GameObject disableObject)
        {
            Stage_Food stage = new Stage_Food(GameManager.Instance.Stage, missionData.Get_Type);

            // 돈 비교 
            //if (GameManager.Instance.CompareMoney(
            //    LevelFoodDatas[stage][FoodLevelTable[(EFoodType)missionData.Get_Type]].Upgrade_Cost, LevelFoodDatas[stage][LevelFoodDatas[stage].Length - 1].UpgradeIndex))
            if(GameManager.Instance.CompareMoney(missionData.Mission_Count, missionData.Mission_Count_Index))
            {
                GameManager.Instance.SubMoney(missionData.Mission_Count);
                RevenueDatas[(EFoodType)missionData.Get_Type].Upgrade(missionData);
#if UNITY_EDITOR
                Debug.Log("Success");
                disableObject.SetActive(false);
#endif  
                // 완료되면 버튼 없애주기.
            }
        }

        // 현재 가진 돈을 비교하여 마이너스 해줘야함.
        public void LevelUp(EFoodType foodType)
        {
            Stage_Food stage = new Stage_Food(GameManager.Instance.Stage, (int)foodType);

            var levelTable = FoodLevelTable[foodType] - 1; 
            // 돈 비교 
            if (GameManager.Instance.CompareMoney(
                LevelFoodDatas[stage][levelTable].Upgrade_Cost, LevelFoodDatas[stage][levelTable].UpgradeIndex))
            {
                GameManager.Instance.SubMoney(LevelFoodDatas[stage][levelTable].Upgrade_Cost);
                if (FoodLevelTable[foodType] < LevelFoodDatas[stage][LevelFoodDatas[stage].Length - 1].Level)
                {
                    FoodLevelTable[foodType]++;
                    GameManager.Instance.UpgradeDitryFlag = true;
                }
                else
                {
#if UNITY_EDITOR
                    Debug.Log("레벨 맥스");
#endif
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("돈 없음");
#endif
            }

        }

        public UpgradeFoodData GetFoodLevelData(EFoodType foodType)
        {
            Stage_Food stage = new Stage_Food(GameManager.Instance.Stage, (int)foodType);

            return LevelFoodDatas[stage][FoodLevelTable[foodType] - 1];
        }
    }
}