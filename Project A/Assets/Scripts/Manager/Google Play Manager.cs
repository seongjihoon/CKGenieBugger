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
            DontDestroyOnLoad(this);
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
            MainPresenter mainUI = GameObject.Find("UI").GetComponent<MainPresenter>();

            if (status == SignInStatus.Success)
            {
                mainUI.UserInfoSet(PlayGamesPlatform.Instance.GetUserDisplayName(), PlayGamesPlatform.Instance.GetUserId());
            }
            else
            {
                mainUI.UserInfoSet("Not Founded", "Not Founded");
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }

}