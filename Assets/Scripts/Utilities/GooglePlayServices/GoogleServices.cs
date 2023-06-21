using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

namespace Dots.Utilities.GooglePlayServices
{
    public class GoogleServices : MonoBehaviour
    {
        public static GoogleServices Instance;
        public bool connectedToGooglePlay;


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }

        private void Start()
        {
            LoginToGooglePlay();
        }

        public void LoginToGooglePlay()
        {
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }

        private void ProcessAuthentication(SignInStatus status)
        {
            if (status == SignInStatus.Success)
            {
                connectedToGooglePlay = true;
            }
            else
            {
                connectedToGooglePlay = false;
            }
        }
    }
}