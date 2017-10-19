using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class NavigateShip : MonoBehaviour {
<<<<<<< HEAD
=======
    public GameObject cameraRig;
>>>>>>> shieldGesture
	public GameObject cameraHead;
	public GameObject spaceShip;

	void Update()
	{
<<<<<<< HEAD
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
=======
        spaceShip.transform.rotation = Quaternion.Euler(new Vector3(0,0, cameraHead.transform.rotation.z*180/3.14f));
        if (cameraHead.transform.rotation.z * 180 / 3.14 < -5 || Input.GetKeyDown("d"))
        {
            //Debug.Log(spaceShip.transform.position.x);
            if (spaceShip.transform.position.x < 2.5f)
            {
                spaceShip.transform.position = new Vector3(spaceShip.transform.position.x - cameraHead.transform.rotation.z * 0.1f, -0.3f, -0.5f);
                cameraRig.transform.position = new Vector3(spaceShip.transform.position.x, 0, 0);
            }
        }
        else if (cameraHead.transform.rotation.z * 180 / 3.14 > 5 || Input.GetKeyDown("a"))
        {
            if (spaceShip.transform.position.x > -2.5f)
            {
                spaceShip.transform.position = new Vector3(spaceShip.transform.position.x - cameraHead.transform.rotation.z * 0.1f, -0.3f, -0.5f);
                cameraRig.transform.position = new Vector3(spaceShip.transform.position.x, 0, 0);
            }
        }
    }    
>>>>>>> shieldGesture
}



