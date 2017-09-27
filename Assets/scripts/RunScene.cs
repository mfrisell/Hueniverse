using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunScene : MonoBehaviour {
	private string scene;

	void OnTriggerEnter(Collider other) {

        if (gameObject.tag == "start") {
			scene = "main";
		}
		else if(gameObject.tag == "tutorial") {
			scene = "tutorialScene";
		}

		//StartCoroutine (changeScene (scene));
	}

	// Run chosen scene after 3 seconds
	IEnumerator changeScene(string scene) {
        Debug.Log(scene);
		yield return new WaitForSeconds (1);

        // Run scene
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
