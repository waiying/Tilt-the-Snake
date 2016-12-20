using UnityEngine;
using System.Collections;

public class Mute : MonoBehaviour {

	public GameObject cross;

	void Start() {
		if (PlayerPrefs.GetInt ("muted", 0) == 1) {
			cross.GetComponent<SpriteRenderer> ().enabled = true;
			AudioListener.pause = true;
		} else {
			cross.GetComponent<SpriteRenderer> ().enabled = false;
			AudioListener.pause = false;
		}
	}

	void OnMouseDown() {
		if (PlayerPrefs.GetInt ("muted", 0) == 1) {
			cross.GetComponent<SpriteRenderer> ().enabled = false;
			AudioListener.pause = false;
			PlayerPrefs.SetInt ("muted", 0);
		} else {
			cross.GetComponent<SpriteRenderer> ().enabled = true;
			AudioListener.pause = true;
			PlayerPrefs.SetInt ("muted", 1);
		}
	}
}
