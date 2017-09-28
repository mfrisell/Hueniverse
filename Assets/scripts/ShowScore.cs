using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

	private Text scoreText;
	private int previousScore;
	private int currentScore;
	private bool incrementing;

	private AudioSource audio;

	// Use this for initialization
	void Start () {

		// Fetch score from GameController
		scoreText = GetComponent<Text>();
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		GameController gameControllerScript = gameControllerObject.GetComponent<GameController> ();
		int gameScore = gameControllerScript.score;

		// Set previous and current score
		previousScore = gameScore;
		currentScore = gameScore;

		incrementing = false;

		// Get Audio Source
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		GameController gameControllerScript = gameControllerObject.GetComponent<GameController> ();
		int gameScore = gameControllerScript.score;
		currentScore = gameScore;

		// increment score if score is not already incrementing
		if (!incrementing) {
			// Update GUI score
			StartCoroutine (scoreIncrement ());
		}
	}

	IEnumerator scoreIncrement() {
		incrementing = true;

		while (previousScore < currentScore) {
            previousScore++;

			string scoreTextString = previousScore.ToString();
			scoreText.text = scoreTextString;

			if(previousScore % 2 == 0) {
				audio.Play ();
			}


			yield return new WaitForSeconds (0.03f);
		}

        while (previousScore > currentScore)
        {
            previousScore -= 10;

            string scoreTextString = previousScore.ToString();
            scoreText.text = scoreTextString;

            if (previousScore % 2 == 0)
            {
                audio.Play();
            }


            yield return new WaitForSeconds(0.05f);
        }


        previousScore = currentScore;
		incrementing = false;

	}
}
