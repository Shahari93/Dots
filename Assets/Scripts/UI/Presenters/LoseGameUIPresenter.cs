using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Dots.Utils.FTUE;
using Dots.Audio.Manager;
using Dots.ScorePoints.Model;
using UnityEngine.SceneManagement;

namespace Dots.PauseGame.Presenter
{
    public class LoseGameUIPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text loseGameScoreText;
        [SerializeField] Button loseGameRestartButton;
        [SerializeField] Button returnToMenuButton;

        void OnEnable()
        {
            loseGameRestartButton.onClick.AddListener(RestartGame);
            returnToMenuButton.onClick.AddListener(ReturnToMenu);
        }

        private void Awake()
        {
            EnableLoseGamePanel();
        }

        void ReturnToMenu()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            SceneManager.LoadScene(1);
            Time.timeScale = 1f;
            CheckForFTUE.LaunchCount++;
        }

        void RestartGame()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
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
    }
}