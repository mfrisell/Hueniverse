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

    public GameObject leftParticleSystemGO;
    public GameObject rightParticleSystemGO;
    public GameObject bulletPrefab;

    //Private variables
    private float currentSwipePosition;
    private float previousSwipePosition;

    private float addedTime = 0f;
    private bool updateColorProgress = false;
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


        leftPS = leftParticleSystemGO.GetComponent<ParticleSystem>();
        rightPS = rightParticleSystemGO.GetComponent<ParticleSystem>();

        leftColorIndex = 0;
        rightColorIndex = 0;

        combinedBulletSpawn = new GameObject();
    }

    void Update()
    {
        //Update left controller color and rotation
        //TODO Rotation
        if (leftDeviceIndex != -1 && SteamVR_Controller.Input(leftDeviceIndex).GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {

            Vector2 axisPress = SteamVR_Controller.Input(leftDeviceIndex).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            float xAxis = axisPress[0];

            //Quaternion handleVector = handleLeft.transform.rotation;

            if (xAxis > 0)
            {
                Debug.Log("Höger tryck");
                // Rotera åt höger


                //targetRotation *= Quaternion.AngleAxis(degree, handleVector);
                //targetRotation *= handleVector;

                leftColorIndex--;

            }
            else
            {
                Debug.Log("Vänster tryck");
                // Rotera åt vänster
                //targetRotation *= Quaternion.AngleAxis(-degree, handleVector);
                
                leftColorIndex++;
            }

            Color endColor = indexToColor(leftColorIndex);
            StartCoroutine(LerpColor(leftPS, leftCurrentColor, endColor));
            leftCurrentColor = endColor;

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
                Debug.Log("Höger tryck");
                // Rotera åt höger


                //targetRotation *= Quaternion.AngleAxis(degree, handleVector);
                //targetRotation *= handleVector;

                rightColorIndex--;

            }
            else
            {
                Debug.Log("Vänster tryck");
                // Rotera åt vänster
                //targetRotation *= Quaternion.AngleAxis(-degree, handleVector);

                rightColorIndex++;
            }

            Color endColor = indexToColor(rightColorIndex);
            StartCoroutine(LerpColor(rightPS, rightCurrentColor, endColor));
            rightCurrentColor = endColor;

            //clawsLeft.transform.rotation = Quaternion.Lerp(clawsLeft.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 leftWeaponPos = leftParticleSystemGO.transform.position;
        Vector3 rightWeaponPos = rightParticleSystemGO.transform.position;

        float distanceBetweenWeapons = (leftParticleSystemGO.transform.position - rightParticleSystemGO.transform.position).magnitude;

        colorIsCombined = false; //Reset flag

        if (distanceBetweenWeapons < maxDistance)
        {
            colorIsCombined = true;
            //Calculate resulting color
            combinedCurrentColor = (leftCurrentColor + rightCurrentColor) / 2;

            //Update ParticleSystem color
            ParticleSystem.MainModule leftMainModule = leftPS.main;
            ParticleSystem.MainModule rightMainModule = rightPS.main;
            leftMainModule.startColor = combinedCurrentColor;
            rightMainModule.startColor = combinedCurrentColor;

            // Create spawn point for projectile
            Vector3 combinedWeaponPos = (leftWeaponPos + rightWeaponPos) / 2;
            Quaternion combinedWeaponRot = Quaternion.Slerp(leftParticleSystemGO.transform.rotation, rightParticleSystemGO.transform.rotation, 0.5f);

            
            combinedBulletSpawn.transform.position = combinedWeaponPos;
            combinedBulletSpawn.transform.rotation = combinedWeaponRot;
            

        }

        if (leftDeviceIndex != -1 && SteamVR_Controller.Input(leftDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {

            StartCoroutine(Shoot(true));
            //Debug.Log(deviceindexLeft);
        }
        

        if (rightDeviceIndex != -1 && SteamVR_Controller.Input(rightDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {

            StartCoroutine(Shoot(false));
            
            //Debug.Log(deviceindexRight);
        }

    }

    IEnumerator Shoot(bool isLeft)
    {

        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(0f);

        Color bulletColor;
        Vector3 bulletSpawnPosition;
        Quaternion bulletSpawnRotation;

        if (colorIsCombined)
        {
            bulletColor = combinedCurrentColor;
            bulletSpawnPosition = combinedBulletSpawn.transform.position;
            bulletSpawnRotation = combinedBulletSpawn.transform.rotation;
        } else
        {
            if (isLeft)
            {
                bulletColor = leftCurrentColor;
                bulletSpawnPosition = leftParticleSystemGO.transform.position;
                bulletSpawnRotation = leftParticleSystemGO.transform.rotation;
            } else
            {
                bulletColor = rightCurrentColor;
                bulletSpawnPosition = rightParticleSystemGO.transform.position;
                bulletSpawnRotation = rightParticleSystemGO.transform.rotation;
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
        else if (bulletColor == Color.yellow)
        {
            bulletObject.gameObject.tag = "yellow";
        }
        else
        {
            Debug.Log("Error");
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
                return Color.black;
        }
    }

}
