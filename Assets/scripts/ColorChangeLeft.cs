using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeLeft : MonoBehaviour {

	public float TimeScale = 2f;
	private Color currentColor = Color.red;
	public Color startColorLeft;

	private Color endColor;

	private Color lerpedColor;
	private ParticleSystem ps;

	public string chosenColorLeft = "red";

	// Use this for initialization
	void Start () {
		startColorLeft = currentColor;

		//		gameObject.GetComponentInParent<Script>();

		ps = gameObject.GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown((KeyCode.T))) {
			endColor = Color.red;
			startColorLeft = endColor;
			chosenColorLeft = "red";
			StartCoroutine(LerpColorLeft());
		}

		if (Input.GetKeyDown((KeyCode.H))) {
			endColor = Color.green;
			startColorLeft = endColor;
			chosenColorLeft = "green";
			StartCoroutine(LerpColorLeft());
		}

		if (Input.GetKeyDown((KeyCode.N))) {
			endColor = Color.blue;
			startColorLeft = endColor;
			chosenColorLeft = "blue";
			StartCoroutine(LerpColorLeft());
		}
	}

	private IEnumerator LerpColorLeft(){
		float progress = 0;

		while(progress <= 1){
			lerpedColor = Color.Lerp(currentColor, endColor, progress);

			var main = ps.main;
			main.startColor = lerpedColor;

			progress += Time.deltaTime * TimeScale;
			yield return null;
		}

		currentColor = endColor;

	} 
}
