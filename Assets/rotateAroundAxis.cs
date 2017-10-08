using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAroundAxis : MonoBehaviour {

	public bool speedFromLifeCircleHolder = false;

	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update()
	{
		// Rotate the object around its local Z axis at 1 degree per second
		if (!speedFromLifeCircleHolder) {
			transform.Rotate (Vector3.forward * Time.deltaTime * speed);
		} else {

			GameObject lifeCircleHolderObject = GameObject.FindGameObjectWithTag ("lifeCircleHolder");

			rotateAroundAxis raa = lifeCircleHolderObject.GetComponent<rotateAroundAxis> ();

			transform.Rotate (Vector3.forward * Time.deltaTime * -raa.speed);
		}

	}
}
