using Dots.ScorePoints.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowHighScore : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;

    private void OnEnable()
    {
        SetHighScoreText();
    }

    void SetHighScoreText()
    {
        highScoreText.text = "High score: " + PlayerPrefs.GetInt("HighScore");
    }
}
