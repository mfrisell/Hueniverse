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

    //Private
    AsteroidManager asteroidManager;
    GameObject redAsteroid;
    GameObject greenAsteroid;
    GameObject blueAsteroid;
    GameObject cyanAsteroid;
    GameObject magentaAsteroid;
    GameObject yellowAsteroid;


    bool stageOneDone = false;
    bool stageTwoDone = false;
    bool stageTwoLoaded = false;
    


    // Use this for initialization
    void Start () {
        asteroidManager = asteroidManagerGO.GetComponent<AsteroidManager>();
        loadStageOne();
    }
	
	// Update is called once per frame
	void Update () {

        if (redAsteroid == null && greenAsteroid == null && blueAsteroid == null && !stageTwoLoaded)
        {
            stageOneDone = true;
            loadStageTwo();
        }

        if (cyanAsteroid == null && magentaAsteroid == null && yellowAsteroid == null && stageOneDone)
        {
            stageTwoDone = true;
            loadStageTwo();
        }

        if(stageTwoDone)
            SceneManager.LoadScene(1, LoadSceneMode.Single);

    }

    private void loadStageOne()
    {
        redAsteroid = asteroidManager.createAsteroid("red", new Vector3(-8, 0, 10));
        greenAsteroid = asteroidManager.createAsteroid("green", new Vector3(0, 0, 10));
        blueAsteroid = asteroidManager.createAsteroid("blue", new Vector3(8, 0, 10));
    }

    private void loadStageTwo()
    {
        cyanAsteroid = asteroidManager.createAsteroid("cyan", new Vector3(-8, 0, 10));
        magentaAsteroid = asteroidManager.createAsteroid("magenta", new Vector3(0, 0, 10));
        yellowAsteroid = asteroidManager.createAsteroid("yellow", new Vector3(8, 0, 10));
        stageTwoLoaded = true;
    }
}
