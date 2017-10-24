using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldRotation : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update()
	{
		// Rotate the object around its local Z axis at 1 degree per second
			transform.Rotate (Vector3.forward * Time.deltaTime * speed);

	}
}
