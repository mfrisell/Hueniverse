using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundCircle : MonoBehaviour {

	public GameObject square;

	private Color colorLeft;
	private Color colorRight;
	private Color otherColor;

	private Color colorLeftPrev;
	private Color colorRightPrev;

	private Color[] colors = {new Color (1, 0, 0, 1),new Color (0, 1, 0, 1),new Color (0, 0, 1, 1)};

	private bool createdCircles = false;

	// Use this for initialization
	void Start () {

		colorLeft = Color.red;
		colorRight = Color.green;

		colorLeftPrev = colorLeft;
		colorRightPrev = colorRight;

		foreach (Color col in colors)
		{
			if (col != colorLeft && col != colorRight) {
				otherColor = col;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		GameObject playerControllerObject = GameObject.FindGameObjectWithTag ("Player");
		PlayerController playerControllerScript = playerControllerObject.GetComponent<PlayerController> ();
		colorLeft = playerControllerScript.leftCurrentColor;
		colorRight = playerControllerScript.rightCurrentColor;

		foreach (Color col in colors)
		{
			if (col != colorLeft && col != colorRight) {
				otherColor = col;
			}
		}

		if (colorLeftPrev != colorLeft || colorRightPrev != colorRight) {
			createdCircles = true;
			DestroyAllObjects ();
			CreateCircle ();

			colorLeftPrev = colorLeft;
			colorRightPrev = colorRight;
		}
		
	}

	void CreateCircle() {

		Color[] colorsInOrder = CreateColorArray ();
		//Debug.Log (colorsInOrder);

		float movedAcrossCircle = -30;

		for (int i = 0; i < 12; i++) {
			GameObject sq = Instantiate (square, transform.position, transform.rotation) as GameObject;

			sq.transform.localScale = new Vector3(0.0025f, 0.0025f, 1);
			sq.transform.SetParent(GetComponent<Transform>());

			Image sqImg = sq.GetComponent<Image> ();

			sqImg.color = colorsInOrder[i];

			movedAcrossCircle += 30;
			sq.transform.Rotate(Vector3.forward * movedAcrossCircle );

		}
	}

	Color[] CreateColorArray() {

		Color colOne = otherColor;
		Color colTwo = colorLeft;

		Color[] colArr = { Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white };

		int j = 0;
		for (int i = 0; i < 12; i++) {

			switch (i)
			{
			case 4:
				colOne = colorLeft;
				colTwo = colorRight;
				break;
			case 8:
				colOne = colorRight;
				colTwo = otherColor;
				break;
			default:
				break;
			}

			if (j >= 4) {
				j = 0;
			}

			Color lerpedColor = Color.white;

			float alpha = 0.2f;

			switch (j)
			{
			case 0:
				lerpedColor = colOne;
				break;
			case 1:
				lerpedColor = (colOne + colOne + colTwo) / 2;
				break;
			case 2:
				lerpedColor = Color.Lerp (colOne, colTwo, 0.5f) * 2;
				break;
			case 3:
				lerpedColor = (colOne + colTwo + colTwo) / 2;
				break;
			default:
				break;
			}

			lerpedColor.a = alpha;
				
			colArr [i] = lerpedColor;

			j++;


		}

		return colArr;
	}

	void DestroyAllObjects()
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("colorSquare");

		for(var i = 0 ; i < gameObjects.Length ; i ++)
		{
			Destroy(gameObjects[i]);
		}
	}
}
