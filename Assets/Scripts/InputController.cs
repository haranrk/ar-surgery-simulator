using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
public class InputController : MonoBehaviour {

	public GameObject maleHuman;
	public GameObject SurgicalDevice;
	public float offset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var touch in Input.touches){
			Shoot(touch.position);
			if (Input.touchCount >1 && touch.phase == TouchPhase.Began){
				GameObject.Instantiate(maleHuman, transform.position + 0.3f*transform.forward, Random.rotation);
			}
		}
		// SurgicalDeviceOrientationUpdate();
	}

	void SurgicalDeviceOrientationUpdate(){
		SurgicalDevice.transform.rotation = transform.rotation;
		SurgicalDevice.transform.Rotate(90,0,0);
		SurgicalDevice.transform.position = transform.position + offset*transform.forward;
	}
	void Shoot(Vector2 screenPoint){
		var ray  = Camera.main.ScreenPointToRay(screenPoint);
		var hitInfo = new RaycastHit();
		if (Physics.Raycast(ray, out hitInfo)){
			hitInfo.rigidbody.AddForceAtPosition(ray.direction,hitInfo.point);
		}
	}
}
