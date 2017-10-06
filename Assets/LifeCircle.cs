using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCircle : MonoBehaviour {

	public GameObject LifeCircleObj;
	public GameObject Heart;

	private GameObject go;
	private GameObject goHeart;

	private int lifes;
	private int prevLifes;

	// Use this for initialization
	void Start () {

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		GameController gameControllerScript = gameControllerObject.GetComponent<GameController> ();
		lifes = gameControllerScript.lifes;

		prevLifes = lifes;

		createLifeCircle ();
		
	}
	
	// Update is called once per frame
	void Update () {

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		GameController gameControllerScript = gameControllerObject.GetComponent<GameController> ();
		lifes = gameControllerScript.lifes;

		if (prevLifes != lifes) {
			prevLifes = lifes;
			DestroyAllObjects();
			createLifeCircle ();

			Debug.Log ("förlorade liv");
		}
		
	}

	void createLifeCircle() {

		transform.rotation = Quaternion.identity;
		
		float amountCircleSectors = lifes * 2;
		float fullCircle = 360;
		float heartLength = fullCircle / 10;
		float lineLength = (fullCircle - (lifes * heartLength)) / lifes;

		float standardLength = fullCircle / amountCircleSectors;

		float movedAcrossCircle = -heartLength/2;

		float heartScale = 0.0001f;


		for (int i = 0; i < amountCircleSectors; i++) {


			if (i % 2 == 0) {

				goHeart = Instantiate (Heart, transform.position, transform.rotation) as GameObject;
				goHeart.transform.localScale = new Vector3(heartScale, heartScale, 1);
				goHeart.transform.SetParent(GetComponent<Transform>());

				float radius = 0.0215f;
				//float degree = 180;
				float degreeToRadian = (movedAcrossCircle +108) * 0.01745329252f;

				float x = radius * Mathf.Cos (degreeToRadian);
				float y = radius * Mathf.Sin (degreeToRadian);

				Vector3 pos = transform.position;
				pos.x += x;
				pos.y += y;

				goHeart.transform.position = pos;

				movedAcrossCircle += heartLength;
			} else {

				go = Instantiate (LifeCircleObj, transform.position, transform.rotation) as GameObject;
				go.transform.localScale = new Vector3(0.0025f, 0.0025f, 1);
				go.transform.SetParent(GetComponent<Transform>());

				Image img = go.GetComponent<Image> ();

				img.fillAmount = lineLength/fullCircle;
				movedAcrossCircle += lineLength;
				go.transform.Rotate(Vector3.forward * movedAcrossCircle );
			}



		}
	}

	void DestroyAllObjects()
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("lifeCirclePart");

		for(var i = 0 ; i < gameObjects.Length ; i ++)
		{
			Destroy(gameObjects[i]);
		}
	}
}
