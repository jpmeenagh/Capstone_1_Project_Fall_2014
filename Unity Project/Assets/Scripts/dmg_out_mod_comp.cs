using UnityEngine;
using System.Collections;

public class dmg_out_mod_comp : MonoBehaviour {

	critical_point thisCritPoint;

	// Use this for initialization
	void Start () {
		thisCritPoint = this.GetComponent<critical_point> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//modifies outgoing dmg
	public int modDmg(int inDmg){
		int outDmg = inDmg;
		outDmg = thisCritPoint.crit_point_multi (outDmg);
		return outDmg;
		}
}
