using UnityEngine;
using System.Collections;

public class dmg_in_mod_ally : MonoBehaviour {


	//stores current in dmg modifyer
	int dmgInModCur;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//called to modify incomming dmg
	public int modDmg(int dmgIn){
		int outDmg;
		outDmg = dmgIn + dmgInModCur;
		if(this.tag == "Companion"){
			//apply phase if applicable
			Phase phaTMP = this.GetComponent<Phase>();
			if (phaTMP.enabledB == true){
				outDmg = phaTMP.dmgNull(outDmg);
			}
		}
		return outDmg;
	}
}
