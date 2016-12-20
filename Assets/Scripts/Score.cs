using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public Texture2D[] numbers = new Texture2D[10];
	public float width = Screen.width;
	public float height = Screen.height;
	bool scoresUpdated;
	int[] highscores = new int[8];
	string highScoreKey;
	string nameKey, userName, temp1;
	int score, temp;
	public static int rank;

	void Start() {
		scoresUpdated = false;
		rank = 10;
	}

	void OnGUI(){
		string scoreStr = SnakeMovement.score.ToString ();
		for (int i = 0; i < scoreStr.Length; i++) {
			//0.08f * screen.width, 0.045f * screen.height
			//positon: 0.015f * screen.height, (0.049f + (0.065f * i)) * screen.width
			//new Rect((units from star + (spacing * i)) * screen.width
			GUI.DrawTexture(new Rect((0.49f + (0.050f * i)) * Screen.width, 0.02f * Screen.height, 0.06f * Screen.width, 0.034f * Screen.height), numbers [int.Parse ("" + scoreStr [i])]);
		}
	}

	void Update() {
		if (SnakeMovement.gameOver && !scoresUpdated) {
			//Saving high scores...
			if (!PlayerPrefs.HasKey("Scores")){
				PlayerPrefs.SetInt("Scores", 0);
			}

			score = SnakeMovement.score;
			userName = GetName.userName;

			for (int i = 0; i<highscores.Length; i++) {

				highScoreKey = "Score" + (i+1).ToString();
				nameKey = "Name" + (i+1).ToString();

				if (score > PlayerPrefs.GetInt(highScoreKey,0)) {
					if (rank == 10) {
						rank = i + 1;
					}
					temp = PlayerPrefs.GetInt(highScoreKey,0);
					temp1 = PlayerPrefs.GetString(nameKey);
					PlayerPrefs.SetInt(highScoreKey, score);
					PlayerPrefs.SetString(nameKey, userName);
					score = temp;
					userName = temp1;
				}
			}

			Options.deleted = false;
			scoresUpdated = true;
		}
	}
}
