using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

    public int asteroidSpeed;
    public float asteroidFrequency;
    public int asteroidSpawnAngleWidth;
    public int asteroidSpawnAngleHeight;
    public int asteroidSpawnRadius;
    public bool mixedColors;
    public float asteroidMinSize;
    public float asteroidMaxSize;
    public float asteroidHitRatio;
    public Vector3 gravity;
    //public float asteroidHealth;


    public GameObject asteroid;
    public GameObject target;

    private IEnumerator coroutine;

    Vector3 targetPosition;
	// Use this for initialization
	void Start () {
        targetPosition = new Vector3(0, 0, 0);

        coroutine = GenerateAsteroids(asteroidFrequency);
        StartCoroutine(coroutine);
	}
	
	// Update is called once per frame
	void Update () {

        if (target != null)
        {
            targetPosition = target.transform.position;
        }

        //Physics.gravity = gravity;
        if (Input.GetKeyDown("space"))
            launchAsteroid();

    }


    void launchAsteroid ()
    {
        //Random.Range( 0.0f, 1.0f )
        int asteroidWidthAngle = Random.Range(0, asteroidSpawnAngleWidth) - (asteroidSpawnAngleWidth / 2);
        int asteroidHeightAngle = Random.Range(0, asteroidSpawnAngleHeight) - (asteroidSpawnAngleHeight / 2);
        Vector3 asteroidSpawnPosition = new Vector3(
            asteroidSpawnRadius * Mathf.Sin(Mathf.Deg2Rad*asteroidWidthAngle) * Mathf.Sin(Mathf.Deg2Rad*asteroidHeightAngle), 
            asteroidSpawnRadius * Mathf.Cos(Mathf.Deg2Rad * asteroidWidthAngle) * Mathf.Sin(Mathf.Deg2Rad * asteroidHeightAngle),
            asteroidSpawnRadius*Mathf.Cos(Mathf.Deg2Rad * asteroidHeightAngle)
        );

        //find the vector pointing from our position to the target
        Vector3 direction = (targetPosition - asteroidSpawnPosition).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        GameObject asteroidObject = Instantiate(asteroid, asteroidSpawnPosition, lookRotation) as GameObject;
        Rigidbody rb = asteroidObject.GetComponent<Rigidbody>();
        rb.velocity = rb.transform.forward * asteroidSpeed;
        //asteroidObject.transform.rotation = Quaternion.LookRotation(targetPosition);
        //asteroidObject.GetComponent<Rigidbody>().velocity = 20*transform.forward;
    }

    private IEnumerator GenerateAsteroids(float frequency)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f/frequency);
            launchAsteroid();
        }
        
    }
}
