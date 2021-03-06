﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shieldInstantiator : MonoBehaviour {

    // Use this for initialization


    private int currentControllerHitBoxIndex;
    private int firstHitBoxIndex;
    public int directionMultiplier;
    private int hitBoxHitCounter = 0;
    private int numberOfHitBoxes = 12;
    private float fill;
    private float guideTimer = 0f;
    public float maxGuideTimer = 5f;
    private string currentShieldController;
    //private GameObject cameraRig;

    public GameObject shieldPrefab;

	void Start () {
       //cameraRig = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("shieldHolder") != null)
        {
            if (guideTimer < maxGuideTimer)
            {
                guideTimer += Time.deltaTime;
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("shield") == null)
                {
                    Destroy(GameObject.FindGameObjectWithTag("shieldHolder"));
                }
                guideTimer = 0f;
            }
        }

    }         

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentShieldControllerTag + " should be " + this.tag);

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentShieldControllerTag == this.tag && other.tag == "shieldHitBox") {
            GameObject progressShieldCircle = GameObject.FindGameObjectWithTag("progressShieldCircle");
            Image img = progressShieldCircle.GetComponent<Image>();

            if (hitBoxHitCounter == 0)
            {

                currentControllerHitBoxIndex = other.gameObject.GetComponent<shieldIndex>().index;
                firstHitBoxIndex = currentControllerHitBoxIndex;
                hitBoxHitCounter += 1;
                progressShieldCircle.transform.Rotate(0, 0, -(360 / numberOfHitBoxes) * (firstHitBoxIndex));
            }

            else if (hitBoxHitCounter == 1)
            {

                //Om 
                if ((other.gameObject.GetComponent<shieldIndex>().index == Mod(currentControllerHitBoxIndex + 1, numberOfHitBoxes)) || (other.gameObject.GetComponent<shieldIndex>().index == Mod(currentControllerHitBoxIndex - 1, numberOfHitBoxes)))
                {
                    if (other.gameObject.GetComponent<shieldIndex>().index == Mod(currentControllerHitBoxIndex + 1, numberOfHitBoxes))
                    {
                        directionMultiplier = 1;
                        img.fillClockwise = true;
                    }
                    else if (other.gameObject.GetComponent<shieldIndex>().index == Mod(currentControllerHitBoxIndex - 1, numberOfHitBoxes))
                    {
                        directionMultiplier = -1;
                        img.fillClockwise = false;
                    }
                    hitBoxHitCounter += 1;
                    fill = ((1 / (float)numberOfHitBoxes) * (float)hitBoxHitCounter);
                    
                    img.fillAmount = fill;
                    currentControllerHitBoxIndex = Mod(currentControllerHitBoxIndex + directionMultiplier, numberOfHitBoxes);
                    
                }

            }

            else if (hitBoxHitCounter == numberOfHitBoxes)
            {
                GameObject shieldHolder = GameObject.FindGameObjectWithTag("shieldHolder");
                foreach (Transform child in shieldHolder.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                //Vector3 pos = new Vector3(shieldHolder.transform.position.x, shieldHolder.transform.position.y, shieldHolder.transform.position.z + 1.5f);
                Vector3 pos = new Vector3(0, 0, 0.5f);
                GameObject shield = Instantiate(shieldPrefab, shieldHolder.transform.position + 1.5f * shieldHolder.transform.forward, shieldHolder.transform.rotation, shieldHolder.transform);
                shieldRotation sr = shield.GetComponent<shieldRotation>();
                sr.speed = -500 * directionMultiplier;

                GameObject gco = GameObject.FindGameObjectWithTag("GameController");
                GameController gc = gco.GetComponent<GameController>();
                gc.shieldActivated = true;

                hitBoxHitCounter = 0;



                //shield.transform.localPosition = pos;
            }
            else
            {

                if (other.gameObject.GetComponent<shieldIndex>().index == Mod(currentControllerHitBoxIndex + directionMultiplier, numberOfHitBoxes))
                {
                    currentControllerHitBoxIndex = Mod(currentControllerHitBoxIndex + directionMultiplier, numberOfHitBoxes);
                    hitBoxHitCounter++;
                    fill = (1 / (float)numberOfHitBoxes) * (float)hitBoxHitCounter;
                    img.fillAmount = fill;


                }
                else
                {                                 
                    GameObject shieldHolder = GameObject.FindGameObjectWithTag("shieldHolder");
                    GameObject.Destroy(shieldHolder);

                    hitBoxHitCounter = 0;

                    
                }

            }
           

        }
    }

    private int Mod(int a, int b)
    {
        return (a % b + b) % b;
    }
}
