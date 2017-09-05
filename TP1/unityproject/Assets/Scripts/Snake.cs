using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour {

	public GameObject tailPrefab;
	public GameObject foodPrefab;

	public Transform borderTop;
	public Transform borderBottom;
	public Transform borderLeft;
	public Transform borderRight;

	public GameObject gameOverPaner;

	public Text scoreText;
	int score;
	bool paused = false;


	List<Transform> tail = new List<Transform>();

	Vector2 dir = Vector2.right;

	bool ate, dead = false;

	void Start () {
		score = 0;
		centerSnake ();
		InvokeRepeating("Move", 0.3f, 0.1f);
		gameOverPaner.SetActive (false);
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow) && dir != -Vector2.right)
			dir = Vector2.right;
		else if (Input.GetKey(KeyCode.DownArrow) && dir != Vector2.up)
			dir = -Vector2.up;
		else if (Input.GetKey(KeyCode.LeftArrow) && dir != Vector2.right)
			dir = -Vector2.right;
		else if (Input.GetKey(KeyCode.UpArrow) && dir != -Vector2.up)
			dir = Vector2.up;
	}

	void Move() {
		Vector2 v = transform.position;
		transform.Translate(dir);

		if (ate) {
			// Load Prefab into the world
			GameObject g =(GameObject)Instantiate(tailPrefab,
				v,
				Quaternion.identity);

			// Keep track of it in our tail list
			tail.Insert(0, g.transform);

			// Reset the flag
			ate = false;
		}
		// Do we have a Tail?
		else if (tail.Count > 0) {
			// Move last Tail Element to where the Head was
			tail.Last().position = v;

			// Add to front of list, remove from the back
			tail.Insert(0, tail.Last());
			tail.RemoveAt(tail.Count-1);
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.name.StartsWith("FoodPrefab")) {
			ate = true;
			UpdateScore ();
			Destroy(coll.gameObject);
			AddFood ();
		} else {
			dead = true;
			CancelInvoke();
			gameOverPaner.SetActive (true);
		}
	}

	void AddFood () {
		int x = (int)Random.Range(borderLeft.position.x,
			borderRight.position.x);

		int y = (int)Random.Range(borderBottom.position.y,
			borderTop.position.y);

		Instantiate (foodPrefab,
			new Vector2 (x, y),
			Quaternion.identity);
	}

	void UpdateScore () {
		score++;
		scoreText.text = "" + score;
	}


	public void PauseHandler() {
		if (!paused) {
			paused = true;
			CancelInvoke ();
		} else {
			paused = false;
			InvokeRepeating("Move", 0.3f, 0.1f);
		}
	}

	public void RestartSnake() {
		score = 0;
		scoreText.text = "" + score;
	
		foreach (Transform t in tail) {
			Destroy (t.gameObject);
		}
		tail = new List<Transform> ();
		paused = false;
		dir = Vector2.right;
		InvokeRepeating("Move", 0.3f, 0.1f);
		centerSnake ();
		gameOverPaner.SetActive (false);

	}

	void centerSnake() {
		int x = (int) (((borderRight.position.x - borderLeft.position.x) / 2) + borderLeft.position.x);
		int y = (int) (((borderTop.position.y - borderBottom.position.y) / 2) + borderBottom.position.y);

		transform.position = new Vector2(x, y);
	}

//	void OnGUI() {
//		
//		if(dead) {
//			GUI.Label(new Rect(0,0,Screen.width, Screen.height),"Game Over");
//		}
//
//	}

}
