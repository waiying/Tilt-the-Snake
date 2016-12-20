using UnityEngine;
using System.Collections;

public class OkButton : MonoBehaviour {

	public AudioClip buttonSound;
	public GameObject popUp;

	void OnMouseDown() {
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play ();
		transform.GetComponent<TextMesh> ().color = new Color (0.13f, 0.67f, 0.07f, 1); //change to custom green
		Invoke ("Destroy", 0.2f);
	}

	void Destroy() {
		Destroy (popUp);
	}
}
