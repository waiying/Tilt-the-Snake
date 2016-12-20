using UnityEngine;
using System.Collections;

public class SeedProjectile : MonoBehaviour {

	GameObject seed;

	void Start() {
		seed = Resources.Load ("Seed Bullet") as GameObject;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && SnakeMovement.seedsLeft <= 3 && SnakeMovement.seedsLeft > 0) {
			GameObject projectile;
			projectile = Instantiate (seed, transform.position, transform.rotation) as GameObject;
			Rigidbody2D rb = projectile.GetComponent<Rigidbody2D> ();
			rb.velocity = transform.up * 15;
			SnakeMovement.seedsLeft -= 1;
			if (SnakeMovement.seedsLeft == 0) {
				SnakeMovement.prevScore = SnakeMovement.score;
			}
		}
	}
}
