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

    public GameObject leftParticleSystemGO;
    public GameObject rightParticleSystemGO;

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
