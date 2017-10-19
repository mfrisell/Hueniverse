using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class NavigateShip : MonoBehaviour {
    public GameObject cameraRig;
	public GameObject cameraHead;
	public GameObject spaceShip;

	void Update()
	{
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
}



