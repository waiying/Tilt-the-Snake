using UnityEngine;
using System.Collections;

public class RateUs : MonoBehaviour {
	public AudioClip button;
	public static bool buttonDisabled;

	void Start() {
		buttonDisabled = true;
	}

	void OnMouseDown() {
		transform.GetComponent<TextMesh> ().color = new Color (0.05f, 0.33f, 0f, 1);
		GetComponent<AudioSource> ().clip = button;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play();
		if (gameObject.name == "Sure") {
			PlayerPrefs.SetInt ("Rated", 1); //1 means true
			Invoke ("LoadRating", 0.2f);
			Invoke ("DestroyObj", 0.2f);
		} else if (gameObject.name == "Rate Later") {
			PlayerPrefs.SetInt ("Rate later", 1);
			Invoke ("DestroyObj", 0.2f);
		} else if (gameObject.name == "No") {
			PlayerPrefs.SetInt ("Rated", 1);
			Invoke ("DestroyObj", 0.2f);
		}
	}

	void DestroyObj() {
		Debug.Log ("destroy");
		buttonDisabled = false;
		Destroy (GameObject.Find ("Rate us(Clone)"));
	}

	void LoadRating() {
		Application.OpenURL ("market://details?id=com.GiraffaGrubs.TiltTheSnake");
	}
}