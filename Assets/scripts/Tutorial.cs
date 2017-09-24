using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * Contains the tutorial and level intended for ForskarFredag
 * 
 **/
public class Tutorial : MonoBehaviour {

    //Public
    public GameObject asteroidManagerGO;

    public float gametimeSeconds = 30;

    //Private
    AsteroidManager asteroidManager;
    GameObject redAsteroid;
    GameObject greenAsteroid;
    GameObject yellowAsteroid;
    


    // Use this for initialization
    void Start () {
        //asteroidManager = asteroidManagerGO.GetComponent<AsteroidManager>();
        //redAsteroid = asteroidManager.createAsteroid("red", new Vector3(-4, 0, 4));
        //greenAsteroid = asteroidManager.createAsteroid("green", new Vector3(0, 0, 4));
        //yellowAsteroid = asteroidManager.createAsteroid("yellow", new Vector3(4, 0, 4));
    }
	
	// Update is called once per frame
	void Update () {

        if (redAsteroid == null && greenAsteroid == null && yellowAsteroid == null)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        

    }
}
