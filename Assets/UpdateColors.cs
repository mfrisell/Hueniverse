using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateColors : MonoBehaviour {

	private Color colorLeft;
	private Color colorRight;

	// Use this for initialization
	void Start () {

//		GameObject playerControllerObject = GameObject.FindGameObjectWithTag ("Player");
//		PlayerController playerControllerScript = playerControllerObject.GetComponent<PlayerController> ();
//
//		colorLeft = playerControllerScript.leftCurrentColor;
//		colorRight = playerControllerScript.rightCurrentColor;
		
	}
	
	// Update is called once per frame
	void Update () {

		GameObject playerControllerObject = GameObject.FindGameObjectWithTag ("Player");
		PlayerController playerControllerScript = playerControllerObject.GetComponent<PlayerController> ();

		colorLeft = playerControllerScript.leftCurrentColor;
		colorRight = playerControllerScript.rightCurrentColor;

		GameObject leftOuterColorObject = GameObject.FindGameObjectWithTag ("leftOuterColor");
		Image imgLeft = leftOuterColorObject.GetComponent<Image> ();
		colorLeft.a = 0.2f;
		imgLeft.color = colorLeft;

		GameObject rightOuterColorObject = GameObject.FindGameObjectWithTag ("rightOuterColor");
		Image imgRight = rightOuterColorObject.GetComponent<Image> ();
		colorRight.a = 0.2f;
		imgRight.color = colorRight;

	}
}
