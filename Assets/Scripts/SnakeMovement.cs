using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class SnakeMovement : MonoBehaviour {

	public GameObject tongue;
	public List<Transform> bodyParts = new List<Transform> (); 
	public Rigidbody2D rb;
	public float speed = 5;
	public Vector2 movement = Vector2.up;
	Vector2 prevDir = Vector2.up;
	Vector2 beforeTurnDir;
	float currRotation;
	public bool isMoving = false;
	public bool loseLife = false;
	bool repeat = false, mushroomSpawned = false, healthSpawned = true;
	int lifeCount = 3;
	int currHighScore;
	public static bool gameOver = false;
	public Vector3 turnedPos = Vector3.zero;
	public bool turned = false;
	public Transform bodyObject;
	public GameObject apple, bomb, explosion, poof, light2, snail, watermelon, loseHeart, 
			heart1, heart2, heart3, newHighScore, gameOVA, rateUs, mushroom, health; //prefabs
	public GameObject normal, swift, fast, insane, maxSpeed; //speed text prefabs
	public AudioClip appleCrunch, wallHit, congrats, explode, lightning, eatSnail, eatHealth; 
	public bool[,] isOccupied = new bool[11, 20];
	public bool lightTriggered = false;
	public static bool bodyHit = false, bombHit = false, borderHit = false;
	bool instantiateSigns = false;
	public static bool noPenalityEnabled = false, melonSpawned, ateMushroom;
	public static int score, seedsLeft, prevScore, prevScore1, prevCount;

	void Start(){
		ateMushroom = false;
		if (ColorSelection.green) {
			poof = Resources.Load ("Poofs/Poof (green)") as GameObject;
		} else if (ColorSelection.purple) {
			poof = Resources.Load ("Poofs/Poof (purple)") as GameObject;
		} else if (ColorSelection.pink) {
			poof = Resources.Load ("Poofs/Poof (pink)") as GameObject;
		} else if (ColorSelection.blue) {
			poof = Resources.Load ("Poofs/Poof (blue)") as GameObject;
		} else if (ColorSelection.black) {
			poof = Resources.Load ("Poofs/Poof (black)") as GameObject;
		} else if (ColorSelection.red) {
			poof = Resources.Load ("Poofs/Poof (red)") as GameObject;
		} else if (ColorSelection.yellow) {
			poof = Resources.Load ("Poofs/Poof (yellow)") as GameObject;
		} else if (ColorSelection.white) {
			poof = Resources.Load ("Poofs/Poof (white)") as GameObject;
		}

		GetComponent<Rigidbody2D> ().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		prevScore = 0;
		prevScore1 = 0;
		bodyHit = false;
		bombHit = false;
		borderHit = false;
		seedsLeft = 0;
		melonSpawned = false;
		AdManager.RequestInterstitial (); //start loading ad
		currHighScore = PlayerPrefs.GetInt ("Score1", 0);
		gameOver = false;
		score = 0;
		heart1 = GameObject.Find ("Heart");
		heart2 = GameObject.Find ("Heart 1");
		heart3 = GameObject.Find ("Heart 2");
		for (int i = 0; i < 11; ++i) {
			for (int j = 0; j < 20; ++j) {
				isOccupied [i, j] = false;
			}
		}
		SpawnApple (4);
		InvokeRepeating ("SpawnBomb", 10, 15);

	}

	void LoadRateUs() {
		Instantiate (rateUs, new Vector3 (0, 0, 0), Quaternion.identity);
	}

	void Update(){
		if (gameOver && !instantiateSigns) {
			if (score > currHighScore) {
				GetComponent<AudioSource> ().clip = congrats;
				GetComponent<AudioSource> ().volume = 0.5f;
				GetComponent<AudioSource> ().Play();
				Instantiate(newHighScore, new Vector3 (0, 0, 0), Quaternion.identity);
			} else Instantiate(gameOVA, new Vector3 (0, 0, 0), Quaternion.identity);
			instantiateSigns = true;

			if (PlayerPrefs.GetInt ("Rated", 0) == 0 && Time.time > 240 && PlayerPrefs.GetInt("Rate later", 0) == 0) {
				Debug.Log ("Yes");
				Invoke ("LoadRateUs", 0.8f);
			} else {
				Invoke ("ShowAd", 1); //Display interstitial ad when game over
			}

			Debug.Log (Time.time);
		}

		if (lifeCount < 3 && score > prevScore1 + 19 && score > 99 && !healthSpawned) {
			prevScore1 = score;
			float k = Random.value;
			if (k < 0.5f) {
				healthSpawned = true;
				SpawnHealth ();
			}
		}

		if (score > 59 && score > prevScore + 14 && seedsLeft == 0 && !melonSpawned) {
			prevScore = score;
			float i = Random.value;
			Debug.Log ("melon random value = " + i.ToString ());
			if (i < 0.6f) {
				//Debug.Log ("Melon spawn");
				melonSpawned = true;
				SpawnMelon ();
			}
		}

		//Debug.Log ("count % 15 = " + (bodyParts.Count % 15).ToString());
		if (bodyParts.Count > 24 && bodyParts.Count > 5 + prevCount && !mushroomSpawned) {
			prevCount = bodyParts.Count;
			float j = Random.value;
			Debug.Log ("shroom random value = " + j.ToString ());
			if (j < 0.6f) {
				prevCount = bodyParts.Count - 10;
				//Debug.Log ("random value = " + i.ToString ());
				mushroomSpawned = true;
				SpawnMushroom ();
			}
		}

		if (speed > 3 && lightTriggered) {
			Invoke ("SpawnSnail", 25); 
			lightTriggered = false;
		} else if (speed == 3 && IsInvoking("SpawnSnail")) {
			CancelInvoke ("SpawnSnail");
		}

		if (speed == 15.1875 || gameOver) {
			CancelInvoke ("SpawnLightning");
		} else if (!IsInvoking ("SpawnLightning") && !gameOver) {
			InvokeRepeating ("SpawnLightning", 20, 30);
		}

		if (loseLife && repeat) {
			(Instantiate (loseHeart, this.transform.position, Quaternion.identity) as GameObject).transform.parent = this.transform;
			lifeCount -= 1;
			healthSpawned = false;

			if (lifeCount == 2){
				heart3.GetComponent<SpriteRenderer>().enabled = false;
				repeat = false;
				Invoke("moveSnake", 2);
			}
			else if (lifeCount == 1) {
				heart2.GetComponent<SpriteRenderer>().enabled = false;
				repeat = false;
				Invoke("moveSnake", 2);
			}
			else if (lifeCount == 0) {
				heart1.GetComponent<SpriteRenderer>().enabled = false;
				gameOver = true;
				repeat = false;
				CancelInvoke("SpawnBomb");
				CancelInvoke("SpawnLightning");
				CancelInvoke("SpawnSnail");
			}
		}
	}

	void ShowAd() {
		AdManager.ShowInterstitial ();
	}

	void FixedUpdate(){
		if ((Mathf.Abs (Input.acceleration.x) > 0.4f || Mathf.Abs (Input.acceleration.y) > 0.4f) && !loseLife && !ateMushroom) {
			//moving in only one axis (x or y) at a time!!!
			if (Mathf.Abs (Input.acceleration.x) > Mathf.Abs (Input.acceleration.y)) {
				if (((prevDir.x > 0 && Input.acceleration.x > 0) || (prevDir.x < 0 && Input.acceleration.x < 0) || prevDir.x == 0)
				    && Mathf.Abs (turnedPos.y - transform.position.y) > 1){
					if (Input.acceleration.x > 0 && (transform.position.y < 10.48f && transform.position.y > -9.7f)){
						movement = new Vector2 (1,0);
						Rotate ();
					} else if (Input.acceleration.x < 0 && (transform.position.y < 10.48f && transform.position.y > -9.7f)) {
						movement = new Vector2 (-1,0);
						Rotate ();
					}
					//isMoving = true;
					//Rotate();
				}
			} else if (Mathf.Abs (Input.acceleration.x) < Mathf.Abs (Input.acceleration.y)) {
				if (((prevDir.y > 0 && Input.acceleration.y > 0) || (prevDir.y < 0 && Input.acceleration.y < 0) || prevDir.y == 0)
				    && Mathf.Abs (turnedPos.x - transform.position.x) > 1){
					if (Input.acceleration.y > 0 && (transform.position.x > -5.2f && transform.position.x < 5.34f)){
						movement = new Vector2 (0,1);
						Rotate ();
					} else if (Input.acceleration.y < 0 && (transform.position.x > -5.2f && transform.position.x < 5.34f)) {
						movement = new Vector2 (0,-1);
						Rotate ();
					}
					//isMoving = true;
					//Rotate ();
				}
			}
		}

		if (movement != Vector2.zero && !loseLife && Fade321go.canStart && !ateMushroom){
			isMoving = true;
			rb.velocity = movement * speed;
			prevDir = movement;
			turned = false;
			//playerPos = gameObject.transform.position;
			//gameObject.transform.position = new Vector2 (Mathf.Clamp (playerPos.x, -5.36f, 5.36f), Mathf.Clamp (playerPos.y, -9.5f, 10.87f));
		}
	}

	void Rotate(){
		if (movement.x > 0 && prevDir.y > 0) {
			beforeTurnDir = prevDir;
			turned = true;
			currRotation -= 90;
			turnedPos = transform.position;
			transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, currRotation);
		} else if (movement.x < 0 && prevDir.y > 0) {
			beforeTurnDir = prevDir;
			turned = true;
			currRotation += 90;
			turnedPos = transform.position;
			transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, currRotation);
		} else if (movement.x > 0 && prevDir.y < 0) {
			beforeTurnDir = prevDir;
			turned = true;
			currRotation += 90;
			turnedPos = transform.position;
			transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, currRotation);
		} else if (movement.x < 0 && prevDir.y < 0) {
			beforeTurnDir = prevDir;
			turned = true;
			currRotation -= 90;
			turnedPos = transform.position;
			transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, currRotation);
		} else if (movement.y > 0 && prevDir.x > 0) {
			beforeTurnDir = prevDir;
			turned = true;
			currRotation += 90;
			turnedPos = transform.position;
			transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, currRotation);
		} else if (movement.y < 0 && prevDir.x > 0) {
			beforeTurnDir = prevDir;
			turned = true;
			currRotation -= 90;
			turnedPos = transform.position;
			transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, currRotation);
		} else if (movement.y > 0 && prevDir.x < 0) {
			beforeTurnDir = prevDir;
			turned = true;
			currRotation -= 90;
			turnedPos = transform.position;
			transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, currRotation);
		} else if (movement.y < 0 && prevDir.x < 0) {
			beforeTurnDir = prevDir;
			turned = true;
			currRotation += 90;
			turnedPos = transform.position;
			transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, currRotation);
		}
	}


	void moveSnake () {
		Debug.Log ("moveSnake()");
		if (bodyHit) {

			if (!noPenalityEnabled) {
				if (beforeTurnDir == Vector2.up || beforeTurnDir == -Vector2.up) {
					if (movement == Vector2.right) {
						gameObject.transform.position = new Vector2 (gameObject.transform.position.x - 0.3f, gameObject.transform.position.y);
					} else if (movement == -Vector2.right) {
						gameObject.transform.position = new Vector2 (gameObject.transform.position.x + 0.3f, gameObject.transform.position.y);
					}

					prevDir = movement;
					if (beforeTurnDir == Vector2.up) {
						movement = Vector2.up;
					} else
						movement = -Vector2.up;
				} else if (beforeTurnDir == Vector2.right || beforeTurnDir == -Vector2.right) {
					if (movement == Vector2.up) {
						gameObject.transform.position = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y - 0.3f);
					} else if (movement == -Vector2.up) {
						gameObject.transform.position = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 0.3f);
					}

					prevDir = movement;
					if (beforeTurnDir == Vector2.right) {
						movement = Vector2.right;
					} else
						movement = -Vector2.right;
				}
			}

			bodyHit = false;
		} 

		if (borderHit || bombHit) {
		//***********HITTING RIGHT/LEFT BORDERS*****************
			if (movement == Vector2.right || movement == -Vector2.right) { //HITTING RIGHT/LEFT BORDERS
				
				if (movement == Vector2.right && !noPenalityEnabled){
					gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.3f, gameObject.transform.position.y);
				}
				else if (movement == -Vector2.right && !noPenalityEnabled) {
					gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y);
				}
					
				if (transform.position.y < -7f){ //HITTING BOTTOM SIDE
					if (bodyParts [2].transform.position.x > 4.7f && !bombHit) { //hitting right border, bottom side, snake moving from top to bottom, close to the right border
						Debug.Log("(close)Bottom right border: " + (bodyParts[2].transform.position.x).ToString());
						prevDir = new Vector2 (0, -1);
						currRotation -= 90;
						movement = new Vector2 (-1, 0);
					} else if (bodyParts [2].transform.position.x < -4.3f && !bombHit) { //hitting left border, bottom side, snaking moving from top to bottom, close to the left border
						Debug.Log("(close)Bottom left border: " + (bodyParts[2].transform.position.x).ToString());
						prevDir = new Vector2 (0, -1);
						currRotation += 90;
						movement = new Vector2 (1, 0);
					} else { //hitting right or left border, bottom side, snake coming straight from left or right
						Debug.Log("(straight) left/right bottom border: " + (bodyParts[2].transform.position.x).ToString());
						prevDir = movement;
						if (bombHit) {
							bombHit = false;
						} else movement = new Vector2 (0, 1); 
					}
				}
				else { //HITTING TOP SIDE or MIDDLE,  (transform.position.y > 10)
					if (bodyParts [2].transform.position.x > 4.3f && !bombHit) { //hitting right border, top side, snake moving from bottom to top, close to the right border
						Debug.Log("(close)top right border: " + (bodyParts[2].transform.position.x).ToString());
						prevDir = new Vector2 (0, 1);
						currRotation += 90;
						movement = new Vector2 (-1, 0);
						//this.GetComponent<Collider2D> ().enabled = false;
						//Invoke ("EnableCollider2D", 1);
					} else if (bodyParts [2].transform.position.x < -4.2f && !bombHit) { //hitting left border, top side, snaking moving from bottom to top, close to the left border
						Debug.Log("(close)top left border: " + (bodyParts[2].transform.position.x).ToString());
						prevDir = new Vector2 (0, 1);
						currRotation -= 90;
						movement = new Vector2 (1, 0);
						//this.GetComponent<Collider2D> ().enabled = false;
						//Invoke ("EnableCollider2D", 1);
					} else { //hitting right or left border, top side, snake coming straight from left or right
						Debug.Log("(straight) left/right top border: " + (bodyParts[2].transform.position.x).ToString());
						prevDir = movement;
						if (bombHit) {
							bombHit = false;
						} else movement = new Vector2 (0, -1); 
						//this.GetComponent<Collider2D> ().enabled = false;
						//Invoke ("EnableCollider2D", 0.7f);
					}
				}

			} 
			
			//***********HITTING TOP/BOTTOM BORDERS*****************
			else if (movement == Vector2.up || movement == -Vector2.up) { //HITTING TOP/BOTTOM BORDERS
				
				if (movement == Vector2.up && !noPenalityEnabled){
					gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f);
				}
				else if (movement == -Vector2.up && !noPenalityEnabled) {
					gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.3f);
				}

				if (transform.position.x > 2.9f && !bombHit) {//HITTING RIGHT SIDE
					Debug.Log(transform.position.x);
					if (bodyParts [2].transform.position.y > 9.9f) { //hitting top border, right side, snake moving from left to right, close to the top border
						Debug.Log("(close) top right border: " + (bodyParts[2].transform.position.y).ToString());
						prevDir = new Vector2 (1, 0);
						currRotation -= 90;
						movement = new Vector2 (0, -1);
						//this.GetComponent<Collider2D> ().enabled = false;
						//Invoke ("EnableCollider2D", 0.7f);
					} else if (bodyParts [1].transform.position.y < -9.1f && !bombHit) { //hitting bottom border, right side, snake moving from left to right close to the top border
						Debug.Log("(close) bottom right border: " + (bodyParts[2].transform.position.y).ToString());
						prevDir = new Vector2 (1, 0);
						currRotation += 90;
						movement = new Vector2 (0, 1);
						//this.GetComponent<Collider2D> ().enabled = false;
						//Invoke ("EnableCollider2D", 0.7f);
					} else {  //hitting top or bottom border, right side, snake coming straight from top or bottom
						Debug.Log("(straight) top/bottom right border: " + (bodyParts[2].transform.position.y).ToString());
						prevDir = movement;
						if (bombHit) {
							bombHit = false;
						} else movement = new Vector2 (-1, 0); 
						//this.GetComponent<Collider2D> ().enabled = false;
						//Invoke ("EnableCollider2D", 0.7f);
					}
				}
				else {
					if (bodyParts [2].transform.position.y > 9.9f && !bombHit) { //hitting top border, left side, snake moving from right to left, close to the top border
						Debug.Log("(close) top left/middle border: " + (bodyParts[2].transform.position.y).ToString());
						prevDir = new Vector2 (-1, 0);
						currRotation += 90;
						movement = new Vector2 (0, -1);
						//this.GetComponent<Collider2D> ().enabled = false;
						//Invoke ("EnableCollider2D", 0.7f);
					} else if (bodyParts [2].transform.position.y < -8.9f && !bombHit) { //hitting bottom border, left side, snake moving from right to left, close to the top border
						Debug.Log("(close) bottom left/middle border: " + (bodyParts[2].transform.position.y).ToString());
						prevDir = new Vector2 (-1, 0);
						currRotation -= 90;
						movement = new Vector2 (0, 1);
						//this.GetComponent<Collider2D> ().enabled = false;
						//Invoke ("EnableCollider2D", 0.7f);
					} else {  //hitting top or bottom border, left side, snake coming straight from top or bottom (-8.49)
						Debug.Log("(straight) top/bottom left border: " + (bodyParts[2].transform.position.y).ToString());
						prevDir = movement;
						if (bombHit) {
							bombHit = false;
						} else movement = new Vector2 (1, 0); 
						//this.GetComponent<Collider2D> ().enabled = false;
						//Invoke ("EnableCollider2D", 0.7f);
					}
				}

			}
		}

		//this.GetComponent<Collider2D> ().enabled = false;
		//Invoke ("EnableCollider2D", 0.7f);
		if (!noPenalityEnabled) {
			noPenalityEnabled = true;
			Invoke ("NoPenalityDisabled", 2f);
			GetComponent<Animation> ().Play ();
			Rotate ();
			rb.velocity = movement * speed;
			tongue.GetComponent<Animator> ().enabled = true;
			loseLife = false;
			borderHit = false;
		} else {
			Rotate ();
			borderHit = false;
		}
	}

	void NoPenalityDisabled() {
		Debug.Log ("One sec passed.");
		noPenalityEnabled = false;
	}

	void EnableCollider2D() {
		this.GetComponent<Collider2D> ().enabled = true;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.transform.tag == "Apple") {
			GetComponent<AudioSource> ().clip = appleCrunch;
			GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
			GetComponent<AudioSource> ().Play ();
			other.GetComponent<Animation> ().Play ();
			other.GetComponent<BoxCollider2D> ().enabled = false;
			if (speed == 3) {
				score += 1;
			} else if (speed == 4.5f) {
				score += 2;
			} else if (speed == 6.75f) {
				score += 4;
			} else if (speed == 10.125f) {
				score += 6;
			} else if (speed == 15.1875f) {
				score += 10;
			}
			isOccupied [(int)other.gameObject.transform.position.x + 5, (int)other.gameObject.transform.position.y + 9] = false;
			Destroy (other.gameObject,1);
			Transform newSegment = Instantiate (bodyObject, bodyParts [bodyParts.Count - 1].position, Quaternion.identity) as Transform;
			newSegment.gameObject.GetComponent<SpriteRenderer> ().sortingOrder = bodyParts.Count + 2;
			bodyParts.Add (newSegment);
			SpawnApple (1); 
		}
		if (other.transform.tag == "Health") {
			isOccupied [(int)other.gameObject.transform.position.x + 5, (int)other.gameObject.transform.position.y + 9] = false;
			GetComponent<AudioSource> ().clip = eatHealth;
			GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
			GetComponent<AudioSource> ().Play ();
			other.GetComponent<Animation> ().Play ();
			other.GetComponent<PolygonCollider2D> ().enabled = false;
			Destroy (other.gameObject,1);
			//healthSpawned = true;
			lifeCount += 1;

			if (lifeCount == 3) {
				heart3.GetComponent<SpriteRenderer> ().enabled = true;
			} else if (lifeCount == 2) {
				heart2.GetComponent<SpriteRenderer> ().enabled = true;
			}
		}
			
		if (other.transform.tag == "Mushroom") { //removes the last 10 segments
			isOccupied [(int)other.gameObject.transform.position.x + 5, (int)other.gameObject.transform.position.y + 9] = false;
			GetComponent<AudioSource> ().clip = appleCrunch;
			GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
			GetComponent<AudioSource> ().Play ();
			other.GetComponent<Animation> ().Play ();
			other.GetComponent<PolygonCollider2D> ().enabled = false;
			Destroy (other.gameObject,1);
			ateMushroom = true;

			if (bodyParts.Count > 12) {
				
				int last = bodyParts.Count - 1; //index = 8
				float j = 0;
				rb.velocity = Vector2.zero * speed;
				tongue.GetComponent<Animator> ().enabled = false; 
				for (int i = last; i > last - 10; --i) {
					StartCoroutine (InstantiatePoof (i, 0.5f + j));
					Destroy (bodyParts [i].gameObject, 0.6f + j);
					j += 0.5f;
				}

				Invoke ("RemoveRange", 6);
				//bodyParts.RemoveRange (last - 5, 6);
			}
		}

		if (other.transform.tag == "Watermelon") {
			isOccupied [(int)other.gameObject.transform.position.x + 5, (int)other.gameObject.transform.position.y + 9] = false;
			GetComponent<AudioSource> ().clip = appleCrunch;
			GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
			GetComponent<AudioSource> ().Play ();
			other.GetComponent<Animation> ().Play ();
			other.GetComponent<CircleCollider2D> ().enabled = false;

			melonSpawned = false;
			seedsLeft = 3;
			Destroy (other.gameObject,1);
		}

		if (other.transform.tag == "Body") {
			if (!noPenalityEnabled) {
				GetComponent<AudioSource> ().clip = wallHit;
				GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
				GetComponent<AudioSource> ().Play ();
				rb.velocity = Vector2.zero * speed;
				tongue.GetComponent<Animator> ().enabled = false;
				loseLife = true;
				repeat = true;
				bodyHit = true;
			} else {
				bodyHit = true;
				moveSnake ();
			}
		}

		if (other.transform.tag == "Wall") {
			if (!noPenalityEnabled) {
				GetComponent<AudioSource> ().clip = wallHit;
				GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
				GetComponent<AudioSource> ().Play ();
				rb.velocity = Vector2.zero * speed;
				tongue.GetComponent<Animator> ().enabled = false;
				loseLife = true;
				repeat = true;
				borderHit = true;

			} else {
				borderHit = true;
				moveSnake ();
			}
		}

		if (other.transform.tag == "Bomb") {
			if (!noPenalityEnabled) {
				GameObject explosionAnimation;
				GetComponent<AudioSource> ().clip = explode;
				GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
				GetComponent<AudioSource> ().Play ();
				explosionAnimation = Instantiate (explosion, other.transform.position, Quaternion.identity) as GameObject;
				Destroy (explosionAnimation, 4);
				Destroy (other.gameObject);
				rb.velocity = Vector2.zero * speed;
				tongue.GetComponent<Animator> ().enabled = false; //stop tongue animation
				loseLife = true;
				repeat = true;
				bombHit = true;
			} else {
				bombHit = true;
				moveSnake ();
			}
		}

		if (other.transform.tag == "Lightningx2") {
			GetComponent<AudioSource> ().clip = lightning;
			GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
			GetComponent<AudioSource> ().Play ();
			isOccupied [(int)other.gameObject.transform.position.x + 5, (int)other.gameObject.transform.position.y + 9] = false;
			lightTriggered = true;
			other.GetComponent<Animation> ().Play ();
			other.GetComponent<BoxCollider2D> ().enabled = false;

			Destroy (other.gameObject,1);

			if (speed <= 10.125f) {
				speed *= 1.5f;
				InstantiateSpeedText (other);
			}

		}

		if (other.transform.tag == "Snail") {
			GetComponent<AudioSource> ().clip = eatSnail;
			GetComponent<AudioSource> ().volume = PlayerPrefs.GetFloat ("SoundEffects", 1);
			GetComponent<AudioSource> ().Play ();
			isOccupied [(int)other.gameObject.transform.position.x + 5, (int)other.gameObject.transform.position.y + 9] = false;
			other.GetComponent<Animation> ().Play ();
			other.GetComponent<CircleCollider2D> ().enabled = false;

			Destroy (other.gameObject,1);

			if (speed >= 4.5f) {
				speed = speed / 1.5f;
				InstantiateSpeedText (other);
			}
		}
	}

	void RemoveRange() {
		int last = bodyParts.Count - 1;
		bodyParts.RemoveRange (last - 9, 10);
		ateMushroom = false;
		mushroomSpawned = false;
	}

	IEnumerator InstantiatePoof(int num, float waitTime) {
		//Debug.Log (num.ToString () + " = (" + bodyParts [num].transform.position.x.ToString () + bodyParts [num].transform.position.y.ToString () + ")");
		yield return new WaitForSeconds(waitTime);
		//Debug.Log ("WaitTime = " + waitTime.ToString ());
		GameObject poof1 = Instantiate (poof, bodyParts [num].transform.position, Quaternion.identity) as GameObject;
		Destroy (poof1, 0.42f);
	}

	//**********************************************************   SPAWNS   ******************************************************************
	void InstantiateSpeedText(Collider2D col){
		if (speed == 3) {
			Instantiate (normal, col.transform.position, Quaternion.identity);
		}
		else if (speed == 4.5f) {
			Instantiate (swift, col.transform.position, Quaternion.identity);
		} else if (speed == 6.75f) {
			Instantiate (fast, col.transform.position, Quaternion.identity);
		} else if (speed == 10.125f) {
			Instantiate (insane, col.transform.position, Quaternion.identity);
		} else if (speed == 15.1875) {
			Instantiate (maxSpeed, col.transform.position, Quaternion.identity);
		}
	}

	void SpawnApple(int amount){
		for (int i = 0; i < amount; ++i) {
			int x = (int) Random.Range (-5, 5);
			int y = (int) Random.Range (-9, 10);


			while (isOccupied[x+5,y+9] || (x > transform.position.x && x < transform.position.x + 2)
				|| (x < transform.position.x && x > transform.position.x - 2) || (y > transform.position.y && y < transform.position.y + 2)
				|| (y < transform.position.y && y > transform.position.y - 2)){
				x = (int) Random.Range (-5, 5);
				y = (int) Random.Range (-9, 10);
			}

			isOccupied[x+5,y+9] = true;
			Instantiate (apple, new Vector2 (x, y), Quaternion.identity);
		}
	}

	void SpawnMelon() {
		int x = (int) Random.Range (-5, 5);
		int y = (int) Random.Range (-9, 10);

		while (isOccupied[x+5,y+9] || (x > transform.position.x && x < transform.position.x + 2)
			|| (x < transform.position.x && x > transform.position.x - 2) || (y > transform.position.y && y < transform.position.y + 2)
			|| (y < transform.position.y && y > transform.position.y - 2)) {
			x = (int) Random.Range (-5, 5);
			y = (int) Random.Range (-9, 10); 
		}

		isOccupied[x+5,y+9] = true;
		Instantiate(watermelon, new Vector2(x,y), Quaternion.identity);
	}

	void SpawnSnail() {
		int x = (int) Random.Range (-5, 5);
		int y = (int) Random.Range (-9, 10);
		
		while (isOccupied[x+5,y+9] || (x > transform.position.x && x < transform.position.x + 2)
			|| (x < transform.position.x && x > transform.position.x - 2) || (y > transform.position.y && y < transform.position.y + 2)
			|| (y < transform.position.y && y > transform.position.y - 2)){
			x = (int) Random.Range (-5, 5);
			y = (int) Random.Range (-9, 10);
		}
		
		isOccupied[x+5,y+9] = true;
		Instantiate (snail, new Vector2 (x, y), Quaternion.identity);
	}
		

	void SpawnBomb() {
		int x = (int) Random.Range (-5, 5);
		int y = (int) Random.Range (-9, 10);

		while (isOccupied[x+5,y+9] || (x > transform.position.x && x < transform.position.x + 2)
			|| (x < transform.position.x && x > transform.position.x - 2) || (y > transform.position.y && y < transform.position.y + 2)
			|| (y < transform.position.y && y > transform.position.y - 2)){
			x = (int) Random.Range (-5, 5);
			y = (int) Random.Range (-9, 10);
		}
		
		isOccupied[x+5,y+9] = true;
		Instantiate (bomb, new Vector2 (x, y), Quaternion.identity);
	}

	void SpawnLightning(){
		int x = (int) Random.Range (-5, 5);
		int y = (int) Random.Range (-9, 10);
		
		while (isOccupied[x+5,y+9] || (x > transform.position.x && x < transform.position.x + 2)
			|| (x < transform.position.x && x > transform.position.x - 2) || (y > transform.position.y && y < transform.position.y + 2)
			|| (y < transform.position.y && y > transform.position.y - 2)){
			x = (int) Random.Range (-5, 5);
			y = (int) Random.Range (-9, 10);
		}
		
		isOccupied[x+5,y+9] = true;
		Instantiate (light2, new Vector2 (x, y), Quaternion.identity);
	}

	void SpawnHealth() {
		int x = (int)Random.Range (-5, 5);
		int y = (int)Random.Range (-9, 10);

		while (isOccupied [x + 5, y + 9] || (x > transform.position.x && x < transform.position.x + 2)
		       || (x < transform.position.x && x > transform.position.x - 2) || (y > transform.position.y && y < transform.position.y + 2)
		       || (y < transform.position.y && y > transform.position.y - 2)) {
			x = (int)Random.Range (-5, 5);
			y = (int)Random.Range (-9, 10);
		}

		isOccupied [x + 5, y + 9] = true;
		Instantiate (health, new Vector2 (x, y), Quaternion.identity);
	}

	void SpawnMushroom() {
		int x = (int)Random.Range (-5, 5);
		int y = (int)Random.Range (-9, 10);

		while (isOccupied [x + 5, y + 9] || (x > transform.position.x && x < transform.position.x + 2)
			|| (x < transform.position.x && x > transform.position.x - 2) || (y > transform.position.y && y < transform.position.y + 2)
			|| (y < transform.position.y && y > transform.position.y - 2)) {
			x = (int)Random.Range (-5, 5);
			y = (int)Random.Range (-9, 10);
		}

		isOccupied [x + 5, y + 9] = true;
		Instantiate (mushroom, new Vector2 (x, y), Quaternion.identity);
	}
}