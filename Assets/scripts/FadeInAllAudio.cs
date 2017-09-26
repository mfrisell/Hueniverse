using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAllAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {

		AudioListener.volume = 0;
		StartCoroutine (FadeInAudio ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator FadeInAudio() {

		for (float i = 0; i < 1; i += 0.01f) {
			AudioListener.volume = i;
			yield return new WaitForSeconds (0.25f * Time.deltaTime);
		}

		AudioListener.volume = 1;
		
	}
}
