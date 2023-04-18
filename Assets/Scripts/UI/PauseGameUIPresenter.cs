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
        [SerializeField] Image loseGamePanel;
        [SerializeField] TMP_Text loseGameScoreText;
        [SerializeField] Button loseGameRestartButton;

        private void OnEnable()
        {
            pauseGameModel.OnGameLosed += UpdateGameFinalScore;
            BadDot.OnLoseGame += EnableLoseGamePanel;

            loseGameRestartButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
        }

        private void UpdateGameFinalScore()
        {
            if (loseGameScoreText != null)
            {
                loseGameScoreText.text = "Score: " + PointsModel.CurrentPointsScore.ToString();
            }
        }

        private void EnableLoseGamePanel()
        {
            loseGamePanel.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            pauseGameModel.OnGameLosed -= UpdateGameFinalScore;
            BadDot.OnLoseGame -= EnableLoseGamePanel;
        }
    }
}