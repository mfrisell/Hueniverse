using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeRight : MonoBehaviour {

	public float TimeScale = 2f;
	private Color currentColor = Color.red;
	public Color startColorRight;

	private Color endColor;

	private Color lerpedColor;
	private ParticleSystem ps;

	public string chosenColorRight = "red";

	// Use this for initialization
	void Start () {
		startColorRight = currentColor;

		//		gameObject.GetComponentInParent<Script>();

		ps = gameObject.GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown((KeyCode.R))) {
			endColor = Color.red;
			startColorRight = endColor;
			chosenColorRight = "red";
			StartCoroutine(LerpColorRight());
		}

		if (Input.GetKeyDown((KeyCode.G))) {
			endColor = Color.green;
			startColorRight = endColor;
			chosenColorRight = "green";
			StartCoroutine(LerpColorRight());
		}

		if (Input.GetKeyDown((KeyCode.B))) {
			endColor = Color.blue;
			startColorRight = endColor;
			chosenColorRight = "blue";
			StartCoroutine(LerpColorRight());
		}
	}

	private IEnumerator LerpColorRight(){
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
