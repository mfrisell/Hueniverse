using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometScript : MonoBehaviour {

	private Rigidbody cometRigid;

	// Use this for initialization
	void Start () {
		cometRigid = this.GetComponent<Rigidbody> ();
		Vector3 velVector = new Vector3(10,0,0);
		cometRigid.velocity = velVector;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// public void TurnOff () {
		// if (!destroying && shot) {
			// destroying = true;
			// particleSystem.Stop ();
// //			transform.parent = null;
			// StartCoroutine (FadeOut ());
		// }
	// }
		
	// IEnumerator FadeOut () {

		// int numParticles = particleSystem.GetParticles (particles);
		// Color testColor = particles [0].GetCurrentColor (particleSystem);

		// while (trail.startWidth > 0 || testColor.a > 0) {
			// float amount = Time.deltaTime / fadeOutTime;

			// trail.startWidth -= amount*5f;
			// Mathf.Clamp (trail.startWidth, 0f, trail.startWidth);

			// trail.time -= amount*5f;
			// Mathf.Clamp (trail.time, 0f, trail.time);

			// for (int p = 0; p < numParticles; p++) {
				// testColor = particles [p].GetCurrentColor (particleSystem);

				// testColor.a -= amount;
				// particles [p].startColor = testColor;
			// }

			// particleSystem.SetParticles (particles, numParticles);
			// yield return null;
		// }

		// Destroy (this.gameObject);
	// }
}