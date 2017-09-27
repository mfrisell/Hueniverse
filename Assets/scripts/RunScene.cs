using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunScene : MonoBehaviour {
	private string scene;

	void OnTriggerEnter(Collider other) {

		if (gameObject.tag == "start") {
			scene = "main";
		}
		else if(gameObject.tag == "tutorial") {
			scene = "Tutorial";
		}

		StartCoroutine (changeScene (scene));
	}

	// Run chosen scene after 3 seconds
	IEnumerator changeScene(string scene) {
		yield return new WaitForSeconds (3);

		// Run scene
		Application.LoadLevel(scene);
	}
}
