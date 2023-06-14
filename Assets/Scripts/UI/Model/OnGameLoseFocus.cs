using System;
using UnityEngine;

namespace Dots.PauseGame.Model
{
    /// <summary>
    /// Class that invokes that the game is paused 
    /// </summary>
	public class OnGameLoseFocus : MonoBehaviour
    {
        public static event Action GameLoseFocus;
        public static Action OnGamePausedOrLoseFocus;

        public static OnGameLoseFocus Instance;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                GameLoseFocus?.Invoke();
            }
        }
    }
}