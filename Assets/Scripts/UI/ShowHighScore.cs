using TMPro;
using UnityEngine;

namespace Dots.ScorePoints.UI
{
    public class ShowHighScore : MonoBehaviour
    {
        [SerializeField] TMP_Text highScoreText;

        void OnEnable()
        {
            SetHighScoreText();
        }

        void SetHighScoreText()
        {
            highScoreText.text = "High score: " + PlayerPrefs.GetInt("HighScore");
        }
    } 
}