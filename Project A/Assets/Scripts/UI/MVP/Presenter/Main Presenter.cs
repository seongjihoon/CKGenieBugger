using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using CKProject.Managers;
using UnityEngine.InputSystem;
using CKProject.Interactable;
using MVP.Upgrade;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        private Camera mainCamera;
        public EventSystem eventSystem;
        public GraphicRaycaster graphicRaycaster;

        //private InputAction Mouse;
        private void Awake()
        {
            //Mouse = InputSystem.actions["Mouse"];
            mainCamera = Camera.main;
            BannerRewardedAD();
            RewardedInitAd();
        }

        private void Start()
        {
            //bannerView.Show();
        }

        private void Update()
        {
#if UNITY_ANDROID
            if(Mouse.current.leftButton.wasPressedThisFrame || Touchscreen.current?.primaryTouch.press.isPressed == true)
            {
                Vector2 inputPosition;

                // ���콺 �Է� ó��
                if (Mouse.current.leftButton.isPressed)
                {
                    inputPosition = Mouse.current.position.ReadValue();
                }
                // ��ġ �Է� ó��
                else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
                {
                    inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                }
                else
                {
                    return;
                }

                // UI üũ: ��ġ �Ǵ� Ŭ���� ��ġ�� UI ��� ������ Ȯ��
                if (IsPointerOverUI(inputPosition))
                {
                    //Debug.Log("UI ������ Ŭ�� �Ǵ� ��ġ�� �߻��߽��ϴ�.");
                    return;  
                }
                TouchInteract(inputPosition);
            }
#else
            if(Mouse.IsPressed())
            {
                Debug.Log("Hello");
            }

#endif
        }
        // UI ��� ������ Ŭ���� �߻��ߴ��� Ȯ���ϴ� �Լ�
        private bool IsPointerOverUI(Vector2 screenPosition)
        {
            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = screenPosition
            };

            // �׷��� UI�� Raycast ��� ã��
            var results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerData, results);

            return results.Count > 0;  // ����� ������ UI ���� �ִٴ� �ǹ�
        }

        private void TouchInteract(Vector3 inputPosition)
        {
            // Ŭ���� ��ġ�� ���� ��ǥ�� ��ȯ
            Ray ray = mainCamera.ScreenPointToRay(inputPosition);
            RaycastHit hit;

            View.UpgradePanel.gameObject.SetActive(false);
            // Raycast�� �߻��Ͽ� Ŭ���� ��ġ�� ������Ʈ ����
            if (Physics.Raycast(ray, out hit))
            {
                // Ŭ���� ��ġ�� ������Ʈ�� ó��
                GameObject clickedObject = hit.collider.gameObject;
                Debug.Log($"Ŭ���� ������Ʈ: {clickedObject.name}");

                // ������Ʈ ��ȣ�ۿ� �Լ� ȣ��
                InteractWithObject(clickedObject);
            }
        }

        private void InteractWithObject(GameObject clickedObject)
        {
            Kitchen kitchenObject = clickedObject.GetComponentInParent<Kitchen>();
            if (kitchenObject != null)
            {
                // ���� �� ������ ����
                View.SetUpgradePanel(kitchenObject.gameObject);
                //Debug.Log("�ش� ������Ʈ�� ��ȣ�ۿ��� �� �ֽ��ϴ�.");
            }
            
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
                // ���� ȹ��.
                RewardedInitAd();
            }
        }

#endregion

        public void UpdateMoney()
        {
            Model.MoneyData.Theorem();
            MyMoneyToString(Model.Money, Model.Index, Model.unitMoney);
        }

        private void MoneyUpdate(int[] money)
        {
            Model.MoneyData.Theorem();
            MyMoneyToString(money, Model.Index, Model.unitMoney);
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
            Model.MoneyData.Theorem();
            MyMoneyToString(Model.Money, Model.Index, Model.unitMoney);
        }

        public void AddMoney(int[] getMoney)
        {
            for (int i = 0; i < getMoney.Length; i++)
            {
                Model.Money[i] += getMoney[i];
            }
            MoneyUpdate(Model.Money);
        }

        public void SubMoney(int[] useMoney)
        {
            for (int i = 0; i < useMoney.Length; i++)
            {
                Model.Money[i] -= useMoney[i];
            }
            MoneyUpdate(Model.Money);
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

        public void SetMissionPanel()
        {
            View.SetMissionPanel();
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