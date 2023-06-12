using System;
using UnityEngine;

namespace Dots.PauseGame.Model
{
    /// <summary>
    /// Class that invokes that the game is paused 
    /// </summary>
	public class PauseGameUIModel : MonoBehaviour
    {
        public static Action OnGamePaused;

        void OnApplicationPause(bool pause)
        {
            OnGamePaused?.Invoke();
        }
    }
}