using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour {

	private float TimeScale = 0.2f;
	public Color startColor;
	public Color endColor;

	public Color lerpedColor = Color.blue;
	public ParticleSystem ps;

	// Use this for initialization
	void Start () {

		ps = gameObject.GetComponent<ParticleSystem> ();

		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown((KeyCode.Space))) {
			StartCoroutine(LerpColor());

		}
		
	}

	IEnumerator LerpColor(){
		float progress = 0;


		while(progress <= 1){
			lerpedColor = Color.Lerp(Color.red, Color.blue, Mathf.SmoothStep(0.0f, 1.0f, progress));

			var main = ps.main;
			main.startColor = lerpedColor;

//			transform.localScale = Vector3.Slerp(startColor, endColor,  Mathf.SmoothStep(0.0f, 1.0f, progress));
			progress += Time.deltaTime * TimeScale;
			yield return null;
		}

		// Sätt slutgiltigt värde

	} 
}
