using UnityEngine;
using Dots.GamePlay.Dot.Bad;
using System.Threading.Tasks;
using UnityEngine.UI;
using System;

namespace Dots.Utils.Pause
{
    public class PauseGameButton : MonoBehaviour
    {
        [SerializeField] Button pauseGameButton;

        private bool isGamePaused;

        private void Awake()
        {
            pauseGameButton.onClick.AddListener(PauseGame);
        }

        private void PauseGame()
        {
            isGamePaused = !isGamePaused;

            if (isGamePaused)
            {
                Time.timeScale = 0;
            }

            else
            {
                Time.timeScale = 1;
            }
        }

        //void OnEnable()
        //{
        //    BadDot.OnLoseGame += SetTimeScale;
        //}

        //// Made static for other classes that want to call the PauseGame Method
        //public async static void SetTimeScale()
        //{
        //    await Task.Delay(50);
        //    Time.timeScale = 0f;
        //}

        //void OnDisable()
        //{
        //    BadDot.OnLoseGame -= SetTimeScale;
        //}
    }
}