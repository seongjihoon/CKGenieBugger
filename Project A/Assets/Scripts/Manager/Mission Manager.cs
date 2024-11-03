using CKProject.SingleTon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKProject.Managers
{
    public class MissionManager : MonoBehaviour
    {
        public static MissionManager Instance;
        enum DataCurrent
        {
            Index = 0,
            Stage_Code,
            Mission_Code,
            Mission_Type,
            Mission_Count,
            Target_Type,
            Get_Type,
            Get_Count,
            Mission_Text,
            Get_Text,
        }

        #region struct
        [System.Serializable]
        public struct MissionData
        {
            public int Index;
            public int Stage;
            public int Mission_Code;
            public int Mission_Type;
            public int[] Mission_Count;
            public int Mission_Count_Index;
            public int Target_Type;
            public int Get_Type;
            public float Get_Count;
            public string Mission_Text;
            public string Get_Text;

            public MissionData(string[] str)
            {
                Index = int.Parse(str[0]);
                Stage = int.Parse(str[1]);
                Mission_Code = int.Parse(str[2]);
                Mission_Type = int.Parse(str[3]);
                //Mission_Count = int.Parse(str[4]);
                Mission_Count_Index = 0;
                Mission_Count = StringToIntArray(str[4], ref Mission_Count_Index);
                Target_Type = int.Parse(str[5]);
                Get_Type = int.Parse(str[6]);
                Get_Count = float.Parse(str[7]);
                Mission_Text = str[8];
                Get_Text = str[9];
            }
            
            public static MissionData Initialize(string[] str)
            {
                MissionData data = new MissionData();

                data.Index = int.Parse(str[0]);
                data.Stage = int.Parse(str[1]);
                data.Mission_Code = int.Parse(str[2]);
                data.Mission_Type = int.Parse(str[3]);
                data.Mission_Count = StringToIntArray(str[4], ref data.Mission_Count_Index);
                data.Target_Type = int.Parse(str[5]);
                data.Get_Type = int.Parse(str[6]);
                data.Get_Count = float.Parse(str[7]);
                data.Mission_Text = str[8];
                data.Get_Text = str[9];

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

            private static int[] Theorem(int[] money, ref int index)
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

        #endregion

        #region SerializeField
        [System.Serializable]
        public class StageMission : SerializableDictionary<int, MissionData[]> { };
        #endregion

        #region DataTable
        public StageMission MissionDatas = new StageMission();
        #endregion

        public int MaxPoolCount;
        private int CurrentUseCount = 0;

        public int Count { get { return CurrentUseCount; } set { CurrentUseCount = value; } }

        private void Awake()
        {
            //CreateInstance(this);
            if (Instance == null)
                Instance = this;
            CallMissionDataTables();

            //DontDestroyOnLoad(gameObject);
        }

        public MissionData[] GetStageMission()
        {
            return MissionDatas[GameManager.Instance.Stage];
        }

        public void MissionComplate()
        {
            CurrentUseCount--;
        }

        //public void CheckMissionList()
        //{
        //    // 현재 미션 리스트가 몇 개 있는지 체크
            
        //}

        // 스테이지 별로 노출되어야하는 미션이 달라야함.
        public void CallMissionDataTables()
        {
            TextAsset tex = (TextAsset)Resources.Load("MissionLevelTable");
            string file = tex.text;
            bool endOfFile = false;
            string[] data_values = file.Split("\n");

            List<MissionData> foodDatas = new List<MissionData>();
            // 일단 두번 체크
            int count = 0;

            try
            {
                while (!endOfFile)
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
                    int a = 100 * (MissionDatas.Count + 2);
                    MaxPoolCount = 0;
                    while (data_value.Length > 1 && int.Parse(data_value[2]) < a)
                    {
                        foodDatas.Add(MissionData.Initialize(data_value));
                        data_value = data_values[count++].Split(',');
                        MaxPoolCount++;
                    }
                    count--;

                    MissionDatas.Add(MissionDatas.Count + 1, foodDatas.ToArray());
                }
            }
            catch
            {
                Debug.Log("파일 없음");
            }
        }


    }
}
