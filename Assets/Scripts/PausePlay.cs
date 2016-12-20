using UnityEngine;
using System.Collections;

public class PausePlay : MonoBehaviour {
	public GameObject pause, play;
	public bool flag = false;
	bool playing = true;
	public AudioClip buttonSound;

	void Start() {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		pause.GetComponent<SpriteRenderer> ().enabled = true;
		play.GetComponent<SpriteRenderer> ().enabled = false;
	}

	void OnMouseDown() {
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play ();
		if (playing) {
			Screen.sleepTimeout = SleepTimeout.SystemSetting;
			pause.GetComponent<SpriteRenderer> ().enabled = false;
			play.GetComponent<SpriteRenderer> ().enabled = true;
			Time.timeScale = 0;
			playing = false;
		} else {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			play.GetComponent<SpriteRenderer> ().enabled = false;
			pause.GetComponent<SpriteRenderer> ().enabled = true;
			Time.timeScale = 1;
			playing = true;
		}
	}
}
