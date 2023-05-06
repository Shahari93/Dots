using TMPro;
using UnityEngine;
using Dots.ScorePoints.Model;
using Dots.GamePlay.Dot.Good;

namespace Dots.ScorePoints.Presenter
{
    /// <summary>
    /// This class responsible of controling the change of data of the points system
    /// Whenever there is a change of state (Point collected) we call a method on the model to update the data
    /// 
    /// </summary>
    public class PointsPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreText;
        [SerializeField] TMP_Text highScoreText;

        void OnEnable()
        {
            PointsModel.Instance.OnScoreChanged += ScoreChanged;
            PointsModel.Instance.OnHighScorePassed += SetHighScoreText;
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

                highScoreText.text = string.Format("High score: {0:0}", PlayerPrefs.GetInt("HighScore"));
            }
        }

        void ScoreChanged()
        {
            UpdateView();
        }

        void SetHighScoreText()
        {
            UpdateView();
        }

        void OnDisable()
        {
            PointsModel.Instance.OnScoreChanged -= ScoreChanged;
            PointsModel.Instance.OnHighScorePassed -= SetHighScoreText;
            GoodDot.OnPlayerCollectedDot -= IncrementPointsScore;
        }
    }
}