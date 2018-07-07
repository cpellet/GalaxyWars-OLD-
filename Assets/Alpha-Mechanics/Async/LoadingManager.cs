using System.Collections;
using UnityEngine;using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour {

	public Slider slider;
	public Text percentageText;

	public void LoadLevel(int sceneIndex)
	{
		StartCoroutine(LoadAsynchronously(sceneIndex));
	}

	IEnumerator LoadAsynchronously(int sceneIndex)
	{
		// Store loading async operation into object we can access
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		//Update UI with progress while operation is running
		while(!operation.isDone)
		{
			//Ignore Unity's activation process
			float progress = Mathf.Clamp01(operation.progress / .9f);
			slider.value = progress;
			if (progress >= .2f){
				percentageText.text = progress * 100f + "%";
			}
			yield return null;
		}
	}
}
