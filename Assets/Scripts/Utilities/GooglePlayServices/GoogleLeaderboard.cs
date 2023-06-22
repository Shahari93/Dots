using UnityEngine;
using UnityEngine.UI;

namespace Dots.Utilities.GooglePlayServices.Leaderboard
{
    public class GoogleLeaderboard : MonoBehaviour
    {
        [SerializeField] Button leaderboardButton;

        private void Awake()
        {
            leaderboardButton.onClick.AddListener(ShowLeaderboard);
        }

        private void ShowLeaderboard()
        {
            if (!GoogleServices.Instance.connectedToGooglePlay)
            {
                GoogleServices.Instance.LoginToGooglePlay();
            }
            Social.ShowLeaderboardUI();
        }

        private void OnDestroy()
        {
            leaderboardButton.onClick.RemoveListener(ShowLeaderboard);
        }
    } 
}