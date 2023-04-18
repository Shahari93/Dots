using TMPro;
using UnityEngine;
using ScorePoints.Model;

namespace ScorePoints.Presenter
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

        private void OnEnable()
        {
            pointsModel.OnScoreChanged += OnScoreChanged;
            GoodDot.OnPlayerCollectedDot += AddPointsScore;
        }

        private void Start()
        {
            Reset();
            UpdateView();
        }

        public void AddPointsScore(int amount)
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
                scoreText.text = "Score: " + pointsModel.CurrentPointsScore.ToString();
            }
        }

        public void OnScoreChanged()
        {
            UpdateView();
        }

        private void OnDisable()
        {
            pointsModel.OnScoreChanged -= OnScoreChanged;
            GoodDot.OnPlayerCollectedDot -= AddPointsScore;
        }
    }
}