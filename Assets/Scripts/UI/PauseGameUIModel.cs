using System;
using UnityEngine;

namespace Dots.PauseGame.Model
{
	public class PauseGameUIModel : MonoBehaviour
	{
		public event Action OnGameLosed;

        public void GamePaused()
        {    
            UpdateGamePaused();
        }

        private void UpdateGamePaused()
        {
            OnGameLosed?.Invoke();
        }
    } 
}