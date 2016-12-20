using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class SocialMedia : MonoBehaviour {

	public AudioClip buttonSound;
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

		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = 0.5f;
		GetComponent<AudioSource> ().Play();
		if (Application.loadedLevel == 0) {
			FB.Mobile.ShareDialogMode = ShareDialogMode.AUTOMATIC;
			FB.ShareLink (
				contentURL: new System.Uri ("https://fb.me/482944335246586"),
				contentTitle: "Check out Tilt the Snake for Android!",
				contentDescription: "Tilt the Snake is a snake game where you tilt your android device to control your snake! Try to beat your friends' and your own high scores! Multiplayer coming soon.",
				photoURL: new System.Uri ("https://s16.postimg.org/mt51ydk2t/Snake_Tilt_Icon_144.png"),
				callback: ShareCallback);

		} else {
			FB.Mobile.ShareDialogMode = ShareDialogMode.AUTOMATIC;
			FB.ShareLink (
				contentURL: new System.Uri ("https://fb.me/482944335246586"),
				contentTitle: PlayerPrefs.GetString("Name1") + " got a new high score of " + PlayerPrefs.GetInt("Score1").ToString() + " on Tilt the Snake!",
				contentDescription: "Tilt the Snake is a snake game where you tilt your android device to control your snake! Try to beat your friends' and your own high scores! Multiplayer coming soon.",
				photoURL: new System.Uri ("https://s16.postimg.org/mt51ydk2t/Snake_Tilt_Icon_144.png"),
				callback: ShareCallback);
		}
	}

	public void ShareToTwitter ()
	{
		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = 0.5f;
		GetComponent<AudioSource> ().Play();

		PlayerPrefs.SetInt ("Shared", 1);

		string textToDisplay;
		if (Application.loadedLevel == 0) {
			textToDisplay = "'Tilt' your phone to play a new, exciting game of Snake and beat your friends' high scores! Get #TiltTheSnake @ http://tinyurl.com/glutg5m!";
		} else {
			textToDisplay = PlayerPrefs.GetString("Name1") + " got a new high score of " + PlayerPrefs.GetInt("Score1").ToString() + " on #TiltTheSnake! Can you beat " + PlayerPrefs.GetString("Name1") + "'s high score? Get Tilt the Snake @ http://tinyurl.com/glutg5m!";
		}
		Application.OpenURL(TWITTER_ADDRESS +
			"?text=" + WWW.EscapeURL(textToDisplay) +
			"&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE));
	}
}
