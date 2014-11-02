using UnityEngine;
using System.Collections;

public class ClickAndMove : MonoBehaviour {

	int numberOfClicks;
	int amountToMove;

	// Use this for initialization
	void Start () {
		numberOfClicks = 0;
	}

	void OnMouseDown() {
		numberOfClicks++;
		Debug.Log ("Clicked on box. Count is: " + numberOfClicks);
		if (numberOfClicks % 2 == 0) {
			Debug.Log ("Moving box back.");
			transform.Translate(-1,-1,1);
		} else {
			Debug.Log("Moving box forward.");
			transform.Translate(1,1,1);
		}
	}

	// Update is called once per frame
	void Update () {
	}
}
