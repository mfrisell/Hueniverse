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
    public AsteroidManager asMan;
    public bool tutorialOff = false;
    public float totalPausedTime = 0;
    

    public Image image;
    public Sprite controlSprite;
    public Sprite baseSprite;
    public Sprite combinedSprite;
    public Sprite shieldSprite;
    public bool isPaused = false;

    //Private
    private bool baseShown = false;
    private bool controlsShown = false;
    private bool combinedShown = false;
    private bool shieldShown = false;

    private int leftDeviceIndex;
    private int rightDeviceIndex;
    private float pauseTime = 0;

    

	// Use this for initialization
	void Start () {
        //Might be better to make these public somewhere and get the index from there
        leftDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        rightDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        image.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Change to check if paused instead of "space"
        if (Time.realtimeSinceStartup - pauseTime > 2 && isPaused && ((leftDeviceIndex != -1 && SteamVR_Controller.Input(leftDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) || (rightDeviceIndex != -1 && SteamVR_Controller.Input(rightDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))))
        {
            Debug.Log(controlsShown);
            Unpause();
        }
    }

    void Pause ()
    {
        if (tutorialOff)
            return;
        isPaused = true;
        image.enabled = true;
        pauseTime = Time.realtimeSinceStartup;
        //TODO Hide controllers
    }

    void Unpause ()
    {
        totalPausedTime += Time.realtimeSinceStartup - pauseTime;
        isPaused = false;
        image.enabled = false;
        pauseTime = 0;
        //TODO Unhide controllers
    }

    public void ShowControls ()
    {
        asMan.launchAsteroid(Color.red, 0, 0);
        controlsShown = true;
        image.sprite = controlSprite;
        Pause();
    }

    public void ShowBaseColor ()
    {
        baseShown = true;
        image.sprite = baseSprite;
        Pause();
    }

    public void ShowCombinedColor ()
    {
        combinedShown = true;
        image.sprite = combinedSprite;
        Pause();
    }

    public void ShowShield ()
    {
        shieldShown = true;
        image.sprite = shieldSprite;
        Pause();
    }

    public bool getControlBool ()
    {
        return controlsShown;
    }

    public bool getBaseBool()
    {
        return baseShown;
    }

    public bool getCombinedBool()
    {
        return combinedShown;
    }

    public bool getShieldBool()
    {
        return shieldShown;
    }
}
