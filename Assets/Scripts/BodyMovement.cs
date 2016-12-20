using UnityEngine;
using System.Collections;

public class BodyMovement : MonoBehaviour {

	int myOrder;
	GameObject head;
	float rotationSpeed;
	public bool stopped = false;
	bool animated = false;
	
	void Start(){
		animated = false;
		rotationSpeed = Time.deltaTime * 8;
		head = GameObject.FindGameObjectWithTag ("Player1").gameObject;
		for (int i = 0; i < head.GetComponent<SnakeMovement>().bodyParts.Count; i++) {
			if (gameObject == head.GetComponent<SnakeMovement> ().bodyParts [i].gameObject) {
				myOrder = i;
			}
		}
	}

	Vector3 movementVelocity;
	[Range(0.0f, 1.0f)]
	public float overTime = 0.5f;

	void Update() {
		if ((SnakeMovement.bodyHit || SnakeMovement.bombHit || SnakeMovement.borderHit) && !animated && !SnakeMovement.noPenalityEnabled) {
			//Debug.Log ("1");
			Invoke ("FlashAnimation", 2.1f);
			animated = true;
		}
			
	}

	void FlashAnimation() {
		if (!SnakeMovement.gameOver) {
			//Debug.Log ("Flash animation invoked.");
			GetComponent<Animation> ().Play ();
			animated = false;
		}
	}

	void FixedUpdate(){
		if ((head.GetComponent<SnakeMovement> ().loseLife && !SnakeMovement.noPenalityEnabled) || SnakeMovement.ateMushroom) {
			//Debug.Log ("body stopped");
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			stopped = true;
		}
		else if (head.GetComponent<SnakeMovement> ().isMoving) {
			if (myOrder == 0) {
				transform.position = Vector3.SmoothDamp (transform.position, head.transform.position, ref movementVelocity, overTime);
				transform.rotation = Quaternion.Lerp (transform.rotation, head.transform.rotation, rotationSpeed);
			} else {
				transform.position = Vector3.SmoothDamp (transform.position, head.GetComponent<SnakeMovement> ().bodyParts [myOrder - 1].position, ref movementVelocity, overTime);
				transform.rotation = Quaternion.Lerp (transform.rotation, head.GetComponent<SnakeMovement> ().bodyParts [myOrder - 1].transform.rotation, rotationSpeed);
			}
		}

		//NORMAL//
		if (head.GetComponent<SnakeMovement> ().speed == 3) {
			rotationSpeed = Time.deltaTime * 8;
			if (myOrder == 0) {
				overTime = 0.18f;
			} 
			else if (myOrder == 1){
				overTime = 0.12f;
			}
			else if (myOrder == 2) {
				overTime = 0.13f;
			}
			else{
				overTime = 0.14f;
			}
		}

		//SWIFT//
		if (head.GetComponent<SnakeMovement> ().speed == 4.5f) {
			rotationSpeed = Time.deltaTime * 9;
			if (myOrder == 0) {
				overTime = 0.125f;
			} 
			else if (myOrder == 1){
				overTime = 0.07f;
			}
			else if (myOrder == 2) {
				overTime = 0.08f;
			}
			else{
				overTime = 0.09f;
			}
		}
		//FAST//
		else if (head.GetComponent<SnakeMovement> ().speed == 6.75f){
			rotationSpeed = Time.deltaTime * 12;
			if (myOrder == 0) {
				overTime = 0.075f;
			} 
			else if (myOrder == 1){
				overTime = 0.04f;
			}
			else if (myOrder == 2){
				overTime = 0.045f;
			}
			else{
				overTime = 0.06f;
			}
		}
		//INSANE//
		else if (head.GetComponent<SnakeMovement> ().speed == 10.125f){
			rotationSpeed = Time.deltaTime * 17;
			if (myOrder == 0) {
				overTime = 0.04f;
			} 
			else if (myOrder == 1) {
				overTime = 0.025f;
			}
			else if (myOrder == 2){
				overTime = 0.025f;
			}
			else {
				overTime = 0.045f;
			}
		}
		//MAX SPEED//
		else if (head.GetComponent<SnakeMovement> ().speed == 15.1875){
			rotationSpeed = Time.deltaTime * 20;
			if (myOrder == 0) {
				overTime = 0.02f;
			}
			else if (myOrder == 1){
				overTime = 0.01f;
			}
			else if (myOrder == 2){
				overTime = 0.01f;
			}
			else {
				overTime = 0.03f;
			}
		}
	}

}