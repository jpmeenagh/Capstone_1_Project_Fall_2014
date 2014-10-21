using UnityEngine;
using System.Collections;

public class dmg_out_mod_robo : MonoBehaviour {

	int dmgOutModCur;

	// Use this for initialization
	void Start () {
		dmgOutModCur = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//modifies outgoing dmg
	public int modDmg(int dmgIn){
		int dmgOutRet;
		dmgOutRet = dmgIn + dmgOutModCur;
		return dmgOutRet;
	}

	//changes the curreny damage mod by adding the given ammount
	public void changeMod (int change){
		dmgOutModCur = dmgOutModCur + change;
	}
}
