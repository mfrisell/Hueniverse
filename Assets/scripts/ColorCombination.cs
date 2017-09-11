using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCombination : MonoBehaviour {

	private GameObject weaponLeftObject;
	private GameObject weaponRightObject;

	// Use this for initialization
	void Start () {

		weaponLeftObject = GameObject.FindGameObjectWithTag("weaponLeft");
		weaponRightObject = GameObject.FindGameObjectWithTag("weaponRight");
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 posLeftWeapon = weaponLeftObject.transform.position;
		Vector3 posRightWeapon = weaponRightObject.transform.position;
		float distanceBetweenWeapons = (posLeftWeapon - posRightWeapon).magnitude;


	}
}
