using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeManager : MonoBehaviour {

	public static SnakeManager instance = null;

	public GameObject foodPrefab;
	public Snake snake;

	// Borders
	public Transform borderTop;
	public Transform borderBottom;
	public Transform borderLeft;
	public Transform borderRight;

	// Score
	public Text scoreText;
	int score;

	// Pause
	public GameObject pausePanel;
	public GameObject pauseButton;
	public Sprite pauseSprite;
	public Sprite playSprite;
	private Button btn;
	bool paused;

	// Game over
	public GameObject gameOverPanel;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	void Start () {
		paused = false;
		btn = pauseButton.GetComponent<Button> ();
		pausePanel.SetActive (false);

		gameOverPanel.SetActive (false);
	
		score = 0;
	}

	public void EatFood (GameObject food) {
		UpdateScore ();
		Destroy(food);
		AddFood ();
	}

	public void Crush () {
		snake.CancelInvoke();
		gameOverPanel.SetActive (true);
	}

	void AddFood () {
		int x = (int) Random.Range(borderLeft.position.x, borderRight.position.x);

		int y = (int) Random.Range(borderBottom.position.y, borderTop.position.y);

		Instantiate (foodPrefab, new Vector2 (x, y), Quaternion.identity);
	}

	void UpdateScore () {
		score++;
		scoreText.text = "" + score;
	}

	public void PauseGame () {
		paused = !paused;
		Debug.Log (pausePanel);
		if (paused) {
			btn.image.overrideSprite = playSprite;
			snake.CancelInvoke ();
			pausePanel.SetActive (true);
		} else {
			btn.image.overrideSprite = pauseSprite;
			snake.InvokeRepeating("Move", 0.3f, 0.1f);
			pausePanel.SetActive (false);
		}
	}

	public void RestartSnake() {
		score = 0;
		scoreText.text = "" + score;
	
		centerSnake ();
		snake.ResetSnake ();

		gameOverPanel.SetActive (false);

		paused = false;
		pausePanel.SetActive (false);
		btn.image.overrideSprite = pauseSprite;
	}

	void centerSnake() {
		int x = (int) (((borderRight.position.x - borderLeft.position.x) / 2) + borderLeft.position.x);
		int y = (int) (((borderTop.position.y - borderBottom.position.y) / 2) + borderBottom.position.y);
		snake.transform.position = new Vector2(x, y);
	}
}
