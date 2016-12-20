using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hyperlinks : MonoBehaviour {
	
	public AudioClip buttonSound;

	void OnMouseDown() {
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play ();
		Invoke ("OpenLink", 0.2f);
	}

	void OpenLink() {
		if (GetComponent<Text> ().text == "\"Apple_Crunch_17.wav\"") {
			Application.OpenURL ("https://www.freesound.org/people/Koops/sounds/20280/");
		} else if (GetComponent<Text> ().text == "CC BY 3.0") {
			Application.OpenURL ("https://creativecommons.org/licenses/by/3.0/");
		} else if (GetComponent<Text> ().text == "\"Selection Sounds\"") {
			Application.OpenURL ("https://www.freesound.org/people/johnthewizar/sounds/319419/");
		} else if (GetComponent<Text> ().text == "YouTube Audio Library Sound Effects") {
			Application.OpenURL ("https://www.youtube.com/audiolibrary/soundeffects");
		} else if (GetComponent<Text> ().text == "\"Bassa Island Game Loop\"") {
			Application.OpenURL ("http://incompetech.com/music/royalty-free/index.html?isrc=USUAN1100840");
		} else if (GetComponent<Text> ().text == "\"carbidexplosion.wav\"") {
			Application.OpenURL ("https://www.freesound.org/people/escortmarius/sounds/172870/");
		} else if (GetComponent<Text> ().text == "\"Level Up 03\"") {
			Application.OpenURL ("https://www.freesound.org/people/rhodesmas/sounds/320657/");
		} else if (GetComponent<Text> ().text == "\"Action 02\"") {
			Application.OpenURL ("https://www.freesound.org/people/rhodesmas/sounds/320776/");
		} else if (GetComponent<Text> ().text == "\"jump2.wav\"") {
			Application.OpenURL ("https://www.freesound.org/people/LloydEvans09/sounds/187024/");
		} else if (GetComponent<Text> ().text == "CC0 1.0") {
			Application.OpenURL ("https://creativecommons.org/publicdomain/zero/1.0/");
		} else if (GetComponent<Text> ().text == "\"Level Up 01\"") {
			Application.OpenURL ("http://www.freesound.org/people/rhodesmas/sounds/320655/");
		}
	}
}
