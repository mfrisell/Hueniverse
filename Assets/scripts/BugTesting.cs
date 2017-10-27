using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BugTesting : MonoBehaviour {

	private Color colorLeft;
	private Color colorRight;

	private int score;
	private int lifes;

	private bool UIactive = true;
	private bool Bpressed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		GameObject playerControllerObject = GameObject.FindGameObjectWithTag ("Player");
		PlayerController playerControllerScript = playerControllerObject.GetComponent<PlayerController> ();
		colorLeft = playerControllerScript.leftCurrentColor;
		colorRight = playerControllerScript.rightCurrentColor;

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		GameController gameControllerScript = gameControllerObject.GetComponent<GameController> ();
		lifes = gameControllerScript.lifes;
		score = gameControllerScript.score;

		if(Input.GetKey(KeyCode.B) && !Bpressed) {
			Bpressed = true;
			StartCoroutine (ShowUI ());
		}
		
	}

	public void buttonPress(int x) {

		GameObject playerControllerObject = GameObject.FindGameObjectWithTag ("Player");
		PlayerController playerControllerScript = playerControllerObject.GetComponent<PlayerController> ();
		colorLeft = playerControllerScript.leftCurrentColor;
		colorRight = playerControllerScript.rightCurrentColor;

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		GameController gameControllerScript = gameControllerObject.GetComponent<GameController> ();
		lifes = gameControllerScript.lifes;
		score = gameControllerScript.score;

		switch (x) {
		case 0:
			score += 10;
			gameControllerScript.score = score;
			break;
		case 1:
			score -= 10;
			gameControllerScript.score = score;
			break;
		case 2:
			lifes += 1;
			gameControllerScript.lifes = lifes;
			break;
		case 3:
			lifes -= 1;
			gameControllerScript.lifes = lifes;
			break;
		case 4:
			playerControllerScript.leftCurrentColor = Color.red;
			break;
		case 5:
			playerControllerScript.leftCurrentColor = Color.green;
			break;
		case 6:
			playerControllerScript.leftCurrentColor = Color.blue;
			break;
		case 7:
			playerControllerScript.rightCurrentColor = Color.red;
			break;
		case 8:
			playerControllerScript.rightCurrentColor = Color.green;
			break;
		case 9:
			playerControllerScript.rightCurrentColor = Color.blue;
			break;
		case 10:
            gameControllerScript.gameTime = 0;
			SteamVR_LoadLevel.Begin("Start");
			break;
		default:
			break;
			
		}
	}

	IEnumerator ShowUI() {
		UIactive = !UIactive;

		foreach (Transform child in this.transform)
		{
			child.gameObject.SetActive(UIactive);
		}
			
		yield return new WaitForSeconds (0.5f);
		Bpressed = false;
	}
}
