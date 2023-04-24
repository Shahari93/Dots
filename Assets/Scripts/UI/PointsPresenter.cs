using TMPro;
using UnityEngine;
using Dots.ScorePoints.Model;

namespace Dots.ScorePoints.Presenter
{
    /// <summary>
    /// This class responsible of controling the change of data of the points system
    /// Whenever there is a change of state (Point collected) we call a method on the model to update the data
    /// 
    /// </summary>
    public class PointsPresenter : MonoBehaviour
    {
        [SerializeField] PointsModel pointsModel;
        [SerializeField] TMP_Text scoreText;

        void OnEnable()
        {
            pointsModel.OnScoreChanged += OnScoreChanged;
            GoodDot.OnPlayerCollectedDot += IncrementPointsScore;
        }

        void Start()
        {
            Reset();
            UpdateView();
        }

        public void IncrementPointsScore(int amount)
        {
            pointsModel?.IncrementScore(amount);
        }

        public void Reset()
        {
            pointsModel?.ResetScore();
        }

        public void UpdateView()
        {
            if (pointsModel == null)
                return;

            if (scoreText != null)
            {
                scoreText.text = "Score: " + PointsModel.CurrentPointsScore.ToString();
            }
        }

        public void OnScoreChanged()
        {
            UpdateView();
        }

        void OnDisable()
        {
            pointsModel.OnScoreChanged -= OnScoreChanged;
            GoodDot.OnPlayerCollectedDot -= IncrementPointsScore;
        }
    }
}