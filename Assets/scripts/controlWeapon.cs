using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

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
	private float myTimeLeft = 0.0F;
	private float myTimeRight = 0.0F;

	public Color chosenColor;

	public Transform shotSpawnLeft;
	public Transform shotSpawnRight;
	public Transform shotSpawnCenter;

	private GameObject newProjectile;

    private int deviceindexLeft;
    private int deviceindexRight;


	// Use this for initialization
	void Start () {
		chosenColor = Color.red;

        deviceindexLeft = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        deviceindexRight = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
    }

    // Update is called once per frame
    void Update () {


        //Debug.Log(deviceindexLeft);
        //Debug.Log(deviceindexRight);

        //if (deviceindexLeft != -1 && SteamVR_Controller.Input(deviceindexLeft).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    Debug.Log("hejsan");
        //}

        //if (deviceindexLeft != -1 && SteamVR_Controller.Input(deviceindexLeft).GetTouch(SteamVR_Controller.ButtonMask.Touchpad)) {
        //    var axis = SteamVR_Controller.Input(deviceindexLeft).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
        //    //Debug.Log("axis: " + axis);

        //}

        myTimeLeft = myTimeLeft + Time.deltaTime;

		if (Input.GetButton("Fire1") && myTimeLeft > nextFire) // TEST TEST TEST WITHOUT VIVE
		{
			nextFire = myTimeLeft + fireDelta;

			StartCoroutine(Shoot("left"));

			nextFire = nextFire - myTimeLeft;
			myTimeLeft = 0.0F;
		}

		if (deviceindexLeft != -1 && SteamVR_Controller.Input(deviceindexLeft).GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && myTimeLeft > nextFire)
		{
			nextFire = myTimeLeft + fireDelta;

			StartCoroutine(Shoot("left"));

			nextFire = nextFire - myTimeLeft;
			myTimeLeft = 0.0F;
            //Debug.Log(deviceindexLeft);
        }

		myTimeRight = myTimeRight + Time.deltaTime;

		if (deviceindexRight != -1 && SteamVR_Controller.Input(deviceindexRight).GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && myTimeRight > nextFire)
		{
			nextFire = myTimeRight + fireDelta;

			StartCoroutine(Shoot("right"));

			nextFire = nextFire - myTimeRight;
			myTimeRight = 0.0F;
            //Debug.Log(deviceindexRight);
        }
		
	}

	//void FixedUpdate() {
	//	float moveHorizontal = Input.GetAxis ("Horizontal");
	//	float moveVertical = Input.GetAxis ("Vertical");
	//	// Disable vertical
	//	moveVertical = 0.0f;

	//	Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
	//	Rigidbody rb = GetComponent<Rigidbody> ();
	//	rb.velocity = movement * speed;
	//}

	IEnumerator Shoot(string weapon) {


		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
		yield return new WaitForSeconds (0.5f);


		GameObject childGameObjectLeft = GameObject.FindGameObjectWithTag ("CircleLeft");
		GameObject childGameObjectRight = GameObject.FindGameObjectWithTag ("CircleRight");

		Debug.Log (childGameObjectLeft);

		ColorChangeLeft ccLeft = childGameObjectLeft.GetComponent<ColorChangeLeft> ();
		ColorChangeRight ccRight = childGameObjectRight.GetComponent<ColorChangeRight> ();

		Debug.Log (childGameObjectLeft);

		if (ccLeft.chosenColorLeft != "magenta" && ccLeft.chosenColorLeft != "yellow" && ccLeft.chosenColorLeft != "cyan") {

			Debug.Log (ccLeft.chosenColorLeft);

			if (weapon == "left") {

				Debug.Log ("APPLE");
				if (ccLeft.chosenColorLeft == "red")
					newProjectile = Instantiate (redBolt, shotSpawnLeft.position, shotSpawnLeft.rotation) as GameObject;
				if (ccLeft.chosenColorLeft == "green")
					newProjectile = Instantiate (greenBolt, shotSpawnLeft.position, shotSpawnLeft.rotation) as GameObject;
				if (ccLeft.chosenColorLeft == "blue")
					newProjectile = Instantiate (blueBolt, shotSpawnLeft.position, shotSpawnLeft.rotation) as GameObject;
			
			} else if (weapon == "right") {
			
				if (ccRight.chosenColorRight == "red")
					newProjectile = Instantiate (redBolt, shotSpawnRight.position, shotSpawnRight.rotation) as GameObject;
				if (ccRight.chosenColorRight == "green")
					newProjectile = Instantiate (greenBolt, shotSpawnRight.position, shotSpawnRight.rotation) as GameObject;
				if (ccRight.chosenColorRight == "blue")
					newProjectile = Instantiate (blueBolt, shotSpawnRight.position, shotSpawnRight.rotation) as GameObject;
			}
		} else {

			if (ccLeft.chosenColorLeft == "magenta")
				newProjectile = Instantiate (magentaBolt, shotSpawnCenter.position, shotSpawnCenter.rotation) as GameObject;
			if (ccLeft.chosenColorLeft == "yellow")
				newProjectile = Instantiate (yellowBolt, shotSpawnCenter.position, shotSpawnCenter.rotation) as GameObject;
			if (ccLeft.chosenColorLeft == "cyan")
				newProjectile = Instantiate (cyanBolt, shotSpawnCenter.position, shotSpawnCenter.rotation) as GameObject;
		}
	}
}
