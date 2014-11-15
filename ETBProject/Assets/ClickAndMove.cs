using UnityEngine;
using System.Collections;

public class ClickAndMove : MonoBehaviour {

	private int numberOfClicks;
	private int amountToMove;
	private bool grounded = false;

	// Use this for initialization
	void Start () {
		numberOfClicks = 0;
	}

	void OnMouseDown() {
		numberOfClicks++;
		Debug.Log ("Clicked on box. Count is: " + numberOfClicks);
		if(grounded) {
			grounded = false;
			if (numberOfClicks % 2 == 0) {
				Debug.Log ("Box hop.");
				this.rigidbody.AddForce(Vector3.up * 120);
			} else {
				Debug.Log("Box leap.");
				this.rigidbody.AddForce(Vector3.up * 250);
			}
		}
	}

	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision collision){
		if(!grounded) {
			grounded = true;
			Debug.Log("grounded");
		}
	}

}
