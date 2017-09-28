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

    public GameObject weaponParticle;
    //public GameObject bulletPrefab;
    public GameObject circlePSPrefab;
    public GameObject weaponEmitter;

    public Animator animLeft;
    public Animator animRight;

    public GameObject leftButtonLeft;
    public GameObject leftButtonRight;

    public GameObject rightButtonLeft;
    public GameObject rightButtonRight;

    public GameObject lightEmitterLeft;
    public GameObject lightEmitterRight;

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

    private ParticleSystem leftWeaponPS;
    private ParticleSystem rightWeaponPS;

    // Rotate claws

    public float rotationSpeed = 10.0f;
    private float degree = 120.0f;

    // Weapon Emitter materials
    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    public Material cyanMaterial;
    public Material magentaMaterial;
    public Material yellowMaterial;

    private GameObject leftWeaponEmitter;
    private GameObject rightWeaponEmitter;


    void Start() {

        
        previousSwipePosition = 0;

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
        leftParticleSystemGO.transform.localScale = new Vector3(0, 0, 0);

        rightParticleSystemGO = Instantiate(circlePSPrefab, rightController.transform.position, rightController.transform.rotation) as GameObject;
        rightParticleSystemGO.transform.SetParent(rightController.transform);
        rightParticleSystemGO.transform.localScale = new Vector3(0, 0, 0);

        leftPS = leftParticleSystemGO.GetComponent<ParticleSystem>();
        rightPS = rightParticleSystemGO.GetComponent<ParticleSystem>();

        leftWeaponEmitter = Instantiate(weaponEmitter, leftController.transform.position + weaponEmitter.transform.position, leftController.transform.rotation) as GameObject;
        leftWeaponEmitter.transform.SetParent(leftController.transform);

        rightWeaponEmitter = Instantiate(weaponEmitter, rightController.transform.position + weaponEmitter.transform.position, rightController.transform.rotation) as GameObject;
        rightWeaponEmitter.transform.SetParent(rightController.transform);

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
        if (leftDeviceIndex != -1 && SteamVR_Controller.Input(leftDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
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

            MeshRenderer leftButtonLeftRenderer = leftButtonLeft.GetComponent<MeshRenderer>();
            MeshRenderer leftButtonRightRenderer = leftButtonRight.GetComponent<MeshRenderer>();

            MeshRenderer lightEmitterLeftRenderer = lightEmitterLeft.GetComponent<MeshRenderer>();

            if (leftColorIndex==0)
            {

                leftWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = redMaterial;

                Material newMaterialLeft = blueMaterial;
                leftButtonLeftRenderer.material = newMaterialLeft;

                Material newMaterialRight = greenMaterial;
                leftButtonRightRenderer.material = newMaterialRight;


                Material newMaterialEmitter = redMaterial;
                lightEmitterLeftRenderer.material = newMaterialEmitter;


            } else if(leftColorIndex==1) {

                leftWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = blueMaterial;

                Material newMaterialLeft = greenMaterial;
                leftButtonLeftRenderer.material = newMaterialLeft;

                Material newMaterialRight = redMaterial;
                leftButtonRightRenderer.material = newMaterialRight;

                Material newMaterialEmitter = blueMaterial;
                lightEmitterLeftRenderer.material = newMaterialEmitter;

            } else 
            {
                leftWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = greenMaterial;

                Material newMaterialLeft = redMaterial;
                leftButtonLeftRenderer.material = newMaterialLeft;

                Material newMaterialRight = blueMaterial;
                leftButtonRightRenderer.material = newMaterialRight;

                Material newMaterialEmitter = greenMaterial;
                lightEmitterLeftRenderer.material = newMaterialEmitter;

            }


            //clawsLeft.transform.rotation = Quaternion.Lerp(clawsLeft.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //Update right controller color and rotation
        //TODO Rotation
        if (rightDeviceIndex != -1 && SteamVR_Controller.Input(rightDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
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

            MeshRenderer rightButtonLeftRenderer = rightButtonLeft.GetComponent<MeshRenderer>();
            MeshRenderer rightButtonRightRenderer = rightButtonRight.GetComponent<MeshRenderer>();

            MeshRenderer lightEmitterRightRenderer = lightEmitterRight.GetComponent<MeshRenderer>();

            if (rightColorIndex == 0)
            {

                rightWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = redMaterial;

                Material newMaterialLeft = blueMaterial;
                rightButtonLeftRenderer.material = newMaterialLeft;

                Material newMaterialRight = greenMaterial;
                rightButtonRightRenderer.material = newMaterialRight;

                Material newMaterialEmitter = redMaterial;
                lightEmitterRightRenderer.material = newMaterialEmitter;


            }
            else if (rightColorIndex == 1)
            {

                rightWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = blueMaterial;

                Material newMaterialLeft = greenMaterial;
                rightButtonLeftRenderer.material = newMaterialLeft;

                Material newMaterialRight = redMaterial;
                rightButtonRightRenderer.material = newMaterialRight;

                Material newMaterialEmitter = blueMaterial;
                lightEmitterRightRenderer.material = newMaterialEmitter;

            }
            else
            {

                rightWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = greenMaterial;

                Material newMaterialLeft = redMaterial;
                rightButtonLeftRenderer.material = newMaterialLeft;

                Material newMaterialRight = blueMaterial;
                rightButtonRightRenderer.material = newMaterialRight;

                Material newMaterialEmitter = greenMaterial;
                lightEmitterRightRenderer.material = newMaterialEmitter;

            }

            //clawsLeft.transform.rotation = Quaternion.Lerp(clawsLeft.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


        float distanceBetweenWeapons = (leftController.transform.position - rightController.transform.position).magnitude;

        colorIsCombined = false; //Reset flag


        ParticleSystem.MainModule leftMainModule = leftPS.main;
        ParticleSystem.MainModule rightMainModule = rightPS.main;

        if (distanceBetweenWeapons < maxDistance) // && leftCurrentColor != rightCurrentColor
        {
            colorIsCombined = true;
            //Calculate resulting color
            combinedCurrentColor = leftCurrentColor + rightCurrentColor;
            combinedCurrentColor = normalizeColor(combinedCurrentColor);

            //Update ParticleSystem color
            leftMainModule.startColor = combinedCurrentColor;
            rightMainModule.startColor = combinedCurrentColor;

            rightWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = colorToMaterial(combinedCurrentColor);
            leftWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = colorToMaterial(combinedCurrentColor);

            // Create spawn point for projectile
            Vector3 combinedWeaponPos = (leftController.transform.position + rightController.transform.position) / 2;
            Quaternion combinedWeaponRot = Quaternion.Slerp(leftController.transform.rotation, rightController.transform.rotation, 0.5f);

            
            combinedBulletSpawn.transform.position = combinedWeaponPos;
            combinedBulletSpawn.transform.rotation = combinedWeaponRot;
            

        } else
        {
            leftMainModule.startColor = leftCurrentColor;
            rightMainModule.startColor = rightCurrentColor;
            rightWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = colorToMaterial(rightCurrentColor);
            leftWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = colorToMaterial(leftCurrentColor);
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
            animLeft.SetTrigger("Shoot");
            animRight.SetTrigger("Shoot");
            bulletColor = combinedCurrentColor;
            bulletSpawnPosition = combinedBulletSpawn.transform.position;
            bulletSpawnRotation = combinedBulletSpawn.transform.rotation * Quaternion.Euler(90f, 0, 0);
        } else
        {
            if (deviceIndex == leftDeviceIndex)
            {
                animLeft.SetTrigger("Shoot");
                bulletColor = leftCurrentColor;
                //Vector3 offsetVector = leftController.transform.forward/6;
                bulletSpawnPosition = leftController.transform.position + weaponParticle.transform.position;
                bulletSpawnRotation = leftController.transform.rotation * Quaternion.Euler(90f, 0, 0);
            } else
            {
                animRight.SetTrigger("Shoot");

                bulletColor = rightCurrentColor;
                //Vector3 offsetVector = rightController.transform.forward/6;
                bulletSpawnPosition = rightController.transform.position + weaponParticle.transform.position;
                bulletSpawnRotation = rightController.transform.rotation * Quaternion.Euler(90f, 0, 0);
            }
        }

        GameObject bulletObject = Instantiate(weaponParticle, bulletSpawnPosition, bulletSpawnRotation) as GameObject;
        bulletObject.GetComponent<ParticleSystemRenderer>().material = colorToMaterial(bulletColor);


        Debug.Log(bulletColor);

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

    private Color normalizeColor(Color color)
    {
        color.r = Mathf.Clamp(color.r, 0, 1);
        color.g = Mathf.Clamp(color.g, 0, 1);
        color.b = Mathf.Clamp(color.b, 0, 1);
        color.a = Mathf.Clamp(color.a, 0, 1);

        return color;
    }

    private Material colorToMaterial(Color color)
    {
        if (color == Color.red)
        {
            return redMaterial;
        }
        else if (color == Color.green)
        {
            return greenMaterial;
        }
        else if (color == Color.blue)
        {
            return blueMaterial;
        }
        else if (color == Color.cyan)
        {
            return cyanMaterial;
        }
        else if (color == Color.magenta)
        {
            return magentaMaterial;
        }
        else if (color == new Color(1, 1, 0)) //Yellow
        {
            return yellowMaterial;
        }
        else
        {
            Debug.Log("Tag error: " + color);
            return null;
        }
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
    }

}
