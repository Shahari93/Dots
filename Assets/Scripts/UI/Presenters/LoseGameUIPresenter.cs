using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Audio.Manager;
using Dots.Utilities.FTUE;
using Dots.ScorePoints.Model;
using UnityEngine.SceneManagement;
using Dots.Utilities.GooglePlayServices;

namespace Dots.PauseGame.Presenter
{
    public class LoseGameUIPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text loseGameScoreText;
        [SerializeField] Button loseGameRestartButton;
        [SerializeField] Button returnToMenuButton;

        public static event Action OnRestartClicked;
        public static event Action OnReturnHomeClicked;

        void Awake()
        {
            loseGameRestartButton.onClick.AddListener(RestartGame);
            returnToMenuButton.onClick.AddListener(ReturnToMenu);
            EnableLoseGamePanel();

            if(GoogleServices.Instance.connectedToGooglePlay)
            {
                Social.ReportScore(PointsModel.CurrentPointsScore, GPGSIds.leaderboard_score_leaderboard, null);
            }
        }

        void ReturnToMenu()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            OnReturnHomeClicked?.Invoke();
            SceneManager.LoadScene(1);
            Time.timeScale = 1f;
            CheckForFTUE.LaunchCount++;
        }

        void RestartGame()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            OnRestartClicked?.Invoke();
            Time.timeScale = 1f;
            CheckForFTUE.LaunchCount++;
            PointsModel.Instance.CheckForHighScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        void EnableLoseGamePanel()
        {
            Time.timeScale = 0f;
            int finalScore = PointsModel.CurrentPointsScore;
            if (loseGameScoreText != null)
            {
                loseGameScoreText.text = string.Format("Your score is: {0:0}", finalScore.ToString());
            }
        }

        void OnDestroy()
        {
            loseGameRestartButton.onClick.RemoveListener(RestartGame);
            returnToMenuButton.onClick.RemoveListener(ReturnToMenu);
        }
    }
}