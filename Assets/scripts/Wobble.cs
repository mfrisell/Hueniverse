using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour {

	private float x = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//
//		var trans = transform.localPosition.y;
//		trans = 1 + Mathf.Sin (x);



		float newY = 1 + (Mathf.Sin (x))/4;
		transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);

		x += 0.01f;
		
	}
}
