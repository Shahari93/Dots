using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Dots.Utils.FTUE;
using Dots.GamePlay.Dot.Bad;
using Dots.ScorePoints.Model;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Dots.PauseGame.Presenter
{
    public class PauseGameUIPresenter : MonoBehaviour
    {
        [SerializeField] GameObject loseGameGO;
        [SerializeField] Image loseGamePanel;
        [SerializeField] TMP_Text loseGameScoreText;
        [SerializeField] Button loseGameRestartButton;
        [SerializeField] Button returnToMenuButton;

        void OnEnable()
        {
            BadDot.OnLoseGame += EnableLoseGamePanel;

            loseGameRestartButton.onClick.AddListener(RestartGame);
            returnToMenuButton.onClick.AddListener(ReturnToMenu);
        }

        void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
            CheckForFTUE.LaunchCount++;
        }

        void RestartGame()
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1f;
            CheckForFTUE.LaunchCount++;
        }

        async void EnableLoseGamePanel()
        {
            await Task.Delay(100);
            Time.timeScale = 0f;
            loseGamePanel.gameObject.SetActive(true);
            loseGameGO.gameObject.SetActive(true);
            int finalScore = PointsModel.CurrentPointsScore;
            if (loseGameScoreText != null)
            {
                loseGameScoreText.text = string.Format("Your score is: {0:0}", finalScore.ToString());
            }
        }

        void OnDisable()
        {
            BadDot.OnLoseGame -= EnableLoseGamePanel;
        }
    }
}