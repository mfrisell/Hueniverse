using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldHolderPosition : MonoBehaviour {

    Vector3 newShipPos, oldShipPos;

	// Use this for initialization
	void Start () {
        oldShipPos = GameObject.FindGameObjectWithTag("spaceShip").transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        newShipPos = GameObject.FindGameObjectWithTag("spaceShip").transform.position;

        //Debug.Log("HALLÅ");

		if (GameObject.FindGameObjectWithTag("shield") == null)
        {
            Vector3 shipMovement = newShipPos - oldShipPos;
            //this.gameObject.transform.position.Set(this.gameObject.transform.position.x + shipMovement.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            this.gameObject.transform.position += shipMovement;
        }
        oldShipPos = newShipPos;
    }
}
