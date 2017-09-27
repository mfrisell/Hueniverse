using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTextController : MonoBehaviour {

	public GameController gameControllerScript;
	public bool gameOver;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		gameOver = gameControllerScript.gameOver;

		if (gameOver)
			anim.SetTrigger ("GameOver");
	}
}
