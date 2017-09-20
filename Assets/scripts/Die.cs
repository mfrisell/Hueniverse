using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {

	public float timeToDeath = 7f;

	// Use this for initialization
	void Start () {
		
		Destroy(this.gameObject, timeToDeath);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
