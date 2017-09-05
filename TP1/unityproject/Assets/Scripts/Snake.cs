using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour {

	public GameObject tailPrefab;
	List<Transform> tail = new List<Transform>();
	Vector2 dir = Vector2.right;

	bool ate;

	void Start () {
		ate = false;
		InvokeRepeating("Move", 0.3f, 0.1f);
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
			GameObject g =(GameObject)Instantiate(tailPrefab,
				v,
				Quaternion.identity);
			tail.Insert(0, g.transform);
			ate = false;
		} else if (tail.Count > 0) {
			tail.Last().position = v;
			tail.Insert(0, tail.Last());
			tail.RemoveAt(tail.Count-1);
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.name.StartsWith("FoodPrefab")) {
			ate = true;
			SnakeManager.instance.EatFood (coll.gameObject);
		} else {
			SnakeManager.instance.Crush ();
		}
	}

	public void ResetSnake () {
		foreach (Transform t in tail) {
			Destroy (t.gameObject);
		}
		tail = new List<Transform> ();
		dir = Vector2.right;
		InvokeRepeating("Move", 0.3f, 0.1f);
	}
}
