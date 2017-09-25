using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

    public int asteroidSpeed;
    public static float asteroidFrequency = 0.1f;
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

	public GameObject asteroidRed;
	public GameObject asteroidGreen;
	public GameObject asteroidBlue;
	public GameObject asteroidCyan;
	public GameObject asteroidMagenta;
	public GameObject asteroidYellow;

	private Transform childAsteroid;

	private GameObject asteroidObject;

    private IEnumerator coroutine;

    Vector3 targetPosition;
	// Use this for initialization
	void Start () {
        targetPosition = new Vector3(0, 0, 0);

        coroutine = GenerateAsteroids();
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

		int asteroidPrefabIndex = Random.Range(0, 4);
		if (asteroidPrefabIndex == 3) {
			asteroidPrefabIndex += Random.Range(0, 3);
		}

		switch (asteroidPrefabIndex)
		{
		case 0:
			asteroidObject = Instantiate(asteroidRed, asteroidSpawnPosition, lookRotation) as GameObject;
			foreach (Transform child in asteroidObject.transform) if (child.CompareTag("red")) {
				childAsteroid = child;
			} 
			//asteroidObject.gameObject.tag = "red";
			break;
		case 1:
			asteroidObject = Instantiate(asteroidGreen, asteroidSpawnPosition, lookRotation) as GameObject;
			foreach (Transform child in asteroidObject.transform) if (child.CompareTag("green")) {
					childAsteroid = child;
			} 
			//asteroidObject.gameObject.tag = "green";
			break;
		case 2:
			asteroidObject = Instantiate(asteroidBlue, asteroidSpawnPosition, lookRotation) as GameObject;
			foreach (Transform child in asteroidObject.transform) if (child.CompareTag("blue")) {
					childAsteroid = child;
			} 
			//asteroidObject.gameObject.tag = "blue";
			break;
		case 3:
			asteroidObject = Instantiate(asteroidCyan, asteroidSpawnPosition, lookRotation) as GameObject;
			foreach (Transform child in asteroidObject.transform) if (child.CompareTag("cyan")) {
					childAsteroid = child;
			} 
			//asteroidObject.gameObject.tag = "cyan";
			break;
		case 4:
			asteroidObject = Instantiate(asteroidMagenta, asteroidSpawnPosition, lookRotation) as GameObject;
			foreach (Transform child in asteroidObject.transform) if (child.CompareTag("magenta")) {
					childAsteroid = child;
			} 
			//asteroidObject.gameObject.tag = "magenta";
			break;
		case 5:
			asteroidObject = Instantiate(asteroidYellow, asteroidSpawnPosition, lookRotation) as GameObject;
			foreach (Transform child in asteroidObject.transform) if (child.CompareTag("yellow")) {
					childAsteroid = child;
			} 
			//asteroidObject.gameObject.tag = "yellow";
			break;
		default:
			Debug.Log("Error");
			//asteroidColor = new Color(1, 1, 1, 1); //It needs some color incase it bugs out, should not happen though
			break;
		}

        //GameObject asteroidObject = Instantiate(asteroid, asteroidSpawnPosition, lookRotation) as GameObject;

        asteroidObject.transform.SetParent(GetComponent<Transform>());
        float asteroidSize = Random.Range(asteroidMinSize, asteroidMaxSize);
        asteroidObject.transform.localScale = new Vector3(asteroidSize, asteroidSize, asteroidSize);
		//asteroidObject.AddComponent<Rigidbody>();


		Rigidbody rb = childAsteroid.GetComponent<Rigidbody>();
		rb.velocity = rb.transform.forward * asteroidSpeed;

//		foreach (Transform child in asteroidObject) if (child.CompareTag("Zone")) {
//				
//		} 

//        Rigidbody rb = asteroidObject.GetComponent<Rigidbody>();
//
//
//		Debug.Log (rb.velocity);
//        rb.velocity = rb.transform.forward * asteroidSpeed;

//		int asteroidColorIndex = Random.Range(0, 4);
//		if (asteroidColorIndex == 3) {
//			asteroidColorIndex += Random.Range(0, 3);
//		}
//        Color asteroidColor;
//
//        switch (asteroidColorIndex)
//        {
//            case 0:
//                asteroidObject.gameObject.tag = "blue";
//                asteroidColor = new Color(0, 0, 1, 1);
//                break;
//            case 1:
//                asteroidObject.gameObject.tag = "red";
//                asteroidColor = new Color(1, 0, 0, 1);
//                break;
//            case 2:
//                asteroidObject.gameObject.tag = "green";
//                asteroidColor = new Color(0, 1, 0, 1);
//                break;
//			case 3:
//				asteroidObject.gameObject.tag = "cyan";
//				asteroidColor = new Color(0, 1, 1, 1);
//				break;
//			case 4:
//				asteroidObject.gameObject.tag = "magenta";
//				asteroidColor = new Color(1, 0, 1, 1);
//				break;
//			case 5:
//				asteroidObject.gameObject.tag = "yellow";
//				asteroidColor = new Color(1, 1, 0, 1);
//				break;
//	            default:
//                Debug.Log("Error");
//                asteroidColor = new Color(1, 1, 1, 1); //It needs some color incase it bugs out, should not happen though
//                break;
//        }
//
//        MeshRenderer gameObjectRenderer = asteroidObject.GetComponent<MeshRenderer>();
//
//        Material newMaterial = new Material(Shader.Find("Standard"));
//
//        newMaterial.color = asteroidColor;
//        gameObjectRenderer.material = newMaterial;
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
        while (true)
        {
            Debug.Log(asteroidFrequency);
            if (asteroidFrequency == 0)
                yield return null;
            else {
                yield return new WaitForSeconds(0.5f / asteroidFrequency);
                Debug.Log(asteroidFrequency);
                launchAsteroid();
            }
            
        }
        
    }
}
