using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ColorChangeRight : MonoBehaviour {

	public float TimeScale = 2f;
	private Color currentColor = Color.red;
	public Color startColorRight;

	private Color endColor;

	private Color lerpedColor;
	private ParticleSystem ps;

	public string chosenColorRight = "red";

    private int deviceindexLeft;
    private int deviceindexRight;

    private int colorIndex;

    // Use this for initialization
    void Start () {
		startColorRight = currentColor;

		//		gameObject.GetComponentInParent<Script>();

		ps = gameObject.GetComponent<ParticleSystem> ();

        deviceindexLeft = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        deviceindexRight = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);

        colorIndex = 0;
    }

	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown((KeyCode.R))) {
			endColor = Color.red;
			startColorRight = endColor;
			chosenColorRight = "red";
			StartCoroutine(LerpColorRight());
		}

		if (Input.GetKeyDown((KeyCode.G))) {
			endColor = Color.green;
			startColorRight = endColor;
			chosenColorRight = "green";
			StartCoroutine(LerpColorRight());
		}

		if (Input.GetKeyDown((KeyCode.B))) {
			endColor = Color.blue;
			startColorRight = endColor;
			chosenColorRight = "blue";
			StartCoroutine(LerpColorRight());
		}

        if (deviceindexRight != -1 && SteamVR_Controller.Input(deviceindexRight).GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (colorIndex == 0)
            {
                endColor = Color.green;
                startColorRight = endColor;
                chosenColorRight = "green";
                StartCoroutine(LerpColorRight());
                colorIndex++;
            }
            else if (colorIndex == 1)
            {
                endColor = Color.blue;
                startColorRight = endColor;
                chosenColorRight = "blue";
                StartCoroutine(LerpColorRight());
                colorIndex++;
            }
            else if (colorIndex == 2)
            {
                endColor = Color.red;
                startColorRight = endColor;
                chosenColorRight = "red";
                StartCoroutine(LerpColorRight());
                colorIndex = 0;
            }

            Debug.Log(colorIndex);


            var axis = SteamVR_Controller.Input(deviceindexRight).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            //Debug.Log("axis: " + axis);

        }
    }

	private IEnumerator LerpColorRight(){
		float progress = 0;

		while(progress <= 1){
			lerpedColor = Color.Lerp(currentColor, endColor, progress);

			var main = ps.main;
			main.startColor = lerpedColor;

			progress += Time.deltaTime * TimeScale;
			yield return null;
		}

		currentColor = endColor;

	} 
}
