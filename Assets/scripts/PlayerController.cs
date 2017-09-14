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


    void Start() {

        deviceindex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        previousSwipePosition = 0;
    }

    void Update()
    {

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
                    Debug.Log(colorProgress);
                    Debug.Log(swipeDirection);
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
