using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningPositionScript : MonoBehaviour {

	public GameObject electrode;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = electrode.transform.position;
	}
}
