using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour {

	public GameObject tailPrefab;

	List<Transform> tail = new List<Transform>();

	Vector2 dir = Vector2.right;

	bool ate, dead = false;

	void Start () {
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
		if (dead) {
			CancelInvoke();
		}

		Vector2 pos = transform.position;

		if (ate) {
			GameObject g =(GameObject)Instantiate(tailPrefab,
				pos,
				Quaternion.identity);

			tail.Insert(tail.Count, g.transform);
			ate = false;
		}

		foreach (Transform item in tail) {
			item.Translate (dir);
		}

		transform.Translate(dir);
	}

	void FullyContains2D (Collider2D coll) {
		Vector2 pos1 = coll.gameObject.transform.position;
		Vector2 pos2 = transform.position;
		// Food?
		if (coll.name.StartsWith("FoodPrefab") && pos1.x == pos2.x && pos1.y == pos2.y) {
			// Get longer in next Move call
			ate = true;
			Debug.Log (coll.name);

			// Remove the Food
//			Destroy(coll.gameObject);
		}
		// Collided with Tail or Border
		else {
			// ToDo 'You lose' screen
//			dead = true;
			Debug.Log (coll.name);
		}
	}


//	void OnGUI() {
//		
//		if(dead) {
//			GUI.Label(new Rect(0,0,Screen.width, Screen.height),"Game Over");
//		}
//
//	}

}
