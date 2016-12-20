using UnityEngine;
using System.Collections;
using Facebook.Unity;

public class RestartQuit : MonoBehaviour {

	public AudioClip buttonSound;

	void Awake() {
		GetComponent<Canvas> ().worldCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
	}

	public void Restart() {
		if (!RateUs.buttonDisabled) {
			PlaySound ();
			Invoke ("LoadLevel2", 0.1f);
		}
	}

	public void Quit() {
		if (!RateUs.buttonDisabled) {
			PlaySound ();
			Invoke ("LoadLevel0", 0.1f);
		}
	}

	void PlaySound() {
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play ();
	}

	void LoadLevel0 () {
		Application.LoadLevel (0);
	}

	void LoadLevel2 () {
		Application.LoadLevel (2);
	}

	private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
	private const string TWEET_LANGUAGE = "en"; 

	private void ShareCallback (IShareResult result) {
		if (result.Cancelled || !string.IsNullOrEmpty(result.Error)) {
			Debug.Log("ShareLink Error: "+result.Error);
		} else if (!string.IsNullOrEmpty(result.PostId)) {
			// Print post identifier of the shared content
			Debug.Log(result.PostId);
		} else {
			// Share succeeded without postID
			Debug.Log("ShareLink success!");
			PlayerPrefs.SetInt ("Shared", 1); //Shared = 1 means true
		}
	}
		
	public void ShareToFB() {

		PlaySound ();
		
		if (SnakeMovement.score > PlayerPrefs.GetInt ("Score1", 0)) {
			FB.Mobile.ShareDialogMode = ShareDialogMode.AUTOMATIC;
			FB.ShareLink (
				contentURL: new System.Uri ("https://fb.me/482944335246586"),
				contentTitle: GetName.userName + " just got a new high score of " + SnakeMovement.score.ToString () + " on Tilt the Snake! Can you beat " + GetName.userName + "'s high score?",
				contentDescription: "Tilt the Snake is a new snake game where you tilt your android device to control your snake! Try to beat your friends' and your own high scores! Multiplayer coming soon.",
				photoURL: new System.Uri ("https://s16.postimg.org/mt51ydk2t/Snake_Tilt_Icon_144.png"),
				callback: ShareCallback);
		} else {
			FB.Mobile.ShareDialogMode = ShareDialogMode.AUTOMATIC;
			FB.ShareLink (
				contentURL: new System.Uri ("https://fb.me/482944335246586"),
				contentTitle: GetName.userName + " just scored " + SnakeMovement.score.ToString () + " on Tilt the Snake! Can you beat " + GetName.userName + "'s score?",
				contentDescription: "Tilt the Snake is a new snake game where you tilt your android device to control your snake! Try to beat your friends' and your own high scores! Multiplayer coming soon.",
				photoURL: new System.Uri ("https://s16.postimg.org/mt51ydk2t/Snake_Tilt_Icon_144.png"),
				callback: ShareCallback);
		}
	}
		
	public void ShareToTwitter ()
	{
		PlaySound ();

		PlayerPrefs.SetInt ("Shared", 1); //Shared = 1 means true

		string textToDisplay;
		if (SnakeMovement.score > PlayerPrefs.GetInt ("Score1", 0)) {
			textToDisplay = GetName.userName + " just got a new high score of " + SnakeMovement.score.ToString () + " on #TiltTheSnake! Can you beat " + GetName.userName + "'s high score? Get Tilt the Snake @ http://tinyurl.com/glutg5m!";
		} else {
			textToDisplay = GetName.userName + " just scored " + SnakeMovement.score.ToString () + " on #TiltTheSnake! Can you beat " + GetName.userName + "'s score? Get Snake Tilt @ http://tinyurl.com/glutg5m!";
		}
		Application.OpenURL(TWITTER_ADDRESS +
			"?text=" + WWW.EscapeURL(textToDisplay) +
			"&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE));
	}
}
