using UnityEngine;
using System.Collections;

public class SeedHit : MonoBehaviour {

	public GameObject explosion;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.transform.tag == "Bomb") {
			SnakeMovement.score += 5;
			GameObject explosionAnimation;

			GetComponent<AudioSource> ().clip = Resources.Load("explosion") as AudioClip;
			GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
			GetComponent<AudioSource> ().Play ();

			explosionAnimation = Instantiate (explosion, other.transform.position, Quaternion.identity) as GameObject;
			Destroy (explosionAnimation, 2);
			this.GetComponent<SpriteRenderer> ().enabled = false;

			Destroy (other.gameObject);
			this.GetComponent<BoxCollider2D> ().enabled = false;
			Destroy (this.gameObject, 2);
		}
	}
}
