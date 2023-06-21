using UnityEngine;
using UnityEngine.UI;

namespace Dots.Utilities.GooglePlayServices.Achievements
{
    public class GoogleAchievements : MonoBehaviour
    {
        [SerializeField] Button achievementsButton;

        private void Awake()
        {
            achievementsButton.onClick.AddListener(ShowAchievements);
        }

        private void ShowAchievements()
        {
            if (!GoogleServices.Instance.connectedToGooglePlay)
            {
                GoogleServices.Instance.LoginToGooglePlay();
            }
            Social.ShowAchievementsUI();
        }

        private void OnDestroy()
        {
            achievementsButton.onClick.RemoveListener(ShowAchievements);
        }
    } 
}