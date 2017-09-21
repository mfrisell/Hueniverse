using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;

	void OnTriggerEnter(Collider other) {

		if (gameObject.tag == other.tag) {
			// Create Explosion
			Instantiate (explosion, transform.position, transform.rotation);

			// Update player score
			GameObject go = GameObject.FindGameObjectWithTag ("GameController");
			GameController gco = go.GetComponent<GameController> ();
			gco.score += 10;

			// Hide mesh
			MeshRenderer mesh = GetComponent<MeshRenderer> ();
			mesh.enabled = false;

			// Fade out audio
			StartCoroutine (fadeAudio ());
		}
	}

	IEnumerator fadeAudio() {
		
		for (int i = 0; i < 10; i++) {
			AudioSource audio = GetComponent<AudioSource> ();
			Debug.Log (audio.volume);
			audio.volume -= 0.02f;
			yield return new WaitForSeconds (0.05f);
		}

		// Remove gameobject from scene
		Destroy (gameObject);
	}
}
