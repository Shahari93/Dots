using System;
using UnityEngine;

namespace Dots.PauseGame.Model
{
    /// <summary>
    /// Class that invokes that the game is paused 
    /// </summary>
	public class PauseGameUIModel : MonoBehaviour
	{
		public event Action OnGamePaused;

        public void GamePaused()
        {    
            UpdateGamePaused();
        }

        private void UpdateGamePaused()
        {
            OnGamePaused?.Invoke();
        }
    } 
}