using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

	public static bool playing = false;

	// Use this for initialization
	void Start () {
		if (playing) {
			Destroy (GameObject.Find("Bassa_Island_Game_Loop_Latinesque (1)"));
		} else {
			playing = true;
			this.name = "Original Music";
			DontDestroyOnLoad (this.gameObject);
		}
	}

	void Update () {
		this.GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("BackgroundVolume", 0.5f);
	}
}
