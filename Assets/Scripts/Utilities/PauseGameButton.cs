using UnityEngine;
using UnityEngine.UI;
using Dots.Coins.Model;
using Dots.ScorePoints.Model;
using UnityEngine.SceneManagement;
using Dots.PauseGame.Model;

namespace Dots.Utils.Pause
{
    public class PauseGameButton : MonoBehaviour
    {
        [SerializeField] Image darkenPanel;
        [SerializeField] Image pauseGamePanel;
        [SerializeField] Button pauseGameButton;
        [SerializeField] Button restartGameButton;
        [SerializeField] Button returnToGameButton;
        [SerializeField] Button returnToMainMenuButton;

        private bool isGamePaused;

        private void Awake()
        {
            pauseGameButton.onClick.AddListener(PauseGame);

            returnToGameButton.onClick.AddListener(UnPauseGame);

            restartGameButton.onClick.AddListener(RestartGame);

            returnToMainMenuButton.onClick.AddListener(ReturnToHome);
        }

        private void PauseGame()
        {
            if (!isGamePaused)
            {
                ShowPausePanel(true);
                isGamePaused = true;
                Time.timeScale = 0;
                PauseGameUIModel.OnGamePaused?.Invoke();
            }
        }

        private void UnPauseGame()
        {
            if (isGamePaused)
            {
                ShowPausePanel(false);
                isGamePaused = false;
                Time.timeScale = 1;
            }
        }

        private void ReturnToHome()
        {
            ReturnToFlow(1);
        }

        private void RestartGame()
        {
            ReturnToFlow(0);
        }

        private void ShowPausePanel(bool show)
        {
            darkenPanel.gameObject.SetActive(show);
            pauseGamePanel.gameObject.SetActive(show);
        }

        private void ReturnToFlow(int sceneIndex)
        {
            Time.timeScale = 1;
            isGamePaused = false;
            PointsModel.Instance.ResetScoreFromPause();
            CoinsModel.Instance.ResetCoins();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - sceneIndex);
        }
    }
}