using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject failExplosion;
	public bool exploded = false;
	private bool gameOver;
	private GameObject go;
	private GameController gc;

	void Update() {
		
		// Check if game is over
		go = GameObject.FindGameObjectWithTag ("GameController");
		gc = go.GetComponent<GameController> ();
		gameOver = gc.gameOver;

	}

	void OnTriggerEnter(Collider other) {

		if (gameObject.tag == other.tag) {
			// Create Explosion
			if (!exploded && !gameOver) {
				exploded = true;
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
		} else {
		}
	}

	IEnumerator fadeAudio() {
		AudioSource audio = GetComponent<AudioSource> ();

		for (int i = 0; i < 10; i++) {
			//Debug.Log (audio.volume);
			audio.volume -= 0.02f;
			yield return new WaitForSeconds (0.05f);
		}

		audio.volume = 0;
		// Remove gameobject from scene
		Destroy (transform.parent.gameObject);
	}
}
