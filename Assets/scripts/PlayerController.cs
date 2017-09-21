using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour {


    //Public variables
	public Color setColorLeft;
	public Color activeColorLeft;

	public Color setColorRight;
	public Color activeColorRight;

    public float colorProgress;

    public float health;
    public float ammunition;
    public float TimeScale = 2f;
    public float maxDistance = 0.5f;

    
    public GameObject bulletPrefab;
    public GameObject circlePSPrefab;

    //Private variables

    private bool delayedStartCompleted = false;
    private GameObject leftParticleSystemGO;
    private GameObject rightParticleSystemGO;
    private GameObject leftController;
    private GameObject rightController;

    private float currentSwipePosition;
    private float previousSwipePosition;

    private float addedTime = 0f;
    private bool updateColorProgress = false;
<<<<<<< HEAD
    private float colorProgressDelta = 0f;

    private string swipeDirection = "";

    private int leftDeviceIndex;
    private int rightDeviceIndex;

    private int leftColorIndex;
    private int rightColorIndex;

    private Color leftCurrentColor;
    private Color rightCurrentColor;
    private Color combinedCurrentColor;
    private bool colorIsCombined = false;

    GameObject combinedBulletSpawn;

    private ParticleSystem leftPS;
    private ParticleSystem rightPS;

    // Rotate claws

    public float rotationSpeed = 10.0f;
    private float degree = 120.0f;
    private Quaternion targetRotation;
    public GameObject clawsLeft;
    public GameObject handleLeft;


    void Start() {

        
        previousSwipePosition = 0;

        targetRotation = clawsLeft.transform.rotation;

        leftDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        rightDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);


        

        leftColorIndex = 0;
        rightColorIndex = 0;

        leftCurrentColor = Color.red;
        rightCurrentColor = Color.red;

        combinedBulletSpawn = new GameObject();
    }
    
    private void delayedStart()
    {
        Debug.Log("delayed start");
        //Instantiate colorcircle particle systems
        leftParticleSystemGO = Instantiate(circlePSPrefab, leftController.transform.position, leftController.transform.rotation) as GameObject;
        leftParticleSystemGO.transform.SetParent(leftController.transform);

        rightParticleSystemGO = Instantiate(circlePSPrefab, rightController.transform.position, rightController.transform.rotation) as GameObject;
        rightParticleSystemGO.transform.SetParent(rightController.transform);

        leftPS = leftParticleSystemGO.GetComponent<ParticleSystem>();
        rightPS = rightParticleSystemGO.GetComponent<ParticleSystem>();
        delayedStartCompleted = true;
    }

    void Update()
    {
        leftController = GameObject.FindWithTag("leftController");
        rightController = GameObject.FindWithTag("rightController");

        if (leftController == null || rightController == null)
            return;
        else if (!delayedStartCompleted)
            delayedStart();
        
        //Update left controller color and rotation
        //TODO Rotation
        if (leftDeviceIndex != -1 && SteamVR_Controller.Input(leftDeviceIndex).GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {

            Vector2 axisPress = SteamVR_Controller.Input(leftDeviceIndex).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            float xAxis = axisPress[0];

            //Quaternion handleVector = handleLeft.transform.rotation;

            if (xAxis > 0)
            {
                
                leftColorIndex--;

            }
            else
            {

                leftColorIndex++;
            }
            leftColorIndex = (leftColorIndex + 3) % 3;

            Debug.Log("Left color index: " + leftColorIndex);
            Color endColor = indexToColor(leftColorIndex);
            StartCoroutine(LerpColor(leftPS, leftCurrentColor, endColor));
            leftCurrentColor = endColor;
            Debug.Log(leftCurrentColor);

            //clawsLeft.transform.rotation = Quaternion.Lerp(clawsLeft.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //Update right controller color and rotation
        //TODO Rotation
        if (rightDeviceIndex != -1 && SteamVR_Controller.Input(rightDeviceIndex).GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {

            Vector2 axisPress = SteamVR_Controller.Input(rightDeviceIndex).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            float xAxis = axisPress[0];

            //Quaternion handleVector = handleLeft.transform.rotation;

            if (xAxis > 0)
            {

                rightColorIndex--;

            }
            else
            {
                rightColorIndex++;
            }

            rightColorIndex = (rightColorIndex + 3) % 3;
            Debug.Log("RIndex: " + rightColorIndex);

            Color endColor = indexToColor(rightColorIndex);
            StartCoroutine(LerpColor(rightPS, rightCurrentColor, endColor));
            rightCurrentColor = endColor;

            //clawsLeft.transform.rotation = Quaternion.Lerp(clawsLeft.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


        float distanceBetweenWeapons = (leftController.transform.position - rightController.transform.position).magnitude;

        colorIsCombined = false; //Reset flag


        ParticleSystem.MainModule leftMainModule = leftPS.main;
        ParticleSystem.MainModule rightMainModule = rightPS.main;

        if (distanceBetweenWeapons < maxDistance)
        {
            colorIsCombined = true;
            //Calculate resulting color
            combinedCurrentColor = leftCurrentColor + rightCurrentColor;

            //Update ParticleSystem color
            leftMainModule.startColor = combinedCurrentColor;
            rightMainModule.startColor = combinedCurrentColor;

            // Create spawn point for projectile
            Vector3 combinedWeaponPos = (leftController.transform.position + rightController.transform.position) / 2;
            Quaternion combinedWeaponRot = Quaternion.Slerp(leftController.transform.rotation, rightController.transform.rotation, 0.5f);

            
            combinedBulletSpawn.transform.position = combinedWeaponPos;
            combinedBulletSpawn.transform.rotation = combinedWeaponRot;
            

        } else
        {
            leftMainModule.startColor = leftCurrentColor;
            rightMainModule.startColor = rightCurrentColor;
        }

        //Left trigger
        if (leftDeviceIndex != -1 && SteamVR_Controller.Input(leftDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {

            Shoot(leftDeviceIndex);
            //Debug.Log(deviceindexLeft);
        }
        
        //Right trigger
        if (rightDeviceIndex != -1 && SteamVR_Controller.Input(rightDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {

            Shoot(rightDeviceIndex);
            
            //Debug.Log(deviceindexRight);
        }

    }

    private void Shoot(int deviceIndex)
    {

        //AudioSource audio = GetComponent<AudioSource>();
        //audio.Play();
        //yield return new WaitForSeconds(0f);
        SteamVR_Controller.Device device = SteamVR_Controller.Input(deviceIndex);

        Color bulletColor;
        Vector3 bulletSpawnPosition;
        Quaternion bulletSpawnRotation;

        if (colorIsCombined)
        {
            bulletColor = combinedCurrentColor;
            bulletSpawnPosition = combinedBulletSpawn.transform.position;
            bulletSpawnRotation = combinedBulletSpawn.transform.rotation * Quaternion.Euler(90f, 0, 0);
        } else
        {
            if (deviceIndex == leftDeviceIndex)
            {
                bulletColor = leftCurrentColor;
                bulletSpawnPosition = leftController.transform.position;
                bulletSpawnRotation = leftController.transform.rotation * Quaternion.Euler(90f, 0, 0);
            } else
            {
                bulletColor = rightCurrentColor;
                bulletSpawnPosition = rightController.transform.position;
                bulletSpawnRotation = rightController.transform.rotation * Quaternion.Euler(90f, 0, 0);
            }
        }

        GameObject bulletObject = Instantiate(bulletPrefab, bulletSpawnPosition, bulletSpawnRotation) as GameObject;

        if (bulletColor == Color.red)
        {
            bulletObject.gameObject.tag = "red";
        }
        else if (bulletColor == Color.green)
        {
            bulletObject.gameObject.tag = "green";
        }
        else if (bulletColor == Color.blue)
        {
            bulletObject.gameObject.tag = "blue";
        }
        else if (bulletColor == Color.cyan)
        {
            bulletObject.gameObject.tag = "cyan";
        }
        else if (bulletColor == Color.magenta)
        {
            bulletObject.gameObject.tag = "magenta";
        }
        else if (bulletColor == new Color(1,1,0)) //Yellow
        {
            bulletObject.gameObject.tag = "yellow";
        }
        else
        {
            Debug.Log("Tag error: " + bulletColor);
        }

        MeshRenderer gameObjectRenderer = bulletObject.GetComponent<MeshRenderer>();

        Material newMaterial = new Material(Shader.Find("Standard"))
        {
            color = bulletColor
        };

        
        gameObjectRenderer.material = newMaterial;
    }

    private IEnumerator LerpColor(ParticleSystem ps, Color currentColor, Color endColor)
    {
        float progress = 0;

        while (progress <= 1)
        {
            Color lerpedColor = Color.Lerp(currentColor, endColor, progress);

            var main = ps.main;
            main.startColor = lerpedColor;

            progress += Time.deltaTime * TimeScale;
            yield return null;
        }

        currentColor = endColor;

    }

    //Returns black on error
    private Color indexToColor(int colorIndex)
    {
        switch (colorIndex)
        {
            case 0:
                return Color.red;
            case 1:
                return Color.blue;
            case 2:
                return Color.green;
            default:
                Debug.Log("Index to Color error, index = " + colorIndex);
                return Color.black;
        }
=======
    private float colorProgressDelta = 0f;

    private string swipeDirection = "";

    // Rotate claws

    public float rotationSpeed = 10.0f;
    private float degree = 120.0f;
    private Quaternion targetRotation;
    public GameObject clawsLeft;
    public GameObject handleLeft;


    void Start() {

        deviceindex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        previousSwipePosition = 0;

        targetRotation = clawsLeft.transform.rotation;
    }

    void Update()
    {

        //if (deviceindex != -1 && SteamVR_Controller.Input(deviceindex).GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        //{

        //    var axisPress = SteamVR_Controller.Input(deviceindex).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
        //    var xAxis = axisPress[0];

        //    var handleVector = handleLeft.transform.rotation;

        //    if (xAxis>0)
        //    {
        //        Debug.Log("Höger tryck");
        //        // Rotera åt höger

                
        //        //targetRotation *= Quaternion.AngleAxis(degree, handleVector);
        //        targetRotation *= handleVector;

        //    } else
        //    {
        //        Debug.Log("Vänster tryck");
        //        // Rotera åt vänster
        //        //targetRotation *= Quaternion.AngleAxis(-degree, handleVector);

        //    }
 

        //}

        //clawsLeft.transform.rotation = Quaternion.Lerp(clawsLeft.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (deviceindex != -1 && SteamVR_Controller.Input(deviceindex).GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {

            var axis = SteamVR_Controller.Input(deviceindex).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

            currentSwipePosition = axis[0];
            addedTime += Time.deltaTime;

            if (previousSwipePosition != currentSwipePosition)
            {
                if (previousSwipePosition > currentSwipePosition)
                {
                
                    colorProgressDelta += (-currentSwipePosition + 1) / 2;
                    swipeDirection = "left";

                }
                else
                {

                    colorProgressDelta += (currentSwipePosition + 1) / 2;
                    swipeDirection = "right";

                }

                if (updateColorProgress)
                {
                    colorProgress = (colorProgressDelta/addedTime)/60;
                    colorProgressDelta = 0f;
                    addedTime = 0;

                    updateColorProgress = false;
                    //Debug.Log(colorProgress);
                    //Debug.Log(swipeDirection);
                }

            }

            if (addedTime > 0.1)
            {
                updateColorProgress = true;
            }

            
            //Debug.Log(currentSwipePosition);


            previousSwipePosition = currentSwipePosition;
        } else
        {
            addedTime = 0f;
            colorProgressDelta = 0f;
            updateColorProgress = false;
        }

    }

    public static float map01(float value, float min, float max)
    {
        return (value - min) * 1f / (max - min);
    }

    public static float superLerp(float from, float to, float from2, float to2, float value)
    {
        if (value <= from2)
            return from;
        else if (value >= to2)
            return to;
        return (to - from) * ((value - from2) / (to2 - from2)) + from;
>>>>>>> master
    }

}
