using UnityEngine;
using System.Collections;

public class YesNoQuit : MonoBehaviour {

	public GameObject warning;
	public AudioClip buttonSound;

	void Awake() {
		GetComponent<Canvas> ().worldCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
	}

	void Start() {
		//GetComponent<Canvas> ().worldCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		if (!RestartGame.notInstantiated) {
			warning = GameObject.Find ("Restart Popup(Clone)");
		} else
			warning = GameObject.Find ("Quit Warning(Clone)");
	}

	public void Yes() {
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play();
		Time.timeScale = 1;
		Invoke ("LoadLevel", 0.1f);
	}

	public void No() {
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play();
		BackButton.notInstantiated = true;
		RestartGame.notInstantiated = true;
		Time.timeScale = 1;
		Destroy (warning, 0.15f);
	}

	void LoadLevel() {
		if (!RestartGame.notInstantiated) {
			Application.LoadLevel (2);
		} else
			Application.LoadLevel (0);
	}
}
