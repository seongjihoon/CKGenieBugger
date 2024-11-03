using CKProject.FSM;
using CKProject.Managers;
using UnityEngine;
using CKProject.TriggerSystem;
using CKProject.Interactable;

namespace CKProject
{
    public class Player : MonoBehaviour
    {
        public CustomTrigger ppp;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ppp = GameManager.Instance.TriggerManager.CheckTriggerZone(transform);
        }
    }

}