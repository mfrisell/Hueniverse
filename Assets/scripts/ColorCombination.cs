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

	private string chosenColorLeftInit;
	private string chosenColorRightInit;

    public GameObject LeftWeapon;
    public GameObject RightWeapon;

    public GameObject childGameObjectLeft;
    public GameObject childGameObjectRight;

    //	private bool firstTimeConvert = true;

    // Use this for initialization
    void Start () {

        //LeftObject = GameObject.FindGameObjectWithTag("ShotSpawnLeft");
        //RightObject = GameObject.FindGameObjectWithTag("ShotSpawnRight");


    }
	
	// Update is called once per frame
	void Update () {

		//Vector3 posLeftWeapon = LeftObject.transform.position;
		//Vector3 posRightWeapon = RightObject.transform.position;

        Vector3 posLeftWeapon = LeftWeapon.transform.position;
        Vector3 posRightWeapon = RightWeapon.transform.position;

        Quaternion rotLeftWeapon = LeftWeapon.transform.rotation;
		Quaternion rotRightWeapon = RightWeapon.transform.rotation;

		float distanceBetweenWeapons = (posLeftWeapon - posRightWeapon).magnitude;

        //GameObject childGameObjectLeft = GameObject.FindGameObjectWithTag("CircleLeft");
        //GameObject childGameObjectRight = GameObject.FindGameObjectWithTag("CircleRight");

        ColorChangeLeft ccLeft = childGameObjectLeft.GetComponent<ColorChangeLeft>();
        ColorChangeRight ccRight = childGameObjectRight.GetComponent<ColorChangeRight>();

        ParticleSystem psLeft = childGameObjectLeft.GetComponent<ParticleSystem> ();
		ParticleSystem psRight = childGameObjectRight.GetComponent<ParticleSystem> ();

		chosenColorLeftInit = ccLeft.chosenColorLeft;
		chosenColorRightInit = ccRight.chosenColorRight;

		if (distanceBetweenWeapons < maxDistance && ccLeft.chosenColorLeft != ccRight.chosenColorRight) {
            Debug.Log("Cloooose");

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
					ccLeft.chosenColorLeft = "magenta";
					ccRight.chosenColorRight = "magenta";

					Debug.Log ("magenta");
			} else if ((ccLeft.chosenColorLeft == "red" || ccLeft.chosenColorLeft == "green") && (ccRight.chosenColorRight == "red" || ccRight.chosenColorRight == "green")) {
					
					ccLeft.chosenColorLeft = "yellow";
					ccRight.chosenColorRight = "yellow";

					Debug.Log ("yellow");
			} else if ((ccLeft.chosenColorLeft == "blue" || ccLeft.chosenColorLeft == "green") && (ccRight.chosenColorRight == "blue" || ccRight.chosenColorRight == "green")) {

					ccLeft.chosenColorLeft = "cyan";
					ccRight.chosenColorRight = "cyan";

					Debug.Log ("cyan");
			}
 
		} else {
            Debug.Log("Faar away");


            var mainLeft = psLeft.main;
			var mainRight = psRight.main;

			float colorRedLeft = mainLeft.startColor.colorMax [0];
			float colorGreenLeft = mainLeft.startColor.colorMax [1];
			float colorBlueLeft = mainLeft.startColor.colorMax [2];

			float colorRedRight = mainRight.startColor.colorMax [0];
			float colorGreenRight = mainRight.startColor.colorMax [1];
			float colorBlueRight = mainRight.startColor.colorMax [2];

			if (colorRedLeft > colorGreenLeft + colorBlueLeft) {
				ccLeft.chosenColorLeft = "red";
			} else if (colorGreenLeft > colorRedLeft + colorBlueLeft) {
				ccLeft.chosenColorLeft = "green";
			} else if (colorBlueLeft > colorGreenLeft + colorRedLeft) {
				ccLeft.chosenColorLeft = "blue";
			}

			if (colorRedRight > colorGreenRight + colorBlueRight) {
				ccRight.chosenColorRight = "red";
			} else if (colorGreenRight > colorRedRight + colorBlueRight) {
				ccRight.chosenColorRight = "green";
			} else if (colorBlueRight > colorGreenRight + colorRedRight) {
				ccRight.chosenColorRight = "blue";
			}
				
			shotSpawnCenter.transform.position = new Vector3 (0, 0, 0);
		}

	}

	public static float map01( float value, float min, float max )  
	{
		return ( value - min ) * 1f / ( max - min );
	}

}
