using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrodeScript : MonoBehaviour {

	private GameObject positiveParticleSystem;
	private GameObject negativeParticleSystem;
	public GameObject negativeElectrode;
	private InstrumentScript positiveInstrumentScript;
	private InstrumentScript negativeInstrumentScript;
	void Start () {
		positiveParticleSystem = this.transform.GetChild(0).gameObject;
		positiveInstrumentScript = this.GetComponent<InstrumentScript>();

		negativeParticleSystem = negativeElectrode.transform.GetChild(0).gameObject;
		negativeInstrumentScript = negativeElectrode.GetComponent<InstrumentScript>();
	}
	
	// Update is called once per frame
	void Update () {
		// Debug.Log(string.Format("Obj:{0} Active:{1}", this.gameObject.name, instrumentScript.isActive));
		if(positiveInstrumentScript.isActive || negativeInstrumentScript.isActive){
			// Debug.Log(string.Format("Active pos:{0} neg:{1}", positiveInstrumentScript.isActive, negativeInstrumentScript.isActive));
			positiveParticleSystem.SetActive(true);
			negativeParticleSystem.SetActive(true);
		}
		else{
			// Debug.Log(string.Format("Inacti pos:{0} neg:{1}", positiveInstrumentScript.isActive, negativeInstrumentScript.isActive));
			positiveParticleSystem.SetActive(false);
			negativeParticleSystem.SetActive(false);
		}
		
	}
}
