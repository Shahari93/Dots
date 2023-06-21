using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;

public class GoogleLeaderboard : MonoBehaviour
{
    public static GoogleLeaderboard Instance;
    public bool connectedToGooglePlay;

    [SerializeField] Button leaderboardButton;

    private void Awake()
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

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        leaderboardButton.onClick.AddListener(ShowLeaderboard);
    }

    private void Start()
    {
        LoginToGooglePlay();
    }

    private void LoginToGooglePlay()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    private void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            connectedToGooglePlay = true;
        }
        else
        {
            connectedToGooglePlay = false;
        }
    }

    private void ShowLeaderboard()
    {
        if (!connectedToGooglePlay)
        {
            LoginToGooglePlay();
        }
        Social.ShowLeaderboardUI();
    }

    private void OnDestroy()
    {
        leaderboardButton.onClick.RemoveListener(ShowLeaderboard);
    }
}