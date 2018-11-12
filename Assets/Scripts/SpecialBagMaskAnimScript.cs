using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBagMaskAnimScript : MonoBehaviour {

	InstrumentScript m_Script;
	Animator anim;
	// Use this for initialization
	void Start () {
		m_Script = this.transform.parent.gameObject.GetComponent<InstrumentScript>();
		anim = this.GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("isactive", m_Script.isActive);
	}
}
