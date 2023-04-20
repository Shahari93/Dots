using UnityEngine;

namespace Dots.Utils.Pause
{
	public class PauseGame : MonoBehaviour
	{
        private void OnEnable()
        {
            BadDot.OnLoseGame += SetTimeScale;
        }

        // Made static for other classes that want to call the PauseGame Method
        public static void SetTimeScale()
        {
            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            BadDot.OnLoseGame -= SetTimeScale;
        }
    } 
}