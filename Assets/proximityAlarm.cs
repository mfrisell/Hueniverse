using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proximityAlarm : MonoBehaviour {

    private GameObject asteroid;
    private Vector3 asteroidPos;
    private GameObject head;
    private Vector3 headPos;
    private Vector3 headDir;

	// Use this for initialization
	void Start () {
        asteroid = gameObject;
		head = GameObject.FindGameObjectWithTag("MainCamera");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //asteroidPos = asteroid.transform.position
        asteroidPos = asteroid.transform.GetChild(0).position;
        headPos = head.transform.position;
        headDir = head.transform.forward;

        Debug.Log("Asteroid | x: " + asteroidPos.x.ToString() + "y: " + asteroidPos.y.ToString() + "z: " + asteroidPos.z.ToString());
        Debug.Log("Head | x: " + headPos.x.ToString() + "y: " + headPos.y.ToString() + "z: " + headPos.z.ToString());

        //Debug.Log("The angle approach angle is: " + Vector3.Angle(head.transform.forward, asteroid.transform.position - head.transform.position).ToString());

        if ((asteroidPos - headPos).magnitude < 1 && Vector3.Angle(headDir, asteroidPos - headPos) > 45);
        {
            Debug.Log("NU jÄVLAR SMÄLLER DET");
        }
	}
}
