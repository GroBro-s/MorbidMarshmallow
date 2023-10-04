/*
* Grobros
* https://github.com/GroBro-s
*/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoaderMB : MonoBehaviour
{
	[SerializeField] private GameObject loadingScreen;
	[SerializeField] private Slider slider;
	[SerializeField] private TextMeshProUGUI progressText;

	IEnumerator LoadAsynchronously(int scene)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

		loadingScreen.SetActive(true);

		while(!operation.isDone)
		{
			UpdateLoadingInformation(operation);

			yield return null;
		}
	}

	private void UpdateLoadingInformation(AsyncOperation operation)
	{
		float progress = Mathf.Clamp01(operation.progress / 0.9f);

		slider.value = progress;
		progressText.text = progress * 100f + "%";
	}

	public void LoadMainLevelScene()
	{
		StartCoroutine(LoadAsynchronously(0));
	}

	public void LoadMainMenu()
	{
		StartCoroutine(LoadAsynchronously(1));
	}

	public void LoadOptionsMenu()
	{
		StartCoroutine(LoadAsynchronously(2));
	}
}
