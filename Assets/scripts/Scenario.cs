using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Handles the tutorial scenario, showing slides etc
 */
public class Scenario : MonoBehaviour {

    //Public
    public GameController gameControllerScript;

    //Private
    private int leftDeviceIndex;
    private int rightDeviceIndex;

    private bool isPaused = false;

	// Use this for initialization
	void Start () {
        //Might be better to make these public somewhere and get the index from there
        leftDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        rightDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);

    }
	
	// Update is called once per frame
	void Update () {

        if (Time.timeScale == 0)
        {
            Pause();
        }

        if ((leftDeviceIndex != -1 && SteamVR_Controller.Input(leftDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) || (rightDeviceIndex != -1 && SteamVR_Controller.Input(rightDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger)))
        {
            
        }
    }

    void Pause ()
    {

    }

    void ShowControls ()
    {

    }

    void ShowBaseColor ()
    {

    }

    void ShowCombinedColor ()
    {

    }

    void ShowShield ()
    {

    }
}
