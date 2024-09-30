using UnityEngine;
using MVP;
using CKProject.AttributeEditor;

namespace CKProject.UI
{
    public class UseMoneyBtn : MonoBehaviour
    {
        [ArrayElementTitle("")]
        public int[] NeedMoney;

        private Controller controller;
        
        protected virtual void Awake()
        {
            controller = GameObject.Find("UI Manager").GetComponent<Controller>();
        }

        public virtual void UseMoney()
        {
            controller.SubMoney(NeedMoney);
        }
    }

}