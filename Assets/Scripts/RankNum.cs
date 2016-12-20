using UnityEngine;
using System.Collections;

public class RankNum : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Score.rank != 10) {
			GetComponent<TextMesh> ().text = "GAME OVER\n" + "Your rank is #" + Score.rank.ToString () + "!";
		} else {
			GetComponent<TextMesh> ().text = "GAME OVER\n" + "Ranked below #8";
		}
	}
}
