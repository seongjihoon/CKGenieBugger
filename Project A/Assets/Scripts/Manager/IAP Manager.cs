using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using MVP.Shop;
using System;

namespace CKProject.Managers
{
    public class IAPManager : MonoBehaviour, IStoreListener
    {
        public static IAPManager Instance;
        public ShopPresenter ShopPresenter;


        IStoreController storeController;

        private string crystal_100 = "crystal100";
        private string ad_block = "adblock";

        private void InitIAP()
        {
            // ��ư �� Ȱ��ȭ
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct(crystal_100, ProductType.Consumable);
            builder.AddProduct(ad_block, ProductType.NonConsumable);

            UnityPurchasing.Initialize(this.GetComponent<IAPManager>(), builder);
        }


        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            storeController = controller;

            ShopPresenter.CheckNonConsumable(CheckNonConsumable(ad_block));
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("�ʱ�ȭ ����: " + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.Log("�ʱ�ȭ ����: " + error + message);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log("���� ����");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            var product = purchaseEvent.purchasedProduct;

            Debug.Log("���� ����: " + product.definition.id);

            if (product.definition.id == crystal_100)
            {
                Debug.Log("ũ����Ż 100�� ���� ����");

                ShopPresenter.AddCrystal(100);
            }
            else if (product.definition.id == ad_block)
            {
                Debug.Log("���� ���� ����");
                
                ShopPresenter.CheckNonConsumable(CheckNonConsumable(ad_block));
            }

            return PurchaseProcessingResult.Complete;
        }

        public void Purchase(string productId)
        {
            storeController.InitiatePurchase(productId);
        }

        private bool CheckNonConsumable(string id)
        {
            // ���� ������ Ȯ��

            var product = storeController.products.WithID(id);
            bool isCheck = false;
            if (product != null)
            {
                isCheck = product.hasReceipt;
                //return true;
            }
            return isCheck;
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            InitIAP();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}