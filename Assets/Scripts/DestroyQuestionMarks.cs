using UnityEngine;
using System.Collections;

public class DestroyQuestionMarks : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("Shared")) {
			Destroy (this.gameObject);
		}
	}
}
