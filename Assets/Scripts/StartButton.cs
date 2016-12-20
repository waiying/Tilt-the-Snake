using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	public GameObject selection, warning;
	public AudioClip buttonSound;

	void Start() {
		selection.GetComponent<SpriteRenderer> ().enabled = false;
	}

	void OnMouseDown() {
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play();
		selection.GetComponent<SpriteRenderer> ().enabled = true;
		if (GetName.userName != "") {
			Invoke ("LoadLevel", 0.1f);
		} else
			Instantiate (warning, new Vector3 (-1.88f, 1.0383f, 0), Quaternion.identity);
	}

	void OnMouseUp() {
		selection.GetComponent<SpriteRenderer> ().enabled = false;
	}

	void LoadLevel() {
		Application.LoadLevel (2);
	}
}
