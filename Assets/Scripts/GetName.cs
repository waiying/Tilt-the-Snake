using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetName : MonoBehaviour {

	InputField inputFieldCo;
	public static string userName;

	void Start() {
		inputFieldCo = GetComponent<InputField> ();
		if (PlayerPrefs.GetString("Saved Name", "") != "") {
			inputFieldCo.text = PlayerPrefs.GetString ("Saved Name");
		}
	}

	void Update() {
		userName = inputFieldCo.text;
		PlayerPrefs.SetString ("Saved Name", userName);
	}

}
