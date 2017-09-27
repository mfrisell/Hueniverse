using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
	public float scaleTime = 0.1f;
	public float scaleFactor = 1f;

	// Use this for initialization
	void Start ()
	{

		StartCoroutine (Scale ());
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


	IEnumerator Scale ()
	{

		for (int i = 0; i <= 100; i+=5) {

			transform.localScale = new Vector3 (i * scaleFactor, i * scaleFactor, i * scaleFactor);
			yield return new WaitForSeconds (scaleTime * Time.deltaTime);

		}
	}
}
