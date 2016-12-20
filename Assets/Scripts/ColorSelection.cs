using UnityEngine;
using System.Collections;

public class ColorSelection : MonoBehaviour {

	public static bool green, blue, pink, yellow, purple, red, black, white;
	public GameObject notification;
	public GameObject selection;
	public AudioClip buttonSound;

	void Start() {
		green = true; blue = false; pink = false; yellow = false; purple = false; red = false; black = false; white = false;
		notification.GetComponent<MeshRenderer> ().enabled = false;
		if (PlayerPrefs.HasKey ("Shared") && gameObject.tag == "Locked") {
			Destroy (this.gameObject);
		}
	}

	void OnMouseDown(){

		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play ();
		selection.transform.position = gameObject.transform.position;
		green = false; blue = false; pink = false; yellow = false; purple = false; red = false; black = false; white = false;

		if (gameObject.tag == "Green" || gameObject.tag == "Locked") {
			green = true;
			if (gameObject.tag == "Locked") {
				notification.GetComponent<MeshRenderer> ().enabled = true;
			} else notification.GetComponent<MeshRenderer> ().enabled = false;
		} else if (gameObject.tag == "Blue") {
			blue = true;
		} else if (gameObject.tag == "Pink") {
			pink = true;
		} else if (gameObject.tag == "Yellow") {
			yellow = true;
		} else if (gameObject.tag == "Purple") {
			purple = true;
		} else if (gameObject.tag == "Red") {
			red = true;
		} else if (gameObject.tag == "Black") {
			black = true;
		} else if (gameObject.tag == "White") {
			white = true;
		}

	}
}
