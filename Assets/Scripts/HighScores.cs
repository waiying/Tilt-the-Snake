using UnityEngine;
using System.Collections;

public class HighScores : MonoBehaviour {

	public GameObject score;

	void Start() {
		if (PlayerPrefs.HasKey("Scores")){
			if (transform.tag == "user1" && PlayerPrefs.HasKey("Name1")) {
				GetComponent<TextMesh>().text = PlayerPrefs.GetString("Name1");
				score.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("Score1").ToString();
			} else if (transform.tag == "user2" && PlayerPrefs.HasKey("Name2")) {
				GetComponent<TextMesh>().text = PlayerPrefs.GetString("Name2");
				score.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("Score2").ToString();
			} else if (transform.tag == "user3" && PlayerPrefs.HasKey("Name3")) {
				GetComponent<TextMesh>().text = PlayerPrefs.GetString("Name3");
				score.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("Score3").ToString();
			} else if (transform.tag == "user4" && PlayerPrefs.HasKey("Name4")) {
				GetComponent<TextMesh>().text = PlayerPrefs.GetString("Name4");
				score.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("Score4").ToString();
			} else if (transform.tag == "user5" && PlayerPrefs.HasKey("Name5")) {
				GetComponent<TextMesh>().text = PlayerPrefs.GetString("Name5");
				score.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("Score5").ToString();
			} else if (transform.tag == "user6" && PlayerPrefs.HasKey("Name6")) {
				GetComponent<TextMesh>().text = PlayerPrefs.GetString("Name6");
				score.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("Score6").ToString();
			} else if (transform.tag == "user7" && PlayerPrefs.HasKey("Name7")) {
				GetComponent<TextMesh>().text = PlayerPrefs.GetString("Name7");
				score.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("Score7").ToString();
			} else if (transform.tag == "user8" && PlayerPrefs.HasKey("Name8")) {
				GetComponent<TextMesh>().text = PlayerPrefs.GetString("Name8");
				score.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("Score8").ToString();
			}
		}
	}
}
