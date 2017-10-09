using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JupiterMovement : MonoBehaviour {

	private Rigidbody jupiterRigid;

	// Use this for initialization
	void Start () {
		jupiterRigid = this.GetComponent<Rigidbody> ();
		Vector3 velVector = new Vector3(0,0,-10);
		jupiterRigid.velocity = velVector;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0,0,1f*Time.deltaTime);
	}
}
