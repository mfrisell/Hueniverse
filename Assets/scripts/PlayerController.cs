using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{


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
    private Vector3 leftPreviousPosition;
    private Vector3 rightPreviousPosition;
    private int amountOfFrames = 0;

    private float addedTime = 0f;
    private bool updateColorProgress = false;
    private float colorProgressDelta = 0f;

    private string swipeDirection = "";

    private int leftDeviceIndex;
    private int rightDeviceIndex;

    private int leftColorIndex;
    private int rightColorIndex;

    public Color leftCurrentColor;
    public Color rightCurrentColor;
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

    private AudioSource audio;

    //Shield gesture variables
    private int numObjects = 10;
    public GameObject gestureCheckpointPrefab;
    public Vector3 center;
    public GameObject shieldHolder;

    //public GameObjects
    public GameObject spaceShip;




    void Start()
    {


        previousSwipePosition = 0;

        leftDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        rightDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);


        leftColorIndex = 0;
        rightColorIndex = 0;

        leftCurrentColor = Color.red;
        rightCurrentColor = Color.red;

        combinedBulletSpawn = new GameObject();

        audio = GetComponent<AudioSource>();
    }

    private void delayedStart()
    {
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

        rightPreviousPosition = rightController.transform.position;
        leftPreviousPosition = leftController.transform.position;


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
            audio.Play();
            SteamVR_Controller.Input(leftDeviceIndex).TriggerHapticPulse(500);
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
            leftColorIndex = Mod(leftColorIndex, 3);

            Debug.Log("Left color index: " + leftColorIndex);
            Color endColor = indexToColor(leftColorIndex);
            StartCoroutine(lerpColor(leftPS, leftCurrentColor, endColor));
            leftCurrentColor = endColor;
            Debug.Log(leftCurrentColor);

            MeshRenderer leftButtonLeftRenderer = leftButtonLeft.GetComponent<MeshRenderer>();
            MeshRenderer leftButtonRightRenderer = leftButtonRight.GetComponent<MeshRenderer>();

            MeshRenderer lightEmitterLeftRenderer = lightEmitterLeft.GetComponent<MeshRenderer>();
            ParticleSystemRenderer leftWeaponPSR = leftWeaponEmitter.GetComponent<ParticleSystemRenderer>();


            SetRenderers(leftColorIndex, leftWeaponPSR, leftButtonLeftRenderer, leftButtonRightRenderer, lightEmitterLeftRenderer);


            //clawsLeft.transform.rotation = Quaternion.Lerp(clawsLeft.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //Update right controller color and rotation
        //TODO Rotation
        if (rightDeviceIndex != -1 && SteamVR_Controller.Input(rightDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            audio.Play();
            SteamVR_Controller.Input(rightDeviceIndex).TriggerHapticPulse(500);
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

            rightColorIndex = Mod(rightColorIndex, 3);

            Color endColor = indexToColor(rightColorIndex);
            StartCoroutine(lerpColor(rightPS, rightCurrentColor, endColor));
            rightCurrentColor = endColor;

            MeshRenderer rightButtonLeftRenderer = rightButtonLeft.GetComponent<MeshRenderer>();
            MeshRenderer rightButtonRightRenderer = rightButtonRight.GetComponent<MeshRenderer>();

            MeshRenderer lightEmitterRightRenderer = lightEmitterRight.GetComponent<MeshRenderer>();
            ParticleSystemRenderer rightWeaponPSR = rightWeaponEmitter.GetComponent<ParticleSystemRenderer>();


            SetRenderers(rightColorIndex, rightWeaponPSR, rightButtonLeftRenderer, rightButtonRightRenderer, lightEmitterRightRenderer);
        }


        // Gesture for shield

        if ((rightDeviceIndex != -1 && SteamVR_Controller.Input(rightDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Grip)) || (leftDeviceIndex != -1 && SteamVR_Controller.Input(leftDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Grip)))
        {
            GameObject currentShieldController;
            if (rightDeviceIndex != -1 && SteamVR_Controller.Input(rightDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
                currentShieldController = rightController;
            }

            else {
                currentShieldController = leftController;
            }
            center = currentShieldController.transform.position;// + currentShieldController.transform.forward;
            
            Vector3 shieldDirection;
            shieldDirection = currentShieldController.transform.position - GameObject.FindWithTag("MainCamera").transform.position;
            GameObject shieldParent = (GameObject) Instantiate(shieldHolder, center, Quaternion.LookRotation(shieldDirection),spaceShip.transform);
            //Debug.Log(shieldParent.transform.position.x);

            for (int i = 0; i < numObjects; i++) {
                //Vector3 pos = CreateGestureCircle(center, 0.5f, (360.0f / numObjects) * i);
                //Quaternion rot = Quaternion.FromToRotation(Vector3.down, center - pos);
                //Instantiate(gestureCheckpointPrefab, pos, rot, shieldParent.transform);

                
                //Quaternion rot = Quaternion.FromToRotation(Vector3.down, center - pos);
                GameObject shieldCheckpoint = (GameObject) Instantiate(gestureCheckpointPrefab, new Vector3(0,0,0), new Quaternion(0,0,0,0), shieldParent.transform);
                shieldIndex sI = shieldCheckpoint.GetComponent<shieldIndex>();
                sI.index = i;

                Vector3 pos = CreateGestureCircle(new Vector3(0,0,0), 0.01f, (360.0f / numObjects) * i);
                //Quaternion rot = Quaternion.FromToRotation(Vector3.down, center - pos);

                shieldCheckpoint.transform.localPosition = pos;
                Debug.Log((360 / numObjects) * i);
                shieldCheckpoint.transform.Rotate(new Vector3(0,0,-(360/numObjects)*i));
            }

        }






        float distanceBetweenWeapons = (leftWeaponEmitter.transform.position - rightWeaponEmitter.transform.position).magnitude;

        colorIsCombined = false; //Reset flag


        ParticleSystem.MainModule leftMainModule = leftPS.main;
        ParticleSystem.MainModule rightMainModule = rightPS.main;

        if (distanceBetweenWeapons < maxDistance)
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


        }
        else
        {
            leftMainModule.startColor = leftCurrentColor;
            rightMainModule.startColor = rightCurrentColor;
            rightWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = colorToMaterial(rightCurrentColor);
            leftWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = colorToMaterial(leftCurrentColor);
        }


        if (colorIsCombined && ((leftDeviceIndex != -1 && SteamVR_Controller.Input(leftDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) || (rightDeviceIndex != -1 && SteamVR_Controller.Input(rightDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))))
        {
            Debug.Log("Shoot combined");
            shootCombined();
        } else
        {
            //Left trigger
            if (leftDeviceIndex != -1 && SteamVR_Controller.Input(leftDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("Shoot left");
                shoot(leftDeviceIndex);
                //Debug.Log(deviceindexLeft);
            }

            //Right trigger
            if (rightDeviceIndex != -1 && SteamVR_Controller.Input(rightDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("Shoot right");
                shoot(rightDeviceIndex);
                //Debug.Log(deviceindexRight);
            }
        }

        

        if (activateShield())
        {
            //TODO Activate Shield
        };

    }

    private void SetRenderers(int colorIndex, ParticleSystemRenderer psr, MeshRenderer leftButton, MeshRenderer rightButton, MeshRenderer lightEmitter)
    {
        rightWeaponEmitter.GetComponent<ParticleSystemRenderer>().material = colorToMaterial(indexToColor(colorIndex));
        leftButton.material = colorToMaterial(indexToColor(colorIndex + 1));
        rightButton.material = colorToMaterial(indexToColor(colorIndex - 1));
        lightEmitter.material = colorToMaterial(indexToColor(colorIndex));
    }

    private void shootCombined()
    {
        Color bulletColor;
        Vector3 bulletSpawnPosition;
        Quaternion bulletSpawnRotation;

        animLeft.SetTrigger("Shoot");
        animRight.SetTrigger("Shoot");
        bulletColor = combinedCurrentColor;
        bulletSpawnPosition = combinedBulletSpawn.transform.position;
        bulletSpawnRotation = combinedBulletSpawn.transform.rotation * Quaternion.Euler(90f, 0, 0);

        spawnBullet(bulletSpawnPosition, bulletSpawnRotation, bulletColor);

    }

    private void shoot(int deviceIndex)
    {

        Color bulletColor;
        Vector3 bulletSpawnPosition;
        Quaternion bulletSpawnRotation;

        if (deviceIndex == leftDeviceIndex)
        {
            SteamVR_Controller.Input(leftDeviceIndex).TriggerHapticPulse(1000);
            animLeft.SetTrigger("Shoot");
            bulletColor = leftCurrentColor;
            //Vector3 offsetVector = leftController.transform.forward/6;
            bulletSpawnPosition = leftController.transform.position + weaponParticle.transform.position;
            bulletSpawnRotation = leftController.transform.rotation * Quaternion.Euler(90f, 0, 0);
        }
        else
        {
            SteamVR_Controller.Input(rightDeviceIndex).TriggerHapticPulse(1000);
            animRight.SetTrigger("Shoot");
            bulletColor = rightCurrentColor;
            //Vector3 offsetVector = rightController.transform.forward/6;
            bulletSpawnPosition = rightController.transform.position + weaponParticle.transform.position;
            bulletSpawnRotation = rightController.transform.rotation * Quaternion.Euler(90f, 0, 0);
        }


        spawnBullet(bulletSpawnPosition, bulletSpawnRotation, bulletColor);
    }

    private void spawnBullet(Vector3 bulletSpawnPosition, Quaternion bulletSpawnRotation, Color bulletColor)
    {
        GameObject bulletObject = Instantiate(weaponParticle, bulletSpawnPosition, bulletSpawnRotation) as GameObject;
        bulletObject.GetComponent<ParticleSystemRenderer>().material = colorToMaterial(bulletColor);


        //Debug.Log(bulletColor);

        bulletObject.gameObject.tag = colorToString(bulletColor);
    }

    private IEnumerator lerpColor(ParticleSystem ps, Color currentColor, Color endColor)
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
            return redMaterial;
        else if (color == Color.green)
            return greenMaterial;
        else if (color == Color.blue)
            return blueMaterial;
        else if (color == Color.cyan)
            return cyanMaterial;
        else if (color == Color.magenta)
            return magentaMaterial;
        else if (color == new Color(1, 1, 0)) //Yellow
            return yellowMaterial;
        else
        {
            Debug.Log("Tag error: " + color);
            return null;
        }
    }

    private int Mod(int a, int b)
    {
        return (a % b + b) % b;
    }

    //Returns black on error
    private Color indexToColor(int colorIndex)
    {
        colorIndex = Mod(colorIndex, 3);
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

    private bool activateShield()
    {
        amountOfFrames++;
        Vector3 leftControllerPosition = leftController.transform.position;
        Vector3 rightControllerPosition = rightController.transform.position;

        if (leftControllerPosition.y > 100 && rightControllerPosition.y > 100 && leftControllerPosition.x > rightControllerPosition.x + 100
         && leftPreviousPosition.x < leftControllerPosition.x - 100 && rightPreviousPosition.x > rightControllerPosition.x + 100)
        {
            if (amountOfFrames % 30 == 0)
            {
                leftPreviousPosition = leftControllerPosition;
                rightPreviousPosition = rightControllerPosition;
            }
            return true;
        }
        if (amountOfFrames % 30 == 0)
        {
            leftPreviousPosition = leftControllerPosition;
            rightPreviousPosition = rightControllerPosition;
        }
        return false;
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

    //Assigning positions for gesture checkpoints

    Vector3 CreateGestureCircle(Vector3 center, float radius, float angle) {

        Vector3 pos;
        Debug.Log("center:" + center.ToString());
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;

    }

}
