using UnityEngine;
using System.Collections;

public class ScrollbarClick : MonoBehaviour {

	public AudioClip buttonSound;

	void OnMouseDown() {
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play ();
	}
}
