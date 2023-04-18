using System;
using UnityEngine;

namespace Dots.ScorePoints.Model
{
    public class PointsModel : MonoBehaviour
    {
        public event Action OnScoreChanged;

        private int currentPointsScore;
        private int highScore;

        public int CurrentPointsScore { get => currentPointsScore; set => currentPointsScore = value; }
        public int HighScore => highScore;

        // Updating the model data with the new score
        public void IncrementScore(int amount)
        {
            currentPointsScore += amount;
            if (currentPointsScore >= highScore)
            {
                highScore = currentPointsScore;
            }
            UpdateScore();
        }

        public void ResetScore()
        {
            currentPointsScore = 0;
            UpdateScore();
        }

        private void UpdateScore()
        {
            OnScoreChanged?.Invoke();
        }
    } 
}