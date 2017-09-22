using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RotateWeapon : MonoBehaviour {
    public Transform parentTrans;
    public GameObject handle;

    private bool clockwise;
    public bool isLeft;

    private int direction;
    private int leftDeviceIndex;
    private int rightDeviceIndex;

    // Use this for initialization
    void Start() {
        leftDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        rightDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
    }

    // Update is called once per frame
    void Update() {

        if (leftDeviceIndex != -1 && SteamVR_Controller.Input(leftDeviceIndex).GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {

            var axisPress = SteamVR_Controller.Input(leftDeviceIndex).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
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

            if(isLeft)
                StartCoroutine(LerpRotation());
        }

        if (rightDeviceIndex != -1 && SteamVR_Controller.Input(rightDeviceIndex).GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {

            var axisPress = SteamVR_Controller.Input(rightDeviceIndex).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
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

            if(!isLeft)
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
            handle.transform.RotateAround(parentTrans.position, parentTrans.forward, -direction);
            GetComponent<Transform>().RotateAround(parentTrans.position, parentTrans.forward, direction);
            
            yield return new WaitForSeconds(.01f);
        }

        Debug.Log("time to rotate");
        Debug.Log(timeToRotate);
       

        
    }
}
