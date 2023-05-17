using UnityEngine;
using Dots.GamePlay.Dot.Bad;
using UnityEngine.SceneManagement;

public class MoveToNextScene : MonoBehaviour
{
    void OnEnable()
    {
        BadDot.OnLoseGame += MoveToLoseGameScene;
    }

    private void MoveToLoseGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnDisable()
    {
        BadDot.OnLoseGame -= MoveToLoseGameScene;
    }
}