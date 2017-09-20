using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ColorChangeLeft : MonoBehaviour {

	public float TimeScale = 2f;
	private Color currentColor = Color.red;
	public Color startColorLeft;

	private Color endColor;

	private Color lerpedColor;
	private ParticleSystem ps;

	public string chosenColorLeft = "red";

    private int deviceindexLeft;
    private int deviceindexRight;

    private int colorIndex;

    // Use this for initialization
    void Start () {
		startColorLeft = currentColor;

		//		gameObject.GetComponentInParent<Script>();

		ps = gameObject.GetComponent<ParticleSystem> ();

        deviceindexLeft = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        deviceindexRight = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);

        colorIndex = 0;
    }

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown((KeyCode.T))) {
			endColor = Color.red;
			startColorLeft = endColor;
			chosenColorLeft = "red";
			StartCoroutine(LerpColorLeft());
		}

		if (Input.GetKeyDown((KeyCode.H))) {
			endColor = Color.green;
			startColorLeft = endColor;
			chosenColorLeft = "green";
			StartCoroutine(LerpColorLeft());
		}

		if (Input.GetKeyDown((KeyCode.N))) {
			endColor = Color.blue;
			startColorLeft = endColor;
			chosenColorLeft = "blue";
			StartCoroutine(LerpColorLeft());
		}

        if (deviceindexLeft != -1 && SteamVR_Controller.Input(deviceindexLeft).GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if(colorIndex==0)
            {
                endColor = Color.green;
                startColorLeft = endColor;
                chosenColorLeft = "green";
                StartCoroutine(LerpColorLeft());
                colorIndex++;
            } else if(colorIndex==1)
            {
                endColor = Color.blue;
                startColorLeft = endColor;
                chosenColorLeft = "blue";
                StartCoroutine(LerpColorLeft());
                colorIndex++;
            } else if(colorIndex==2)
            {
                endColor = Color.red;
                startColorLeft = endColor;
                chosenColorLeft = "red";
                StartCoroutine(LerpColorLeft());
                colorIndex = 0;
            }

            Debug.Log(colorIndex);


            var axis = SteamVR_Controller.Input(deviceindexLeft).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            Debug.Log("axis: " + axis);

        }
    }

	private IEnumerator LerpColorLeft(){
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
