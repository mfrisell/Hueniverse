using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kino;

public class LifeCircle : MonoBehaviour {

	public GameObject LifeCircleObj;
	public GameObject Heart;

	private GameObject go;
	private GameObject goHeart;

	private int lifes;
	private int prevLifes;

	public GameObject agObj;

	public GameObject hud;

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

			StartCoroutine (GlitchScreen());
			StartCoroutine (ChangeLife());
		}
		
	}

	void createLifeCircle() {

		
		float amountCircleSectors = lifes * 2;
		float fullCircle = 360;
		float heartLength = fullCircle / 10;
		float lineLength = (fullCircle - (lifes * heartLength)) / lifes;

		float standardLength = fullCircle / amountCircleSectors;

		float movedAcrossCircle = -heartLength/2;

		float heartScale = 0.05f;

		float eulerAng = transform.rotation.eulerAngles.z;

		for (int i = 0; i < amountCircleSectors; i++) {


			if (i % 2 == 0) {

				goHeart = Instantiate (Heart, transform.position, transform.rotation) as GameObject;

				float zRotOffset = 360 - eulerAng;
				goHeart.transform.Rotate (new Vector3 (0,0,zRotOffset));

				goHeart.transform.SetParent(GetComponent<Transform>());

				float radius = 9f;
				float degreeToRadian = (movedAcrossCircle +108) * 0.01745329252f;

				float x = radius * Mathf.Cos (degreeToRadian);
				float y = radius * Mathf.Sin (degreeToRadian);

				Vector3 pos = goHeart.transform.localPosition;
				pos.x += x;
				pos.y += y;

				goHeart.transform.localPosition = pos;
				goHeart.transform.localScale = new Vector3(heartScale, heartScale, heartScale);

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

	IEnumerator ChangeLife() {

		rotateAroundAxis raa = transform.GetComponent<rotateAroundAxis> ();
		float inititalSpeed = raa.speed;

		for (int i = 0; i < 20; i++) {
			raa.speed = inititalSpeed * i * i;
			yield return new WaitForSeconds (0.01f);
		}

		DestroyAllObjects();
		createLifeCircle ();

		for (int i = 20; i > 0; i--) {
			raa.speed = inititalSpeed * i * i;
			yield return new WaitForSeconds (0.01f);
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

	IEnumerator GlitchScreen() {

		AnalogGlitch ag = agObj.GetComponent<AnalogGlitch> ();

		float increaseAmount = 0.05f;

		for (int i = 0; i < 5; i++) {
			
			ag.colorDrift += increaseAmount;
			ag.scanLineJitter += increaseAmount;
			yield return new WaitForSeconds (0.05f);
		}

		for (int i = 5; i > 0; i--) {
			ag.colorDrift -= increaseAmount;
			ag.scanLineJitter -= increaseAmount;
			yield return new WaitForSeconds (0.05f);
		}

		ag.colorDrift = 0;
		ag.scanLineJitter = 0;

	}


}
