using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using CKProject.AttributeEditor;
using MVP;

namespace CKProject.UI
{
    // 몰라 일단 상속하지 말아봐.
    public class AdBtn : MonoBehaviour
    {
        private RewardedAd rewardedAd;

        private Controller controller;

        [Header("Manager에서 관리해야함 여긴 임시")]
        public string adUnitId;

        [ArrayElementTitle()]
        public int[] AddRewardMoney;

        private void Start()
        {
            InitAd();
        }

        private void InitAd()
        {            //adUnitId
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif
            controller = GameObject.Find("UI Manager").GetComponent<Controller>();

            #region Legarcy
            //rewardedAd = new RewardedAd(adUnitId);
            #endregion

            RewardedAd.Load(adUnitId, new AdRequest.Builder().Build(), LoadCallback);

        }

        public void LoadCallback(RewardedAd rewardedAd, LoadAdError loadAdError)
        {
            if(rewardedAd == null)
            {
                Debug.Log($"Rewarded Failed : {loadAdError.GetMessage()}");

            }
            else
            {
                Debug.Log("Rewarded Success");
                this.rewardedAd = rewardedAd;
            }
        }

        public void AddMoney()
        {
            controller.AddMoney(AddRewardMoney);
        }

        public void ShowAds()
        {
            if(rewardedAd.CanShowAd())
            {
                rewardedAd.Show(GetRewared);
            }
            else
            {
                Debug.Log("Ad Failed");
            }
        }

        public void GetRewared(Reward reward)
        {
            try
            {
                if (reward.Amount > 0)
                {
                    controller.AddMoney(AddRewardMoney);
                }
                InitAd();
            }
            catch
            {
                Debug.Log("Not Have A Reward");
            }
        }
    }
}