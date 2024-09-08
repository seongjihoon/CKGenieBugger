using CKProject.FSM;
using CKProject.Managers;
using UnityEngine;
using CKProject.CustomSystem;
using CKProject.Interactable;

namespace CKProject
{
    public class Player : MonoBehaviour
    {
        public CustomCollision ppp;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ppp = CollisionManager.Instance.CheckCollision(transform);
        }
    }

}