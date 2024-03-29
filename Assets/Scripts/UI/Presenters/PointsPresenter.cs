using TMPro;
using UnityEngine;
using Dots.ScorePoints.Model;
using Dots.GamePlay.Dot.Good;

namespace Dots.ScorePoints.Presenter
{
    /// <summary>
    /// This class responsible of controlling the change of data of the points system
    /// Whenever there is a change of state (Point collected) we call a method on the model to update the data
    /// </summary>
    public class PointsPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreText;
        [SerializeField] TMP_Text highScoreText;

        void OnEnable()
        {
            GoodDot.OnPlayerCollectedDot += IncrementPointsScore;
        }

        void Start()
        {
            Reset();
            UpdateView();
        }

        void IncrementPointsScore(int amount)
        {
            PointsModel.Instance?.IncrementScore(amount);
            UpdateView();
        }

        void Reset()
        {
            PointsModel.Instance?.ResetScore();
        }

        void UpdateView()
        {
            if (PointsModel.Instance == null)
                return;

            if (scoreText != null)
            {
                scoreText.text = string.Format("Score: {0:0}", PointsModel.CurrentPointsScore.ToString());
            }

            if (highScoreText != null)
            {
                //TODO: Think if need to use this
                /*if(PointsModel.HighScore == 0)
                    highScoreText.gameObject.SetActive(false);*/

                highScoreText.text = string.Format("High score: {0:0}", PointsModel.HighScore.ToString());
            }
        }

        void OnDisable()
        {
            GoodDot.OnPlayerCollectedDot -= IncrementPointsScore;
        }
    }
}