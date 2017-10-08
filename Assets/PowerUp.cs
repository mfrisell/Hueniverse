using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

	private float powerUp;
	private bool powerUpAvailable;

	// Use this for initialization
	void Start () {

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		GameController gameControllerScript = gameControllerObject.GetComponent<GameController> ();
		powerUp = gameControllerScript.powerUp;
		powerUpAvailable = gameControllerScript.powerUpAvailable;
		
	}
	
	// Update is called once per frame
	void Update () {

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		GameController gameControllerScript = gameControllerObject.GetComponent<GameController> ();
		powerUp = gameControllerScript.powerUp;
		powerUpAvailable = gameControllerScript.powerUpAvailable;

		Image img = GetComponent<Image> ();

		img.fillAmount = powerUp;
		
	}
}
