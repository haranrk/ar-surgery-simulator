using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagMaskBehaviour : MonoBehaviour {

	InstrumentScript instrumentScript;
	Animator anim;
	void Start () {
		instrumentScript = this.GetComponent<InstrumentScript>();
		anim = this.GetComponent<Animator>();
	}
	
	void Update () {
		// instrumentScript.isActive  = instrumentScript.isEquipped;
		anim.SetBool("isactive", instrumentScript.isActive);
		Debug.Log(string.Format("Anim:{0} Actual:{1}", anim.GetBool("isactive"), instrumentScript.isActive));
	}

}
