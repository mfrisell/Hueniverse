using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCombination : MonoBehaviour {

	private GameObject LeftObject;
	private GameObject RightObject;
	public float maxDistance = 0.5f;
	public float minDistance = 0.1f;

	private Color lerpedColorLeft;
	private Color lerpedColorRight;

	private Color colorLeft;
	private Color colorRight;
	private Color combinedColor;

	public Transform shotSpawnCenter;

//	private bool firstTimeConvert = true;

	// Use this for initialization
	void Start () {

		LeftObject = GameObject.FindGameObjectWithTag("ShotSpawnLeft");
		RightObject = GameObject.FindGameObjectWithTag("ShotSpawnRight");

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 posLeftWeapon = LeftObject.transform.position;
		Vector3 posRightWeapon = RightObject.transform.position;

		Quaternion rotLeftWeapon = LeftObject.transform.rotation;
		Quaternion rotRightWeapon = RightObject.transform.rotation;
		float distanceBetweenWeapons = (posLeftWeapon - posRightWeapon).magnitude;

		GameObject childGameObjectLeft = GameObject.FindGameObjectWithTag ("CircleLeft");
		GameObject childGameObjectRight = GameObject.FindGameObjectWithTag ("CircleRight");

		ColorChangeLeft ccLeft = childGameObjectLeft.GetComponent<ColorChangeLeft> ();
		ColorChangeRight ccRight = childGameObjectRight.GetComponent<ColorChangeRight> ();

		ParticleSystem psLeft = childGameObjectLeft.GetComponent<ParticleSystem> ();
		ParticleSystem psRight = childGameObjectRight.GetComponent<ParticleSystem> ();

		if (distanceBetweenWeapons < maxDistance) {

			// Hämta ursprungsfärger
			colorLeft = ccLeft.startColorLeft;
			colorRight = ccRight.startColorRight;

			// Mappa om distance mellan vapnen till 0->1
			float distanceProgress = map01 (distanceBetweenWeapons, maxDistance, minDistance);

			// Ta reda på slutgiltig färg
			combinedColor = (colorLeft + colorRight) / 2;
			
			// Börja lerpa färgen mot den slutgiltiga
			lerpedColorLeft = Color.Lerp (colorLeft, combinedColor, distanceProgress);
			lerpedColorRight = Color.Lerp (colorRight, combinedColor, distanceProgress);

			// Uppdatera partikelsystemet
			var mainLeft = psLeft.main;
			var mainRight = psRight.main;
			mainLeft.startColor = lerpedColorLeft;
			mainRight.startColor = lerpedColorRight;


			// Create spawn point for projectile
			Vector3 middle = Vector3.Lerp (posLeftWeapon, posRightWeapon, 0.5f);
			Quaternion middleRot = Quaternion.Slerp(rotLeftWeapon, rotRightWeapon, 0.5f);

			shotSpawnCenter.transform.position = middle;
			shotSpawnCenter.transform.rotation = middleRot;

			// Update colortags
			if ((ccLeft.chosenColorLeft == "red" || ccLeft.chosenColorLeft == "blue") && (ccRight.chosenColorRight == "red" || ccRight.chosenColorRight == "blue")) {
				if (ccLeft.chosenColorLeft != ccRight.chosenColorRight) {

					ccLeft.chosenColorLeft = "magenta";
					ccRight.chosenColorRight = "magenta";

					// do shizz
					Debug.Log ("Magenta");
				}
			} else if ((ccLeft.chosenColorLeft == "red" || ccLeft.chosenColorLeft == "green") && (ccRight.chosenColorRight == "red" || ccRight.chosenColorRight == "green")) {
				if (ccLeft.chosenColorLeft != ccRight.chosenColorRight) {
					
					ccLeft.chosenColorLeft = "yellow";
					ccRight.chosenColorRight = "yellow";
					// do shizz
					Debug.Log ("Yellow");
				}
			} else if ((ccLeft.chosenColorLeft == "blue" || ccLeft.chosenColorLeft == "green") && (ccRight.chosenColorRight == "blue" || ccRight.chosenColorRight == "green")) {
				if (ccLeft.chosenColorLeft != ccRight.chosenColorRight) {

					ccLeft.chosenColorLeft = "cyan";
					ccRight.chosenColorRight = "cyan";
					// do shizz
					Debug.Log ("Cyan");
				}
			}
 
		} else {

			shotSpawnCenter.transform.position = new Vector3 (0, 0, 0);
		}

	}

	public static float map01( float value, float min, float max )  
	{
		return ( value - min ) * 1f / ( max - min );
	}

}
