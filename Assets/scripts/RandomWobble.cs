using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWobble : MonoBehaviour {


	public float fireDelta = 0.5F;
	private float nextFire = 0.5F;
	private float myTime = 0.0F;


	public float ScalingFactor = 1f;
	private float TimeScale = 2f;
	private float TimeScaleDown = 10f;
	private Vector3 InitialScale;
	private Vector3 FinalScale;


	void Start () {

		InitialScale = transform.localScale;
		FinalScale = new Vector3(InitialScale.x + ScalingFactor, 
		InitialScale.y + ScalingFactor/2,
		InitialScale.z + ScalingFactor);


	}

	// Update is called once per frame
	void Update () {

		myTime = myTime + Time.deltaTime;

		if (Input.GetButton("Fire1") && myTime > nextFire)
		{
			nextFire = myTime + fireDelta;

			StartCoroutine (LerpUp ());

			nextFire = nextFire - myTime;
			myTime = 0.0F;
		}
			
	}

	IEnumerator LerpUp(){
		float progress = 0;


		while(progress <= 1){
			transform.localScale = Vector3.Slerp(InitialScale, FinalScale,  Mathf.SmoothStep(0.0f, 1.0f, progress));
			progress += Time.deltaTime * TimeScale;
			yield return null;
		}
		transform.localScale = FinalScale;
		StartCoroutine (LerpDown ());

	} 

	IEnumerator LerpDown(){
		float progress = 0;

		while(progress <= 1){
			transform.localScale = Vector3.Slerp(FinalScale, InitialScale,  Mathf.SmoothStep(0.0f, 1.0f, progress));
			progress += Time.deltaTime * TimeScaleDown;
			yield return null;
		}
		transform.localScale = InitialScale;


	} 


}
