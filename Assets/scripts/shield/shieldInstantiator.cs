﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldInstantiator : MonoBehaviour {

    // Use this for initialization


    private int currentControllerHitBoxIndex;
    private int firstHitBoxIndex;
    private bool clockWise;
    private int directionMultiplier;
    private int hitBoxHitCounter = 0;
    private int numberOfHitBoxes = 6;

    public GameObject shieldPrefab;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}         

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("banan");

        if (other.tag == "shieldHitBox") {
            //Debug.Log("apple");
            if (hitBoxHitCounter == 0)
            {


                currentControllerHitBoxIndex = other.gameObject.GetComponent<shieldIndex>().index;
                firstHitBoxIndex = currentControllerHitBoxIndex;
                Debug.Log("first Hitbox " + currentControllerHitBoxIndex.ToString());
                hitBoxHitCounter += 1;
            }

            else if (hitBoxHitCounter == 1)
            {

                Debug.Log(Mod(currentControllerHitBoxIndex - 1, numberOfHitBoxes));

                //Om 
                if ((other.gameObject.GetComponent<shieldIndex>().index == Mod(currentControllerHitBoxIndex + 1, numberOfHitBoxes)) || (other.gameObject.GetComponent<shieldIndex>().index == Mod(currentControllerHitBoxIndex - 1, numberOfHitBoxes)))
                {
                    if (other.gameObject.GetComponent<shieldIndex>().index == Mod(currentControllerHitBoxIndex + 1, numberOfHitBoxes))
                    {
                        clockWise = true;
                        directionMultiplier = 1;
                        Debug.Log("Clockwise: " + currentControllerHitBoxIndex.ToString());
                    }
                    else if (other.gameObject.GetComponent<shieldIndex>().index == Mod(currentControllerHitBoxIndex - 1, numberOfHitBoxes))
                    {
                        clockWise = false;
                        directionMultiplier = -1;
                        Debug.Log("Counter clockwise: " + currentControllerHitBoxIndex.ToString());
                    }
                    hitBoxHitCounter += 1;
                    currentControllerHitBoxIndex = Mod(currentControllerHitBoxIndex + directionMultiplier, numberOfHitBoxes);
                    Debug.Log("Second Hitbox: " + currentControllerHitBoxIndex.ToString());
                }

            }

            else if (hitBoxHitCounter == numberOfHitBoxes)
            {
                Debug.Log("instantiate shield!!!");
                GameObject shieldHolder = GameObject.FindGameObjectWithTag("shieldHolder");
                foreach (Transform child in shieldHolder.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                //Vector3 pos = new Vector3(shieldHolder.transform.position.x, shieldHolder.transform.position.y, shieldHolder.transform.position.z + 1.5f);
                Vector3 pos = new Vector3(0, 0, 0.5f);
                GameObject shield = Instantiate(shieldPrefab, shieldHolder.transform.position + 1.5f * shieldHolder.transform.forward, shieldHolder.transform.rotation, shieldHolder.transform);

                GameObject gco = GameObject.FindGameObjectWithTag("GameController");
                GameController gc = gco.GetComponent<GameController>();
                gc.shieldActivated = true;

                hitBoxHitCounter = 0;



                //shield.transform.localPosition = pos;
            }
            else
            {

                if (other.gameObject.GetComponent<shieldIndex>().index == (currentControllerHitBoxIndex + directionMultiplier) % numberOfHitBoxes)
                {
                    currentControllerHitBoxIndex = (currentControllerHitBoxIndex + directionMultiplier) % numberOfHitBoxes;
                    Debug.Log("current hitbox " + currentControllerHitBoxIndex.ToString());
                    hitBoxHitCounter++;

                }
                else
                {                                 
                    Debug.Log("wrong index");
                    Debug.Log(other.gameObject.GetComponent<shieldIndex>().index);

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
