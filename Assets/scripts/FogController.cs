using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {

	private float waitBetweenSpawns;
	private float ambientSpeed;
	private float ambientSpawnNumber;
	private float zRange;
	private float zCone;
	private float ambientSize;

	public GameObject Fog;

	// Use this for initialization
	void Start () {

		StartCoroutine(SpawnFog());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SpawnFog() {

		while (true) {

			ambientSpawnNumber = Random.Range (1, 2);
			zRange = 1000;
			zCone = 200;
			ambientSize = Random.Range (50, 200);

			for (int i = 0; i < ambientSpawnNumber; i++) {

				Vector3 randomStartPos = Random.insideUnitCircle * zCone;
				Vector3 startPos = new Vector3 (randomStartPos.x, randomStartPos.y, zRange);

				ambientSpeed = Random.Range (100, 120);

				GameObject ambas = Instantiate (Fog, startPos, Quaternion.identity) as GameObject;
				//Debug.Log (ambas);
				ambas.transform.SetParent(GetComponent<Transform>());
				ambas.transform.localScale = new Vector3 (ambientSize,ambientSize,ambientSize);
				Rigidbody ambasRigid = ambas.GetComponent<Rigidbody> ();
				ambasRigid.velocity = -transform.forward * ambientSpeed;

			}

			waitBetweenSpawns = Random.Range (6,20);
			yield return new WaitForSeconds (waitBetweenSpawns);


		}


	}
}
