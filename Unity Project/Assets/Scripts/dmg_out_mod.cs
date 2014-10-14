using UnityEngine;
using System.Collections;

public class dmg_out_mod : MonoBehaviour {

	int dmgModCur = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//takes outgoing dmg and does final number modifications before its sent to target
	public int modDmg (int defDam){
		int outDam;
		outDam = defDam + dmgModCur;
		return outDam;
	}

	//changes the curreny damage mod bu adding the given ammount
	public void changeMod (int change){
		dmgModCur = dmgModCur + change;
		}

}
