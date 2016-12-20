using UnityEngine;
using System.Collections;
using Facebook.Unity;

public class MenuButtons : MonoBehaviour {

	public AudioClip buttonSound;

	void Awake ()
	{
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}

		PlayerPrefs.SetInt ("Rate later", 0);
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	void Update() {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				Application.Quit ();
			}
		}
	}
				
	void OnMouseDown(){
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play();
		transform.GetComponent<TextMesh> ().color = new Color (0.13f, 0.67f, 0.07f, 1); //change to custom green
		Invoke ("LoadScene", 0.1f);
	}

	void LoadScene() {
		if (transform.tag == "Single") {
			Application.LoadLevel (1);
		}

		if (transform.tag == "High Scores") {
			Application.LoadLevel (3);
		}

		if (transform.tag == "Instructions") {
			Application.LoadLevel (4);
		}

		if (transform.tag == "Options") {
			Application.LoadLevel (5);
		}
	}
}
