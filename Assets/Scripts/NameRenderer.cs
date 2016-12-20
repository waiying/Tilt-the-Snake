using UnityEngine;
using System.Collections;

public class NameRenderer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<TextMesh> ().text = GetName.userName;
		GetComponent<Renderer> ().sortingLayerName = "Decorations";
		GetComponent<Renderer> ().sortingOrder = 1;
	}

}
