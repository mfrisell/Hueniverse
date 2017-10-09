using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaturnMovement : MonoBehaviour {

	private Rigidbody saturnRigid;
	// Use this for initialization
	void Start () {
		saturnRigid = this.GetComponent<Rigidbody> ();
		Vector3 velVector = new Vector3(0,0,-50);
		saturnRigid.velocity = velVector;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0,0,3f*Time.deltaTime);
	}
}
