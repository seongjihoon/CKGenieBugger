using CKProject.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVP.Shop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] public GameObject NonADBlockBtn;
        [SerializeField] public GameObject ADBlockBtn;
        // Start is called before the first frame update
        //void Start()
        //{
        //}

        // Update is called once per frame
        void Update()
        {

        }

        public void ADBlockBtnCheck(bool check)
        {
            if(check)
            {
                NonADBlockBtn.SetActive(true);
                ADBlockBtn.SetActive(false);
            }
            else if(!check)
            {
                NonADBlockBtn.SetActive(false);
                ADBlockBtn.SetActive(true);
            }
        }
    }

}