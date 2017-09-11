using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWeapon : MonoBehaviour {

	public GameObject redBolt;
	public GameObject blueBolt;
	public GameObject greenBolt;

	public GameObject magentaBolt;
	public GameObject yellowBolt;
	public GameObject cyanBolt;

	public float speed;

	public float fireDelta = 0.5F;
	private float nextFire = 0.5F;
	private float myTime = 0.0F;

	public Color chosenColor;

	public Transform shotSpawn;
	private GameObject newProjectile;


	// Use this for initialization
	void Start () {
		chosenColor = Color.red;
	}
	
	// Update is called once per frame
	void Update () {

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
		// Disable vertical
		moveVertical = 0.0f;

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = movement * speed;
	}

	IEnumerator Shoot() {

		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
		yield return new WaitForSeconds (0.5f);

		GameObject childGameObject = GameObject.FindGameObjectWithTag("CircleTag");

		ColorChange cc = childGameObject.GetComponent<ColorChange>();

		if(cc.chosenColor == "red") newProjectile = Instantiate(redBolt, shotSpawn.position, shotSpawn.rotation) as GameObject;
		if(cc.chosenColor == "green") newProjectile = Instantiate(greenBolt, shotSpawn.position, shotSpawn.rotation) as GameObject;
		if(cc.chosenColor == "blue") newProjectile = Instantiate(blueBolt, shotSpawn.position, shotSpawn.rotation) as GameObject;
	}
}
