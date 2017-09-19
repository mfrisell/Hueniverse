using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
//		Destroy (other.gameObject);
//		Destroy (gameObject);

		if (gameObject.tag == other.tag) {
			Destroy (gameObject);
			GameObject go = GameObject.FindGameObjectWithTag ("GameController");
			GameController gco = go.GetComponent<GameController> ();
			gco.score += 10;
		}
	}
}
