using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	//private GameObject gameControllerObject;
	public GameController gameControllerScript;
	public bool gameOver;

	//private Text finalScore;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		//GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		//GameController gameControllerScript;

		//GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		//GameController gameControllerScript = gameControllerObject.GetComponent<GameController> ();
		bool gameOver = gameControllerScript.gameOver;
	}
	
	// Update is called once per frame
	void Update () {
		//GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		//GameController gameControllerScript = gameControllerObject.GetComponent<GameController> ();
		gameOver = gameControllerScript.gameOver;
		if (gameOver == true) {
			Debug.Log ("Setting trigger");
			anim.SetTrigger ("GameOverText");
			//showHighScore ();
		}
	}

	/*void showHighScore() {
		//GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		//GameController gameControllerScript = gameControllerObject.GetComponent<GameController> ();
		finalScore = GetComponent<Text> ();	
		string scoreText = "GAME OVER!\n\nYOUR SCORE: " + gameControllerScript.score.ToString();
		finalScore.text = scoreText;
	}*/
}
