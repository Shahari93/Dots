using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


namespace Dots.Utils.SceneLoader
{
	public class AsyncSceneLoader : MonoBehaviour
	{
		[SerializeField] Button loadSceneButton;

        void Start()
        {
			loadSceneButton.onClick.AddListener(OnPlayButtonPressed);
        }

		void OnPlayButtonPressed()
		{
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
	} 
}