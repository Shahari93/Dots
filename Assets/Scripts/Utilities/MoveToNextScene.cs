using UnityEngine;
using Dots.GamePlay.Dot.Bad;
using UnityEngine.SceneManagement;

public class MoveToNextScene : MonoBehaviour
{
    private static int loseCount;
    public static int LoseCount
    {
        get
        {
            return loseCount;
        }
        set
        {
            loseCount = value;
        }
    }

    void OnEnable()
    {
        BadDot.OnLoseGame += MoveToLoseGameScene;
    }

    private void MoveToLoseGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Checking how many times the player lost so we can show him the In-App Review
        // check if the key exists.
        // if so, add to count
        if (PlayerPrefs.HasKey("LoseCount"))
        {
            // get the current count
            loseCount = PlayerPrefs.GetInt("LoseCount");
            // increment the count
            loseCount += 1;
            // set to PlayerPrefs
            PlayerPrefs.SetInt("LoseCount", loseCount);
        }
        // if not, first time launched, add key
        else
        {
            PlayerPrefs.SetInt("LoseCount", 1);
        }
    }

    void OnDisable()
    {
        BadDot.OnLoseGame -= MoveToLoseGameScene;
    }
}