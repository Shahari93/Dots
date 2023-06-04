using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Audio.Manager;
using UnityEngine.SceneManagement;

namespace Dots.Utils.GDPR
{
    public class AcceptGDPR : MonoBehaviour
    {
        [SerializeField] Button acceptButton;

        private static bool didAcceptGdpr;
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

        private void Awake()
        {
            acceptButton.onClick.AddListener(MoveToStartingScene);
        }

        private void MoveToStartingScene()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            AudioManager.Instance.PlayMusic("ThemeMusic");
            didAcceptGdpr = true;
            PlayerPrefs.SetInt("AcceptGDPR", Convert.ToInt32(didAcceptGdpr));
        }
    }
}