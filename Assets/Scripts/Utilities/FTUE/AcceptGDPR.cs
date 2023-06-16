using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Audio.Manager;
using UnityEngine.SceneManagement;

namespace Dots.Utilities.GDPR
{
    public class AcceptGDPR : MonoBehaviour
    {
        [SerializeField] Button acceptButton;

        static bool didAcceptGdpr;
        public static bool DidAcceptGdpr
        {
            get
            {
                return didAcceptGdpr;
            }
            set
            {
                didAcceptGdpr = value;
            }
        }

        void Awake()
        {
            acceptButton.onClick.AddListener(MoveToStartingScene);
        }

        void MoveToStartingScene()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            AudioManager.Instance.PlayMusic("ThemeMusic");
            didAcceptGdpr = true;
            PlayerPrefs.SetInt("AcceptGDPR", Convert.ToInt32(didAcceptGdpr));
        }

        void OnDestroy()
        {
            acceptButton.onClick.RemoveListener(MoveToStartingScene);
        }
    }
}