using UnityEngine;
using System.Collections;

public class Frenzy_suit : MonoBehaviour {

	dmg_out_mod_player dmgOut;
	int curFrz;
	public int frzTime = 100;
	int frzTimeRem;
	public int frzDmg = 10;

	// Use this for initialization
	void Start () {
		dmgOut = this.GetComponent<dmg_out_mod_player> ();
		curFrz = 0;
		frzTimeRem = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//decreases time on current stacks and calls falloff if time has run out
		if (frzTimeRem > 0){
				frzTimeRem = frzTimeRem - 1;
			}

		if (frzTimeRem <= 0) {
				frzFallOff ();
			}
	}

	//on kill refreshes time, adds more dmg, and increments the internal counter
	public void gotKill(){
		frzTimeRem = frzTime;
		dmgOut.changeMod (frzDmg);
		curFrz = curFrz + frzDmg;
		//print ("frz inc" + curFrz);
	}

	//called when time runs out, un-modifies dmg, and resets internal count
	void frzFallOff(){
		if (curFrz > 0) {
					dmgOut.changeMod (-curFrz);
					curFrz = 0;
					//print ("fall");
			}
	
	}
}
