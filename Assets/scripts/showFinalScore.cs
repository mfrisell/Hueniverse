using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showFinalScore : MonoBehaviour {

	public GameController gameControllerScript;
	public bool gameOver;
	private Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		gameOver = gameControllerScript.gameOver;

		if (gameOver)
			updateText ();
	}

	void updateText (){
		scoreText.text = "Final score: " + gameControllerScript.score.ToString();
	}
}
