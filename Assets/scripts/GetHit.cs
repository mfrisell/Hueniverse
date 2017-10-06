using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GetHit : MonoBehaviour {

	private int leftDeviceIndex;
	private int rightDeviceIndex;
	private AudioSource audio;

	void Start() {
		leftDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
		rightDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
		audio = GetComponent<AudioSource> ();
	}

    void OnTriggerEnter (Collider other)
    {
		audio.Play ();
		SteamVR_Controller.Input (leftDeviceIndex).TriggerHapticPulse(2000);
		SteamVR_Controller.Input (rightDeviceIndex).TriggerHapticPulse(2000);

        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        GameController gco = go.GetComponent<GameController>();
		gco.lifes -= 1;
//        gco.score -= 50;
//        if (gco.score < 0)
//            gco.score = 0;
    }
}
