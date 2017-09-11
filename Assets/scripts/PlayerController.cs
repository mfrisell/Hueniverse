using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;

	public GameObject shot;
	public Transform shotSpawn;
	private GameObject newProjectile;

	public float fireDelta = 0.5F;
	private float nextFire = 0.5F;
	private float myTime = 0.0F;

	void Update() {

		myTime = myTime + Time.deltaTime;

		if (Input.GetButton("Fire1") && myTime > nextFire)
		{
			nextFire = myTime + fireDelta;

			StartCoroutine(Shoot());


			// create code here that animates the newProjectile

			nextFire = nextFire - myTime;
			myTime = 0.0F;
		}

	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
	
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = movement * speed;

	}

	IEnumerator Shoot() {

		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
		yield return new WaitForSeconds (0.5f);
		newProjectile = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
	}

}
