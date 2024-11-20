using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CKProject.Managers;
using CKProject.UI;

namespace MVP.Shop
{
    public class ShopPresenter : MonoBehaviour
    {
        public MainPresenter MainPresenter;
        public ShopView View;
        public ShopModel Model;
        // Start is called before the first frame update
        void Start()
        {
        }

        //public void CheckNonConsumable()
        //{
        //    //int check
        //    if (IAPManager.Instance.CheckNonConsumable("ad_block"))
        //    {
        //        //View.ADBlockBtnCheck(check);
        //    }
        //}

        public void CheckNonConsumable(bool check)
        {
            if(check)
            {
                View.ADBlockBtnCheck(check);
            }
        }

        public void AddCrystal(int Crystals)
        {
            MainPresenter.AddCrystals(Crystals);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}