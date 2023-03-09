using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SceneLoader : MonoBehaviour
{
	public GameObject loadingScreen;
	public Slider slider;
	[SerializeField]
	public TextMeshProUGUI progressText;

	IEnumerator LoadAsynchronously(int scene)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

		loadingScreen.SetActive(true);

		while(!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / 0.9f);

			slider.value = progress;
			progressText.text = progress * 100f + "%";

			yield return null;
		}
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
