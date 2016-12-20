using UnityEngine;
using System.Collections;

public class SeedCount : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<TextMesh> ().text = "x0";
	}
	
	// Update is called once per frame
	void Update () {
		if (SnakeMovement.seedsLeft == 3) {
			this.GetComponent<TextMesh> ().text = "x3";
		} else if (SnakeMovement.seedsLeft == 2) {
			this.GetComponent<TextMesh> ().text = "x2";
		} else if (SnakeMovement.seedsLeft == 1) {
			this.GetComponent<TextMesh> ().text = "x1";
		} else this.GetComponent<TextMesh> ().text = "x0";
	}
}
