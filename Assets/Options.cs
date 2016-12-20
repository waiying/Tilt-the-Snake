using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour {

	public static bool deleted = false;
	public GameObject selection, popUp, reset, credits;
	public AudioClip buttonSound;

	void Start () {
		selection.GetComponent<SpriteRenderer> ().enabled = false;
		GameObject.Find ("Background Music").GetComponent<Scrollbar> ().value = PlayerPrefs.GetFloat ("BackgroundVolume", 0.5f);
		GameObject.Find("Sound Effects").GetComponent<Scrollbar>().value = PlayerPrefs.GetFloat ("SoundEffects", 1);
	}
 
	public void ChangeBackgroundMusic () {
		PlayerPrefs.SetFloat("BackgroundVolume", GameObject.Find("Background Music").GetComponent<Scrollbar> ().value);
	}

	public void ChangeSoundVolume() {
		PlayerPrefs.SetFloat ("SoundEffects", GameObject.Find ("Sound Effects").GetComponent<Scrollbar> ().value);
	}

	public void ResetScores() {
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play ();
		selection.transform.position = reset.transform.position;
		selection.GetComponent<SpriteRenderer> ().enabled = true;
		if (!deleted) { 
			PlayerPrefs.DeleteKey ("Scores");
			for (int i = 0; i < 8; i++) {
				PlayerPrefs.DeleteKey ("Score" + (i + 1).ToString());
				PlayerPrefs.DeleteKey ("Name" + (i + 1).ToString());
			}
			deleted = true;
		}
		Invoke ("Selection", 0.1f);
		Instantiate (popUp, new Vector3 (0, 0, 0), Quaternion.identity);
	}

	public void Credits() {
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play ();
		selection.transform.position = credits.transform.position;
		selection.GetComponent<SpriteRenderer> ().enabled = true;
		Invoke ("LoadCredits", 0.1f);
	}

	void LoadCredits() {
		Application.LoadLevel (6);
	}

	void Selection() {
		selection.GetComponent<SpriteRenderer> ().enabled = false;
	}
}
