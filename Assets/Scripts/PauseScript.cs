using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

	bool paused = false;
	public Sprite pauseSprite;
	public Sprite playSprite;
	public GameObject pauseButton;

	private Button sr;

	void Start () {
		gameObject.SetActive (false);
		sr = pauseButton.GetComponent<Button> ();
	}

	public void PauseGame() {
		if (!paused) {
			paused = true;
			gameObject.SetActive (true);
			sr.image.overrideSprite = playSprite;
		} else {
			paused = false;
			gameObject.SetActive (false);
			sr.image.overrideSprite = pauseSprite;
		}
	}

	public void RestartGame() {
		gameObject.SetActive (false);
		paused = false;
		sr.image.overrideSprite = pauseSprite;
	}
}
