using UnityEngine;
using System.Collections;

public class Phase : MonoBehaviour {

	public bool enabledB = false;
	Companion_Behavior comp_b;
	public float phaseSpeedInitial = 2.0f;
	public int nullPercent = 10;

	// Use this for initialization
	void Start () {
		comp_b = this.GetComponent<Companion_Behavior> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void enabler(){
		if (enabledB == false) {
			comp_b.speed = comp_b.speed + phaseSpeedInitial;
		}
		enabledB = true;
	}

	public void disabler(){
		if (enabledB == true) {
			comp_b.speed = comp_b.speed - phaseSpeedInitial;
		}
		enabledB = false;

	}

	public int dmgNull(int dmgIn){
		int dmgOut = dmgIn;
		int rollTMP = Random.Range (1, 101);
		if (rollTMP <= nullPercent){
			dmgOut = 0;
		}
		return dmgOut;
		}

}
