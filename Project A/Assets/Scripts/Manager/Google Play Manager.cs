using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using CKProject.UI;

namespace CKProject.Managers
{
    public class GooglePlayManager : MonoBehaviour
    {
        public static GooglePlayManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

        }

        // Start is called before the first frame update
        void Start()
        {
            GPGS_LogIn();
        }

        public void GPGS_LogIn()
        {
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }

        public void ProcessAuthentication(SignInStatus status)
        {
            if (status == SignInStatus.Success)
            {
                MainPresenter mainUI = GameObject.Find("Main UI Manager").GetComponent<MainPresenter>();
                mainUI.UserInfoSet(PlayGamesPlatform.Instance.GetUserDisplayName(), PlayGamesPlatform.Instance.GetUserId());
            }
            else
            {

            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }

}