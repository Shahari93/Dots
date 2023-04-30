using System;
using UnityEngine;

namespace Dots.ScorePoints.Model
{
    public class PointsModel : MonoBehaviour
    {
        public static PointsModel Instance;

        static int currentPointsScore;
        static int highScore;

        public static int CurrentPointsScore { get => currentPointsScore; set => currentPointsScore = value; }
        public static int HighScore { get => highScore; set => highScore = value; }

        public event Action OnScoreChanged;
        public event Action OnHighScorePassed;

        void OnEnable()
        {
            highScore = PlayerPrefs.GetInt("HighScore", 0);
        }

        void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }

        // Updating the model data with the new score
        public void IncrementScore(int amount)
        {
            currentPointsScore += amount;
            UpdateScore();
        }

        public void ResetScore()
        {
            currentPointsScore = 0;
            UpdateScore();
        }

        void UpdateScore()
        {
            OnScoreChanged?.Invoke();
        }

        void OnDisable()
        {
            if (currentPointsScore > highScore)
            {
                highScore = currentPointsScore;
                PlayerPrefs.SetInt("HighScore", highScore);
                OnHighScorePassed?.Invoke();
            }
        }

    }
}