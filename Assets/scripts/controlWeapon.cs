using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlWeapon : MonoBehaviour {

	public float speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		// Disable vertical
		moveVertical = 0.0f;

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = movement * speed;
	}
}
