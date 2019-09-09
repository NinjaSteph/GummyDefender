using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void LoadStartMenu() {
		SceneManager.LoadScene("StartMenu");
	}

	public void LoadGameOver() {
		SceneManager.LoadScene("GameOver");
	}

	public void LoadGame() {
		SceneManager.LoadScene("Game");
	}

	public void QuitGame() {
		Application.Quit();
	}

}
