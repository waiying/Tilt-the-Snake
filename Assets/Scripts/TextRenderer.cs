using UnityEngine;
using System.Collections;

public class TextRenderer : MonoBehaviour {

	public string sortingLayerName;
	public int sortingOrder;

	void Start () {
		GetComponent<Renderer> ().sortingLayerName = sortingLayerName;
		GetComponent<Renderer> ().sortingOrder = sortingOrder;
	}
}
