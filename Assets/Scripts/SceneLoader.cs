using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	[SerializeField] float delayInSeconds = 2f;

	public void LoadStartMenu() {
		SceneManager.LoadScene("StartMenu");
	}

	public void LoadGameOver() {
		StartCoroutine(WaitAndLoad());
	}

	private IEnumerator WaitAndLoad() {
		yield return new WaitForSeconds(delayInSeconds);
		SceneManager.LoadScene("GameOver");
	}

	public void LoadGame() {
		SceneManager.LoadScene("Game");
		GameSession gameSession = FindObjectOfType<GameSession>();
		if (gameSession) {
			gameSession.ResetGame();
		}
	}

	public void QuitGame() {
		Application.Quit();
	}

}
