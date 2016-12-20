using UnityEngine;
using System.Collections;

public class Fade321go : MonoBehaviour {

	public GameObject fade;
	public static bool canStart;
	// Use this for initialization
	void Start () {
		canStart = false;
		fade.GetComponent<Animator> ().enabled = false;
		Invoke ("PlayFade", 3);
	}
	
	// Update is called once per frame
	void PlayFade () {
		fade.GetComponent<Animator> ().enabled = true;
		Invoke ("Go", 1);
	}

	void Go() {
		canStart = true;
	}
}
