using System;
using UnityEngine;
using UnityEngine.UI;

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
        }

        private void PauseGame()
        {
            if (!isGamePaused)
            {
                darkenPanel.gameObject.SetActive(true);
                pauseGamePanel.gameObject.SetActive(true);
                isGamePaused = true;
                Time.timeScale = 0;
            }
        }

        private void UnPauseGame()
        {
            if (isGamePaused)
            {
                darkenPanel.gameObject.SetActive(false);
                pauseGamePanel.gameObject.SetActive(false);
                Time.timeScale = 1;
                isGamePaused = false;
            }
        }
    }
}