using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentScript : MonoBehaviour {

	float speed = 100;
	public bool isEquipped;
	public bool isActive;

	private Animator anim;

	void Start () {
		anim = this.GetComponent<Animator>();
		isEquipped = false;
		isActive = false;
	}
	void Update () {
		if(anim!=null){
			anim.SetBool("isactive", isActive);
			// Debug.Log(string.Format("Obj:{2} Anim:{0} Actual:{1}", anim.GetBool("isactive"), isActive, this.name));
		}		
	}

	public void RotateAnimation(){
		if(!isEquipped){
			transform.Rotate(Vector3.up*Time.deltaTime*speed, Space.World);
		}
	}
	public void Unequip(){
		isEquipped=false;
	}
}
