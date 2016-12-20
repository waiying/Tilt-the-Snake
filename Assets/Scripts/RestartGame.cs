using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {
	public AudioClip buttonSound;
	public GameObject selection;
	public GameObject restartPopUp;
	public static bool notInstantiated;

	void Start() {
		notInstantiated = true;
	}

	void OnMouseDown() {
		if (notInstantiated) {
			notInstantiated = false;
			GetComponent<AudioSource> ().clip = buttonSound;
			GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
			GetComponent<AudioSource> ().Play ();
			selection.GetComponent<SpriteRenderer> ().enabled = true;
			Time.timeScale = 0;

			Instantiate (restartPopUp, new Vector3 (0, 0, 0), Quaternion.identity);
		}
	}

	void OnMouseUp() {
		selection.GetComponent<SpriteRenderer> ().enabled = false;
	}
}
