using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

    public int asteroidSpeed;
    public int asteroidSpawnAngleWidth;
    public int asteroidSpawnAngleHeight;
    public int asteroidSpawnRadius;
    public bool mixedColors;
    public float asteroidMinSize;
    public float asteroidMaxSize;
    public float asteroidHitRatio;
    public Vector3 gravity;
    //public float asteroidHealth;

	public GameController gameControllerScript;
	public bool gameOver;

    public GameObject asteroid;
    public GameObject target;

	public GameObject asteroidRed;
	public GameObject asteroidGreen;
	public GameObject asteroidBlue;
	public GameObject asteroidCyan;
	public GameObject asteroidMagenta;
	public GameObject asteroidYellow;

    private float percentComplete = 0;
    private Transform childAsteroid;

	private GameObject asteroidObject;

    private float asteroidFrequency = 0.1f;
    private float mixedPercentage = 0.1f;

    private IEnumerator coroutine;

    Vector3 targetPosition;
	// Use this for initialization
	void Start () {
        targetPosition = new Vector3(0, 0, 0);

		gameOver = gameControllerScript.gameOver;

        coroutine = GenerateAsteroids();
        StartCoroutine(coroutine);
	}
	
	// Update is called once per frame
	void Update () {
		gameOver = gameControllerScript.gameOver;

        if (target != null)
        {
            targetPosition = target.transform.position;
        }
        percentComplete = (gameControllerScript.gameTime / gameControllerScript.maxGameTime);
        Mathf.Clamp01(percentComplete);
        asteroidFrequency = (percentComplete / 3) + 0.1f; //Will be negative for about 10 seconds
        //Debug.Log(asteroidFrequency);
        mixedPercentage = (percentComplete / 3) - 0.1f; //Go linearly from -10 to 23 percent, should pass 0 at ~60 seconds in
        
        if (Input.GetKeyDown("space"))
        {
            //Time.timeScale = 1 - Time.timeScale;
            //launchAsteroid();
        }
            

    }


    void launchAsteroid ()
    {
        
        //Randomize positions and center them
        int asteroidWidthAngle = Random.Range(0, asteroidSpawnAngleWidth) - (asteroidSpawnAngleWidth / 2);
        int asteroidHeightAngle = Random.Range(0, asteroidSpawnAngleHeight) - (asteroidSpawnAngleHeight / 2);

        Vector3 asteroidSpawnPosition = new Vector3(
            targetPosition.x + asteroidSpawnRadius * Mathf.Sin(Mathf.Deg2Rad * asteroidWidthAngle) * Mathf.Cos(Mathf.Deg2Rad * asteroidHeightAngle),
            targetPosition.y + asteroidSpawnRadius * Mathf.Sin(Mathf.Deg2Rad * asteroidHeightAngle),
            targetPosition.z + asteroidSpawnRadius * Mathf.Cos(Mathf.Deg2Rad * asteroidHeightAngle) * Mathf.Cos(Mathf.Deg2Rad * asteroidWidthAngle)
        );

        //find the vector pointing from our position to the target
        Vector3 direction = (targetPosition - asteroidSpawnPosition).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        int randomNumber = Random.Range(0, 10);
        int asteroidPrefabIndex = 0;

        if (randomNumber < (mixedPercentage*10))
        {
            asteroidPrefabIndex += Random.Range(3, 6);
        } else
        {
            asteroidPrefabIndex = Random.Range(0, 3);
        }


		asteroidObject = Instantiate(colorToPrefab(indexToColor(asteroidPrefabIndex)), asteroidSpawnPosition, lookRotation) as GameObject;
        foreach (Transform child in asteroidObject.transform) if (child.CompareTag(colorToString(indexToColor(asteroidPrefabIndex))))
        {
            childAsteroid = child;
        }

        asteroidObject.transform.SetParent(GetComponent<Transform>());
        float asteroidSize = Random.Range(asteroidMinSize, asteroidMaxSize);
        asteroidObject.transform.localScale = new Vector3(asteroidSize, asteroidSize, asteroidSize);
		//asteroidObject.AddComponent<Rigidbody>();


		Rigidbody rb = childAsteroid.GetComponent<Rigidbody>();
		rb.velocity = rb.transform.forward * asteroidSpeed;
    }

    void launchAsteroid(Color color, int widthAngle, int heightAngle)
    {
        Vector3 asteroidSpawnPosition = new Vector3(
            targetPosition.x + asteroidSpawnRadius * Mathf.Sin(Mathf.Deg2Rad * widthAngle) * Mathf.Cos(Mathf.Deg2Rad * heightAngle),
            targetPosition.y + asteroidSpawnRadius * Mathf.Sin(Mathf.Deg2Rad * heightAngle),
            targetPosition.z + asteroidSpawnRadius * Mathf.Cos(Mathf.Deg2Rad * heightAngle) * Mathf.Cos(Mathf.Deg2Rad * widthAngle)
        );

        //find the vector pointing from our position to the target
        Vector3 direction = (targetPosition - asteroidSpawnPosition).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        int randomNumber = Random.Range(0, 10);
        int asteroidPrefabIndex = 0;

        if (randomNumber < (mixedPercentage * 10))
        {
            asteroidPrefabIndex += Random.Range(3, 6);
        }
        else
        {
            asteroidPrefabIndex = Random.Range(0, 3);
        }


        asteroidObject = Instantiate(colorToPrefab(color), asteroidSpawnPosition, lookRotation) as GameObject;
        foreach (Transform child in asteroidObject.transform) if (child.CompareTag(colorToString(color)))
        {
            childAsteroid = child;
        }

        asteroidObject.transform.SetParent(GetComponent<Transform>());
        float asteroidSize = Random.Range(asteroidMinSize, asteroidMaxSize);
        asteroidObject.transform.localScale = new Vector3(asteroidSize, asteroidSize, asteroidSize);
        //asteroidObject.AddComponent<Rigidbody>();


        Rigidbody rb = childAsteroid.GetComponent<Rigidbody>();
        rb.velocity = rb.transform.forward * asteroidSpeed;
    }

    public GameObject createAsteroid(string color, Vector3 position)
    {
        GameObject prefab;
        switch (color)
        {
            case "red":
                prefab = asteroidRed;
                break;
            case "green":
                prefab = asteroidGreen;
                break;
            case "blue":
                prefab = asteroidBlue;
                break;
            case "cyan":
                prefab = asteroidCyan;
                break;
            case "magenta":
                prefab = asteroidMagenta;
                break;
            case "yellow":
                prefab = asteroidYellow;
                break;
            default:
                prefab = null;
                break;
        }
        return Instantiate(prefab, position, Quaternion.identity) as GameObject;
    }

    private IEnumerator GenerateAsteroids()
    {
		while (gameOver == false)
        {
            //Debug.Log(asteroidFrequency);
            if (asteroidFrequency <= 0.12f)
            {
                //Debug.Log("a" + asteroidFrequency);
                yield return null;
            }
            else {
                Debug.Log(asteroidFrequency + " " + 0.5f / asteroidFrequency);
                yield return new WaitForSeconds(0.5f / asteroidFrequency);
                Debug.Log("Launch");
                launchAsteroid();
            }
            
        }
        
    }

    private int Mod(int a, int b)
    {
        return (a % b + b) % b;
    }

    //Returns black on error
    private Color indexToColor(int colorIndex)
    {
        //colorIndex = Mod(colorIndex, 6);
        switch (colorIndex)
        {
            case 0:
                return Color.red;
            case 1:
                return Color.blue;
            case 2:
                return Color.green;
            case 3:
                return Color.cyan;
            case 4:
                return Color.magenta;
            case 5:
                return new Color(1, 1, 0); //Yellow
            default:
                Debug.Log("Index to Color error, index = " + colorIndex);
                return Color.black;
        }
    }

    private string colorToString(Color color)
    {
        if (color == Color.red)
            return "red";
        else if (color == Color.green)
            return "green";
        else if (color == Color.blue)
            return "blue";
        else if (color == Color.cyan)
            return "cyan";
        else if (color == Color.magenta)
            return "magenta";
        else if (color == new Color(1, 1, 0)) //Yellow
            return "yellow";
        else
        {
            Debug.Log("Color string error: " + color);
            return null;
        }
    }

    private GameObject colorToPrefab(Color color)
    {
        if (color == Color.red)
            return asteroidRed;
        else if (color == Color.green)
            return asteroidGreen;
        else if (color == Color.blue)
            return asteroidBlue;
        else if (color == Color.cyan)
            return asteroidCyan;
        else if (color == Color.magenta)
            return asteroidMagenta;
        else if (color == new Color(1, 1, 0)) //Yellow
            return asteroidYellow;
        else
        {
            Debug.Log("Tag error: " + color);
            return null;
        }
    }
}
