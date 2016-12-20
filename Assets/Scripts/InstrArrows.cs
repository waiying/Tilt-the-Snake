using UnityEngine;
using System.Collections;

public class InstrArrows : MonoBehaviour {

	public GameObject left, right, leftSel, rightSel;
	public GameObject page0, page1, page2;
	public static int pageNum = 0;
	public AudioClip buttonSound;

	// Use this for initialization
	void Start () {

		pageNum = 0;

		left.GetComponent<SpriteRenderer> ().enabled = false;
		leftSel.GetComponent<SpriteRenderer> ().enabled = false;
		right.GetComponent<SpriteRenderer> ().enabled = true;
		rightSel.GetComponent<SpriteRenderer> ().enabled = false;

		page0.GetComponent<SpriteRenderer> ().enabled = true;
		page1.GetComponent<SpriteRenderer> ().enabled = false;
		page2.GetComponent<SpriteRenderer> ().enabled = false;
	}
		
	void OnMouseDown() {

		GetComponent<AudioSource> ().clip = buttonSound;
		GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
		GetComponent<AudioSource> ().Play ();

		if (gameObject.name == "right arrow" && pageNum < 2) {
			pageNum += 1;
			rightSel.GetComponent<SpriteRenderer> ().enabled = true;
		} else if (gameObject.name == "left arrow" && pageNum > 0) {
			pageNum -= 1;
			leftSel.GetComponent<SpriteRenderer> ().enabled = true;
		}
	}

	void OnMouseUp() {
		rightSel.GetComponent<SpriteRenderer> ().enabled = false;
		leftSel.GetComponent<SpriteRenderer> ().enabled = false;

		if (pageNum == 0) {
			page0.GetComponent<SpriteRenderer> ().enabled = true;
			page1.GetComponent<SpriteRenderer> ().enabled = false;
			page2.GetComponent<SpriteRenderer> ().enabled = false;
			left.GetComponent<SpriteRenderer> ().enabled = false;
		} else if (pageNum == 1) {
			page0.GetComponent<SpriteRenderer> ().enabled = false;
			page1.GetComponent<SpriteRenderer> ().enabled = true;
			page2.GetComponent<SpriteRenderer> ().enabled = false;
			left.GetComponent<SpriteRenderer> ().enabled = true;
			right.GetComponent<SpriteRenderer> ().enabled = true;
		} else if (pageNum == 2) {
			page0.GetComponent<SpriteRenderer> ().enabled = false;
			page1.GetComponent<SpriteRenderer> ().enabled = false;
			page2.GetComponent<SpriteRenderer> ().enabled = true;
			right.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
}
