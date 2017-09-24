using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowProgress : MonoBehaviour {

	public float gameProgress;
	private float timer;

	// Use this for initialization
	void Start () {

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		GameController gameControllerScript = gameControllerObject.GetComponent<GameController> ();
		gameProgress = gameControllerScript.maxGameTime;
		
	}
	
	// Update is called once per frame
	void Update () {

		// Get time, convert to range (0,1)
		timer += Time.deltaTime;
		float timeToProgress = map01 (timer, 0, gameProgress);

		Image progress = GetComponent<Image>();
		progress.fillAmount = timeToProgress;
	}

	public static float map01( float value, float min, float max )  
	{
		return ( value - min ) * 1f / ( max - min );
	}
}
