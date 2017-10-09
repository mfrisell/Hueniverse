﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation : MonoBehaviour {

	private Rigidbody earthRigid;

	// Use this for initialization
	void Start () {
		earthRigid = this.GetComponent<Rigidbody> ();
		//Vector3 zpos = transform.position.z;
		//zpos += 0.5f * Time.deltaTime;
		Vector3 velVector = new Vector3(0,0,-70);
		earthRigid.velocity = velVector;
		//Vector3 (0, 0, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0,0,1f*Time.deltaTime);
	}
}
