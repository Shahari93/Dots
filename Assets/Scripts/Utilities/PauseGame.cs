using UnityEngine;

namespace Dots.Utils.Pause
{
	public class PauseGame : MonoBehaviour
	{
        private void OnEnable()
        {
            BadDot.OnLoseGame += PauseGameOnLose;
        }

        private void PauseGameOnLose()
        {
            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            BadDot.OnLoseGame -= PauseGameOnLose;
        }
    } 
}