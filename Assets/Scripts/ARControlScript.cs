using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
public class ARControlScript : MonoBehaviour {

	public GameObject male;
	public GameObject ARCamera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Touch touch;
		if (Input.touchCount < 1 || ((touch=Input.GetTouch(0)).phase != TouchPhase.Began)){
			return;
		}	

		TrackableHit hit;
		if(Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit)){
			Anchor male_anchor = hit.Trackable.CreateAnchor(hit.Pose);
			male.SetActive(true);
			male.transform.position = hit.Pose.position;
			// male.transform.rotation = hit.Pose.rotation;
			male.transform.LookAt(ARCamera.transform);
			male.transform.SetParent(male_anchor.transform);
		}
	}
}
