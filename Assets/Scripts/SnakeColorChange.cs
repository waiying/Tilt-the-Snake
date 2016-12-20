using UnityEngine;
using System.Collections;

public class SnakeColorChange : MonoBehaviour {

	void Update() {

		if (ColorSelection.green && gameObject.tag == "Green") {
			this.GetComponent<SpriteRenderer>().enabled = true;
		} else if (ColorSelection.blue && gameObject.tag == "Blue") {
			this.GetComponent<SpriteRenderer>().enabled = true;
		} else if (ColorSelection.pink && gameObject.tag == "Pink") {
			this.GetComponent<SpriteRenderer>().enabled = true;
		} else if (ColorSelection.yellow && gameObject.tag == "Yellow") {
			this.GetComponent<SpriteRenderer>().enabled = true;
		} else if (ColorSelection.purple && gameObject.tag == "Purple") {
			this.GetComponent<SpriteRenderer>().enabled = true;
		} else if (ColorSelection.red && gameObject.tag == "Red") {
			this.GetComponent<SpriteRenderer>().enabled = true;
		} else if (ColorSelection.black && gameObject.tag == "Black") {
			this.GetComponent<SpriteRenderer>().enabled = true;
		} else if (ColorSelection.white && gameObject.tag == "White") {
			this.GetComponent<SpriteRenderer>().enabled = true;
		} else
			this.GetComponent<SpriteRenderer>().enabled = false;

	}
}
