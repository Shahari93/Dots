using System;
using UnityEngine;

namespace Dots.PauseGame.Model
{
    /// <summary>
    /// Class that invokes that the game is paused 
    /// </summary>
	public class PauseGameUIModel : MonoBehaviour
    {
        public static event Action OnGamePaused;

        private void OnApplicationPause(bool pause)
        {
            OnGamePaused?.Invoke();
        }
    }
}