using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongTarget : MonoBehaviour {

	public GameObject failExplosion;
	public bool removeObject = true;
	private bool exploded = false;

	// Use this for initialization
	void Start () {

		Debug.Log (removeObject);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {

		if (!exploded || !removeObject) {
			exploded = true;

			if (gameObject.tag != other.tag) {
			
				Instantiate (failExplosion, gameObject.transform.position, gameObject.transform.rotation);

				// Hide bullet mesh
				if (removeObject) {
					MeshRenderer mesh = GetComponent<MeshRenderer> ();
					mesh.enabled = false;
				}

				// Fade out audio and then destroy bullet
				StartCoroutine (fadeAudio ());
			} 
		}
	}

	IEnumerator fadeAudio() {
		//AudioSource audio = GetComponent<AudioSource> ();

		for (int i = 0; i < 10; i++) {
			//Debug.Log (audio.volume);
			//audio.volume -= 0.02f;
			yield return new WaitForSeconds (0.05f);
		}

		// audio.volume = 0;
		// Remove gameobject from scene

		if (removeObject) {
			Destroy (gameObject);
		}
	}
}
