using UnityEngine;
using System.Collections;

public class InstantiateSnake : MonoBehaviour {

	public GameObject green, blue, pink, yellow, purple, red, black, white;

	// Use this for initialization
	void Start () {

		if (ColorSelection.green) {
			Instantiate (green, transform.position, Quaternion.identity);
		} else if (ColorSelection.blue) {
			Instantiate (blue, transform.position, Quaternion.identity);
		} else if (ColorSelection.pink) {
			Instantiate (pink, transform.position, Quaternion.identity);
		} else if (ColorSelection.yellow) {
			Instantiate (yellow, transform.position, Quaternion.identity);
		} else if (ColorSelection.purple) {
			Instantiate (purple, transform.position, Quaternion.identity);
		} else if (ColorSelection.red) {
			Instantiate (red, transform.position, Quaternion.identity);
		} else if (ColorSelection.black) {
			Instantiate (black, transform.position, Quaternion.identity);
		} else if (ColorSelection.white) {
			Instantiate (white, transform.position, Quaternion.identity);
		}

	}

}
