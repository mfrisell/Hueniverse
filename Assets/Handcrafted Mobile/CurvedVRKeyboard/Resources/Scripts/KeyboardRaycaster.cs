using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
namespace CurvedVRKeyboard {

    public class KeyboardRaycaster: KeyboardComponent {

        //------Raycasting----
        [SerializeField, HideInInspector]
        private Transform raycastingSource;

        [SerializeField, HideInInspector]
        private GameObject target;

        private float rayLength;
        private Ray ray;
        private RaycastHit hit;
        private LayerMask layer;
        private float minRaylengthMultipler = 1.5f;
        //---interactedKeys---
        private KeyboardStatus keyboardStatus;
        private KeyboardItem keyItemCurrent;

		private Transform previousChosenKey;

        [SerializeField, HideInInspector]
        private string clickInputName;
        private int rightDeviceIndex;

        //public GameObject RightController;

        void Start () {
            keyboardStatus = gameObject.GetComponent<KeyboardStatus>();
            int layerNumber = gameObject.layer;
            layer = 1 << layerNumber;
            rightDeviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
        }

        void Update () {
            // * sum of all scales so keys are never to far
            rayLength = Vector3.Distance(raycastingSource.position, target.transform.position) * (minRaylengthMultipler + 
                 (Mathf.Abs(target.transform.lossyScale.x) + Mathf.Abs(target.transform.lossyScale.y) + Mathf.Abs(target.transform.lossyScale.z)));
            RayCastKeyboard();

            Debug.Log(rayLength);

			// Highlight chosen key
			if (keyItemCurrent != null) {
				
				Transform childObject = keyItemCurrent.transform.Find ("Value");

				if ((childObject != previousChosenKey) && (previousChosenKey != null)) {
					Text childObjectTextPrevious = previousChosenKey.GetComponent < Text> ();
					childObjectTextPrevious.color = Color.white;
				}

				Text childObjectText = childObject.GetComponent < Text> ();
				childObjectText.color = Color.red;

				previousChosenKey = childObject;

			}
        }

        /// <summary>
        /// Check if camera is pointing at any key. 
        /// If it does changes state of key
        /// </summary>
        private void RayCastKeyboard () {
            ray = new Ray(raycastingSource.position, raycastingSource.forward);
            if(Physics.Raycast(ray, out hit, rayLength, layer)) { // If any key was hit
                KeyboardItem focusedKeyItem = hit.transform.gameObject.GetComponent<KeyboardItem>();
                if(focusedKeyItem != null) { // Hit may occur on item without script
                    ChangeCurrentKeyItem(focusedKeyItem);
                    keyItemCurrent.Hovering();

                    //if(Input.GetButtonDown(clickInputName)) {// If key clicked

                    if(SteamVR_Controller.Input(rightDeviceIndex).GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
                    
                        keyItemCurrent.Click();
                        keyboardStatus.HandleClick(keyItemCurrent);
                    }
                }
            } else if(keyItemCurrent != null) {// If no target hit and lost focus on item
                ChangeCurrentKeyItem(null);
            }
        }

        private void ChangeCurrentKeyItem ( KeyboardItem key ) {
            if(keyItemCurrent != null) {
                keyItemCurrent.StopHovering();
            }
            keyItemCurrent = key;
        }

        //---Setters---
        public void SetRayLength ( float rayLength ) {
            this.rayLength = rayLength;
        }

        public void SetRaycastingTransform ( Transform raycastingSource ) {
            this.raycastingSource = raycastingSource;
        }

        public void SetClickButton ( string clickInputName ) {
            this.clickInputName = clickInputName;
        }

        public void SetTarget ( GameObject target ) {
            this.target = target;
        }
    }
}