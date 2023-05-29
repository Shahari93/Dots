using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            didAcceptGdpr = true;
            PlayerPrefs.SetInt("AcceptGDPR", Convert.ToInt32(didAcceptGdpr));
        }
    } 
}