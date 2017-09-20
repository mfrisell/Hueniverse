using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour {

	public Color setColorLeft;
	public Color activeColorLeft;

	public Color setColorRight;
	public Color activeColorRight;

    public float colorProgress;

    public float health;
    public float ammunition;

    public int deviceindex;

    private float currentSwipePosition;
    private float previousSwipePosition;

    private float addedTime = 0f;
    private bool updateColorProgress = false;
    private float colorProgressDelta = 0f;

    private string swipeDirection = "";

    // Rotate claws

    public float rotationSpeed = 10.0f;
    private float degree = 120.0f;
    private Quaternion targetRotation;
    public GameObject clawsLeft;
    public GameObject handleLeft;


    void Start() {

        deviceindex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        previousSwipePosition = 0;

        targetRotation = clawsLeft.transform.rotation;
    }

    void Update()
    {

        //if (deviceindex != -1 && SteamVR_Controller.Input(deviceindex).GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        //{

        //    var axisPress = SteamVR_Controller.Input(deviceindex).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
        //    var xAxis = axisPress[0];

        //    var handleVector = handleLeft.transform.rotation;

        //    if (xAxis>0)
        //    {
        //        Debug.Log("Höger tryck");
        //        // Rotera åt höger

                
        //        //targetRotation *= Quaternion.AngleAxis(degree, handleVector);
        //        targetRotation *= handleVector;

        //    } else
        //    {
        //        Debug.Log("Vänster tryck");
        //        // Rotera åt vänster
        //        //targetRotation *= Quaternion.AngleAxis(-degree, handleVector);

        //    }
 

        //}

        //clawsLeft.transform.rotation = Quaternion.Lerp(clawsLeft.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (deviceindex != -1 && SteamVR_Controller.Input(deviceindex).GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {

            var axis = SteamVR_Controller.Input(deviceindex).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

            currentSwipePosition = axis[0];
            addedTime += Time.deltaTime;

            if (previousSwipePosition != currentSwipePosition)
            {
                if (previousSwipePosition > currentSwipePosition)
                {
                
                    colorProgressDelta += (-currentSwipePosition + 1) / 2;
                    swipeDirection = "left";

                }
                else
                {

                    colorProgressDelta += (currentSwipePosition + 1) / 2;
                    swipeDirection = "right";

                }

                if (updateColorProgress)
                {
                    colorProgress = (colorProgressDelta/addedTime)/60;
                    colorProgressDelta = 0f;
                    addedTime = 0;

                    updateColorProgress = false;
                    //Debug.Log(colorProgress);
                    //Debug.Log(swipeDirection);
                }

            }

            if (addedTime > 0.1)
            {
                updateColorProgress = true;
            }

            
            //Debug.Log(currentSwipePosition);


            previousSwipePosition = currentSwipePosition;
        } else
        {
            addedTime = 0f;
            colorProgressDelta = 0f;
            updateColorProgress = false;
        }

    }

    public static float map01(float value, float min, float max)
    {
        return (value - min) * 1f / (max - min);
    }

    public static float superLerp(float from, float to, float from2, float to2, float value)
    {
        if (value <= from2)
            return from;
        else if (value >= to2)
            return to;
        return (to - from) * ((value - from2) / (to2 - from2)) + from;
    }

}
