using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class NavigateShip : MonoBehaviour {
	public GameObject cameraHead;
	public GameObject spaceShip;

	void Update()
	{
		spaceShip.transform.rotation = cameraHead.transform.rotation;
		if (cameraHead.transform.rotation.z < -10 || Input.GetKeyDown ("a")) {
			Debug.Log ("Pressing a");
			if(spaceShip.transform.position.x > -8)
				spaceShip.transform.position = new Vector3 (spaceShip.transform.position.x - 1, -0.3, 0.5);
		}
		if (cameraHead.transform.rotation.z > 10 || Input.GetKeyDown ("d")) {
			Debug.Log ("Pressing d");
			if(spaceShip.transform.position.x < 8)
				spaceShip.transform.position = new Vector3 (spaceShip.transform.position.x + 1, -0.3, 0.5);
		}
	}
}



