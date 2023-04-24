using System;
using UnityEngine;

namespace Dots.ScorePoints.Model
{
    public class PointsModel : MonoBehaviour
    {

        static int currentPointsScore;
        static int highScore;

        public static int CurrentPointsScore { get => currentPointsScore; set => currentPointsScore = value; }
        public static int HighScore { get => highScore; set => highScore = value; }

        public event Action OnScoreChanged;

        void OnEnable()
        {
            highScore = PlayerPrefs.GetInt("HighScore", 0);
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
            }
        }

    }
}