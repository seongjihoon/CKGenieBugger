using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

namespace CKProject.UI
{
    public class MainPresenter : MonoBehaviour
    {
        public MainModel Model;
        public MainView View;

        private RewardedAd rewardedAd;
        private BannerView bannerView;
        private string rewardedAdUnitId;
        private string AdUnitId; 
        private void Awake()
        {
            //MobileAds.Initialize((InitializationStatus initStatus) =>
            //{
            //    // 
            //});
            BannerRewardedAD();
            RewardedInitAd();
        }

        private void Start()
        {
            bannerView.Show();
        }

        #region bannerView 
        private void BannerRewardedAD()
        {
#if UNITY_ANDROID
            AdUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            //AdUnitId = "ca-app-pub-3940256099942544/6300978111;
#else
            AdUnitId = "unexpected_platform";
#endif
            bannerView = new BannerView(AdUnitId, AdSize.Banner, AdPosition.Top);
        }

        public void LoadAd()
        {
            // create an instance of a banner view first.
            if (bannerView == null)
            {
                BannerRewardedAD();
            }

            // create our request used to load the ad.
            AdRequest adRequest = new AdRequest.Builder().Build();

            // send the request to load the ad.
            Debug.Log("Loading banner ad.");
            bannerView.LoadAd(adRequest);
        }


        #endregion


        #region RewardedAD

        private void RewardedInitAd()
        {
            //adUnitId
#if UNITY_ANDROID
            rewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

#region Legarcy
            //rewardedAd = new RewardedAd(adUnitId);
#endregion

            RewardedAd.Load(rewardedAdUnitId, new AdRequest.Builder().Build(), LoadCallback);
        }

        public void LoadCallback(RewardedAd rewardedAd, LoadAdError loadAdError)
        {
            if (rewardedAd == null)
            {
                Debug.Log($"Rewarded Failed : {loadAdError.GetMessage()}");

            }
            else
            {
                Debug.Log("Rewarded Success");
                this.rewardedAd = rewardedAd;
            }
        }

        public void ShowAds()
        {
            if (rewardedAd != null & rewardedAd.CanShowAd()) 
            {
                rewardedAd.Show(GetReward);
            }
        }

        public void GetReward(Reward reward)
        {
            if(reward.Amount >0 )
            {
                // º¸»ó È¹µæ.
                RewardedInitAd();
            }
        }

#endregion

        private void MoneyUpdate(ref int[] money)
        {
            Theorem();
            MyMoneyToString(money, Model.Index, Model.unitMoney);
        }

        private void Theorem()
        {
            for (int i = 0; i < Model.Size; i++)
            {
                if (Model.Money[i] > 0)
                    Model.Index = i;
            }

            for (int i = 0; i <= Model.Index; i++)
            {
                if (Model.Money[i] >= 1000)
                {
                    Model.Money[i] -= 1000;
                    Model.Money[i + 1] += 1;
                }

                if (Model.Money[i] < 0)
                {
                    if (Model.Index > 1)
                    {
                        Model.Money[i + 1] -= 1;
                        Model.Money[i] += 1000;
                    }
                }
            }
        }

        public void AddMoney(int money)
        {
            int index = 0;
            int memo = 0;
            while (money > 1000)
            {
                memo = money % 1000;
                Model.Money[index] += memo;
                index++;
                money /= 1000;
            }
            Model.Money[index] += memo;
            Theorem();
            MyMoneyToString(Model.Money, Model.Index, Model.unitMoney);
        }

        public void AddMoney(int[] getMoney)
        {
            for (int i = 0; i < getMoney.Length; i++)
            {
                Model.Money[i] += getMoney[i];
            }
            MoneyUpdate(ref Model.Money);
        }

        public void SubMoney(int[] useMoney)
        {
            for (int i = 0; i < useMoney.Length; i++)
            {
                Model.Money[i] -= useMoney[i];
            }
            MoneyUpdate(ref Model.Money);
        }

        public void MyMoneyToString(int[] Money, int index, string[] unit)
        {
            float a = Money[index];

            if (index > 0)
            {
                float b = Money[index - 1];
                a += b / 1000;
            }

            if (index == 0)
            {
                a += 0;
            }

            string p;
            p = (float)(Math.Truncate(a * 100) / 100) + unit[index];
            View.UpdateMoneyText(p);
        }

        public void SetUpgradePanel()
        {
            View.SetUpgradePanel();
        }
        public void SetSettingPanel()
        {
            View.SetSettingPanel();
        }

        public void SetShopPanel()
        {
            View.SetShopPanel();
        }
    }
}