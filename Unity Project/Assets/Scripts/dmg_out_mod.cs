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

	int modDmg (int defDam){
		int outDam;
		outDam = defDam + dmgModCur;
		return outDam;
	}

	void changeMod (int change){
		dmgModCur = dmgModCur + change;
		}

}
