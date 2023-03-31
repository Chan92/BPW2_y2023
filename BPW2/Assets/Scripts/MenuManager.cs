using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void ReloadScene() {
		SetScene(SceneManager.GetActiveScene().name);
	}

	public void GoGameScene() {
		SetScene("GameMap_Level0");
	}

	public void GoMenuScene() {
		SetScene("StartMenu");
	}

	private void SetScene(string sceneId) {
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		Resources.UnloadUnusedAssets();
		SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
	}

	public void ExitGame() {
		Application.Quit();
	}

}
