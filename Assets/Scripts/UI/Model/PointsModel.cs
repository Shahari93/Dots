using Dots.Utilities.GooglePlayServices;
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
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        // Updating the model data with the new score
        public void IncrementScore(int amount)
        {
            currentPointsScore += amount;
            if (GoogleServices.Instance.connectedToGooglePlay)
            {
                switch (currentPointsScore)
                {
                    case 5:
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQAg", 100.0f, null);
                        break;
                    case 10:
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQAw", 100.0f, null);
                        break;
                    case 20:
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQBA", 100.0f, null);
                        break;
                    case 30:
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQBQ", 100.0f, null);
                        break;
                    case 40:
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQBg", 100.0f, null);
                        break;
                    case 50:
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQBw", 100.0f, null);
                        break;
                    case 60:
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQCA", 100.0f, null);
                        break;
                    case 70:
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQCQ", 100.0f, null);
                        break;
                    case 80:
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQCg", 100.0f, null);
                        break;
                    case 90:
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQCw", 100.0f, null);
                        break;
                    case 100:
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQDA", 100.0f, null);
                        break;
                }
            }
        }

        public void ResetScore()
        {
            currentPointsScore = 0;
        }

        public void ResetScoreFromPause()
        {
            currentPointsScore = 0;
        }

        public void CheckForHighScore()
        {
            if (currentPointsScore > highScore)
            {
                highScore = currentPointsScore;
                PlayerPrefs.SetInt("HighScore", highScore);
                highScore = PlayerPrefs.GetInt("HighScore");
            }
        }

        void OnDisable()
        {
            CheckForHighScore();
        }
    }
}