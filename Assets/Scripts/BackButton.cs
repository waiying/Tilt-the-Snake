using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

	public static bool notInstantiated;
	public GameObject selection;
	public GameObject quitWarning;
	public AudioClip buttonSound;

	void Start() {
		notInstantiated = true;
		selection.GetComponent<SpriteRenderer> ().enabled = false;
	}

	void Update() {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				if (transform.tag == "Back1") {
					Application.LoadLevel (0);
				} else if (notInstantiated && !SnakeMovement.gameOver) {
					Instantiate (quitWarning, new Vector3 (0, 0, 0), Quaternion.identity);
					//quitWarning.Find("Canvas").GetComponent<Canvas> ().worldCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
					Time.timeScale = 0;
					notInstantiated = false;
				}
			}
		}
	}

	void OnMouseDown() {
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play();
		selection.GetComponent<SpriteRenderer> ().enabled = true;

		if (transform.tag == "Back1") {
			Invoke("LoadLevel", 0.1f);
		} else if (notInstantiated && !SnakeMovement.gameOver) {
			Instantiate (quitWarning, new Vector3 (0, 0, 0), Quaternion.identity);
			//quitWarning.Find("Canvas").GetComponent<Canvas> ().worldCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
			Time.timeScale = 0;
			notInstantiated = false;
		}
	}

	void OnMouseUp() {
		selection.GetComponent<SpriteRenderer> ().enabled = false;
	}

	void LoadLevel() {
		if (gameObject.name == "Back Button Credits") {
			Application.LoadLevel (5);
		} else Application.LoadLevel (0);
	}
}
