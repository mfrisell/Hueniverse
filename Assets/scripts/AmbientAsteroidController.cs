using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAsteroidController : MonoBehaviour {

	private float waitBetweenSpawns;
	private float ambientSpeed;
	private float ambientSpawnNumber;
	private float zRange;
	private float zCone;
	private float ambientSize;

	public GameObject AmbientAsteroid;

	// Use this for initialization
	void Start () {

		StartCoroutine(SpawnAmbient());

		//Debug.Log (Random.insideUnitCircle * 10);

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	IEnumerator SpawnAmbient() {

		while (true) {

			ambientSpawnNumber = Random.Range (20, 50);
			zRange = 1000;
			zCone = 200;
			ambientSize = Random.Range (10, 200);


			for (int i = 0; i < ambientSpawnNumber; i++) {

				Vector3 randomStartPos = Random.insideUnitCircle * zCone;
				Vector3 startPos = new Vector3 (randomStartPos.x, randomStartPos.y, zRange);

				ambientSpeed = Random.Range (100, 400);

				GameObject ambas = Instantiate (AmbientAsteroid, startPos, Quaternion.identity) as GameObject;
                ambas.transform.SetParent(GetComponent<Transform>());
                ambas.transform.localScale = new Vector3 (ambientSize,ambientSize,ambientSize);
				Rigidbody ambasRigid = ambas.GetComponent<Rigidbody> ();
				ambasRigid.velocity = -transform.forward * ambientSpeed;

			}

			waitBetweenSpawns = Random.Range (1,3);
			yield return new WaitForSeconds (waitBetweenSpawns);


		}

	
	}
}
