using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RotateWeapon : MonoBehaviour {
    public Transform parentTrans;

    private bool clockwise;

    private int direction;
    private int deviceindex;

    // Use this for initialization
    void Start() {
        deviceindex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
    }

    // Update is called once per frame
    void Update() {

        if (deviceindex != -1 && SteamVR_Controller.Input(deviceindex).GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {

            var axisPress = SteamVR_Controller.Input(deviceindex).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            var xAxis = axisPress[0];

            if (xAxis > 0)
            {
                Debug.Log("Höger tryck");
                clockwise = true;
            }
            else
            {
                Debug.Log("Vänster tryck");
                clockwise = false;
            }

            StartCoroutine(LerpRotation());
        }
    }

    private IEnumerator LerpRotation() {

        float timeToRotate = 1 / 30;

        if(clockwise)
        {
            direction = -4;
        } else
        {
            direction = 4;
        }

        for (var i = 0; i< 30; i++)
        {
         transform.RotateAround(parentTrans.position, parentTrans.up, direction);
            
            yield return new WaitForSeconds(.01f);
        }

        Debug.Log("time to rotate");
        Debug.Log(timeToRotate);
       

        
    }
}
