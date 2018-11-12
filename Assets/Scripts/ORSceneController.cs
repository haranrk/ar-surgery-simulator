using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;    

#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = GoogleARCore.InstantPreviewInput;
#endif

public class ORSceneController : MonoBehaviour {
	public GameObject OperationTheater;
	public GameObject ARCamera;

	Vector3 GroundOffsetVector = new Vector3(0.0f, 0.7f, 0.0f);
	bool SetupCompleted = false;
    GameObject currentInstrument;
	private bool m_IsQuitting = false;
	float ObjForce = 50f;
    Vector3 equippedInstrumentPosOffset = new Vector3(0.05f, -0.005f,0.4f);
    Vector3 equippedInstrumenRotOffset = new Vector3(-15f, 0, -15);
    
	
	void Start () {
		OperationTheater.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		_UpdateApplicationLifecycle();
		if(SetupCompleted==false){
			ORSetup();
		}
		else{
            Touch touch;
            if(Input.touchCount<1 || (touch=Input.GetTouch(0)).phase != TouchPhase.Began){
                return;
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit)){
                GameObject Instrument = hit.collider.gameObject;
                InstrumentScript instrumentScript = Instrument.GetComponent<InstrumentScript>();

                if(Instrument.tag == "Instruments"){
                    if(Input.touchCount==2){
                        Debug.Log("Double Touch detected");
                        instrumentScript.isActive = instrumentScript.isActive? false:true;
                    }
                    else if(currentInstrument == Instrument){
                        Instrument.transform.SetParent(OperationTheater.transform);
                        currentInstrument = null;
                        instrumentScript.isEquipped = false;
                    }
                    else{
                        if(currentInstrument!=null){
                            currentInstrument.transform.SetParent(Instrument.transform.parent);
                            currentInstrument.transform.position = Instrument.transform.position;
                            currentInstrument.transform.rotation = Instrument.transform.rotation;
                            currentInstrument.GetComponent<InstrumentScript>().isEquipped=false;
                        }
                        Instrument.transform.SetParent(ARCamera.transform);
                        Instrument.transform.localPosition = equippedInstrumentPosOffset;
                        Instrument.transform.localEulerAngles = equippedInstrumenRotOffset;
                    
                        currentInstrument = Instrument;
                        instrumentScript.isEquipped = true;
                    }
                    
                }
               
            }
			
		}
	}

	void ORSetup(){
		Touch touch;
		if(Input.touchCount<1 || (touch=Input.GetTouch(0)).phase != TouchPhase.Began){
			return;
		}
		TrackableHit hit;
		TrackableHitFlags rayCastFilter = TrackableHitFlags.PlaneWithinPolygon;
		if (Frame.Raycast(touch.position.x, touch.position.y, rayCastFilter, out hit)){
			Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
			OperationTheater.SetActive(true);
			OperationTheater.transform.position = hit.Pose.position + GroundOffsetVector;
			OperationTheater.transform.SetParent(anchor.transform);
			SetupCompleted = true;
		}
	}

	private void _UpdateApplicationLifecycle()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Only allow the screen to sleep when not tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                const int lostTrackingSleepTimeout = 15;
                Screen.sleepTimeout = lostTrackingSleepTimeout;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (m_IsQuitting)
            {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
        }

        /// <summary>
        /// Actually quit the application.
        /// </summary>
        private void _DoQuit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Show an Android toast message.
        /// </summary>
        /// <param name="message">Message string to show in the toast.</param>
        private void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                        message, 0);
                    toastObject.Call("show");
                }));
            }
        }
}
