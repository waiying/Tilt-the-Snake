using UnityEngine;
using System.Collections;

public class ShowHighScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<TextMesh> ().text = SnakeMovement.score.ToString();
	}
}
