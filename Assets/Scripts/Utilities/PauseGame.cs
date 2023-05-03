using UnityEngine;
using Dots.GamePlay.Dot.Bad;
using System.Threading.Tasks;

namespace Dots.Utils.Pause
{
	public class PauseGame : MonoBehaviour
	{
        void OnEnable()
        {
            BadDot.OnLoseGame += SetTimeScale;
        }

        // Made static for other classes that want to call the PauseGame Method
        public async static void SetTimeScale()
        {
            await Task.Delay(50);
            Time.timeScale = 0f;
        }

        void OnDisable()
        {
            BadDot.OnLoseGame -= SetTimeScale;
        }
    } 
}