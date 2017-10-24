using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GetHit : MonoBehaviour {

	private int leftDeviceIndex;
	private int rightDeviceIndex;
	private AudioSource audio;

	private float gameTimer = 0f;
	private float protectionTimer = 3f;

	void Start() {
		leftDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
		rightDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
		audio = GetComponent<AudioSource> ();
	}

	void Update() {
		gameTimer += Time.deltaTime;
	}

    void OnTriggerEnter (Collider other)
    {

		GameObject go = GameObject.FindGameObjectWithTag("GameController");
		GameController gco = go.GetComponent<GameController>();
        DestroyByContact dbc = other.GetComponent<DestroyByContact>();
        

		if ((gameTimer > protectionTimer) && !gco.gameOver && !dbc.exploded) {
			gameTimer = 0f;

			audio.Play ();

			gco.lifes -= 1;

            if (leftDeviceIndex != -1)
			    SteamVR_Controller.Input (leftDeviceIndex).TriggerHapticPulse(2000);
            if (rightDeviceIndex != -1)
			    SteamVR_Controller.Input (rightDeviceIndex).TriggerHapticPulse(2000);

		}
    }
}
