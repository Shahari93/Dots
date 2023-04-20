using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Dots.PauseGame.Model;
using Dots.ScorePoints.Model;
using UnityEngine.SceneManagement;

namespace Dots.PauseGame.Presenter
{
    public class PauseGameUIPresenter : MonoBehaviour
    {
        [SerializeField] PauseGameUIModel pauseGameModel;
        [SerializeField] GameObject loseGameGO;
        [SerializeField] Image loseGamePanel;
        [SerializeField] TMP_Text loseGameScoreText;
        [SerializeField] Button loseGameRestartButton;

        private void OnEnable()
        {
            BadDot.OnLoseGame += EnableLoseGamePanel;

            loseGameRestartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
        }

        private void EnableLoseGamePanel()
        {
            loseGamePanel.gameObject.SetActive(true);
            loseGameGO.gameObject.SetActive(true);
            int finalScore = PointsModel.CurrentPointsScore;
            if (loseGameScoreText != null)
            {
                loseGameScoreText.text = "Your score is: " + finalScore.ToString();
            }
        }

        private void OnDisable()
        {
            BadDot.OnLoseGame -= EnableLoseGamePanel;
        }
    }
}