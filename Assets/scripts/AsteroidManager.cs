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
        
        //Debug.Log(asteroidColorIndex);
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

		int asteroidColorIndex = Random.Range(0, 4);
		if (asteroidColorIndex == 3) {
			asteroidColorIndex += Random.Range(0, 3);
		}
        Color asteroidColor;

        switch (asteroidColorIndex)
        {
            case 0:
                asteroidObject.gameObject.tag = "blue";
                asteroidColor = new Color(0, 0, 1, 1);
                break;
            case 1:
                asteroidObject.gameObject.tag = "red";
                asteroidColor = new Color(1, 0, 0, 1);
                break;
            case 2:
                asteroidObject.gameObject.tag = "green";
                asteroidColor = new Color(0, 1, 0, 1);
                break;
			case 3:
				asteroidObject.gameObject.tag = "cyan";
				asteroidColor = new Color(0, 1, 1, 1);
				break;
			case 4:
				asteroidObject.gameObject.tag = "magenta";
				asteroidColor = new Color(1, 0, 1, 1);
				break;
			case 5:
				asteroidObject.gameObject.tag = "yellow";
				asteroidColor = new Color(1, 1, 0, 1);
				break;
	            default:
                Debug.Log("Error");
                asteroidColor = new Color(1, 1, 1, 1); //It needs some color incase it bugs out, should not happen though
                break;
        }



        MeshRenderer gameObjectRenderer = asteroidObject.GetComponent<MeshRenderer>();

        Material newMaterial = new Material(Shader.Find("Standard"));

        newMaterial.color = asteroidColor;
        gameObjectRenderer.material = newMaterial;
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
