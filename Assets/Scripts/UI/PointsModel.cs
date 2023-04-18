using System;
using UnityEngine;

namespace ScorePoints.Model
{
    public class PointsModel : MonoBehaviour
    {
        public event Action OnScoreChanged;

        private int currentPointsScore;
        private int highScore;

        public int CurrentPointsScore { get => currentPointsScore; set => currentPointsScore = value; }
        public int HighScore => highScore;

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

        public void UpdateScore()
        {
            OnScoreChanged?.Invoke();
        }
    } 
}