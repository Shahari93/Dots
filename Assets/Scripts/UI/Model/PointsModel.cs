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

        void OnEnable()
        {
            highScore = PlayerPrefs.GetInt("HighScore", 0);
        }

        void Awake()
        {
            if (Instance != null)
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
        }

        public void ResetScore()
        {
            currentPointsScore = 0;
        }

        public void ResetScoreFromPause()
        {
            currentPointsScore = 0;
        }

        void CheckForHighScore()
        {
            if (currentPointsScore > highScore)
            {
                highScore = currentPointsScore;
                PlayerPrefs.SetInt("HighScore", highScore);
                highScore = PlayerPrefs.GetInt("HighScore");
            }
        }

        private void OnDisable()
        {
            CheckForHighScore();
        }
    }
}