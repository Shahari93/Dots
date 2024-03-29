using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Dots.Audio.Manager;
using UnityEngine.SceneManagement;

namespace Dots.Utilities.SceneLoader
{
	public class AsyncSceneLoader : MonoBehaviour
	{
		[SerializeField] Button loadSceneButton;

        void Awake()
        {
			loadSceneButton.onClick.AddListener(OnPlayButtonPressed);
        }

		void OnPlayButtonPressed()
		{
			AudioManager.Instance.PlaySFX("ButtonClick");
			StartCoroutine(LoadAsyncScene());
		}

        IEnumerator LoadAsyncScene()
		{
			AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync("Dots_Gameplay", LoadSceneMode.Single);
			while (!asyncSceneLoad.isDone)
			{
				yield return null;
			}
		}

        private void OnDestroy()
        {
            loadSceneButton.onClick.RemoveListener(OnPlayButtonPressed);
        }
    } 
}