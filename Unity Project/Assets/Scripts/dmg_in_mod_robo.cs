using UnityEngine;
using System.Collections;

public class dmg_in_mod_robo : MonoBehaviour {

	//stores current in dmg modifyer
	int dmgInModCur;

	//variabels keeping track of sabotage
	int saboTime;
	int saboStrCur;


	// Use this for initialization
	void Start () {
		dmgInModCur = 0;
		saboTime = 0;
		saboStrCur = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//if sabotage is active time it till it falls off
		if (saboTime > 0) {
			saboTime = saboTime - 1;
			if (saboTime == 0){
				sabofall();
				}
			}

	}

	//called to modify incomming dmg
	public int modDmg(int dmgIn){
		int outDmg;
		outDmg = dmgIn + dmgInModCur;
		return outDmg;

		}

	//called by companion sabotage ability
	public void sabo_def (int saboInTime, int saboStr){
		saboTime = saboInTime;
		dmgInModCur = dmgInModCur + saboStr;
		saboStrCur = saboStr;

		}

	
	//called when sabotage ends to cleans the effects
	void sabofall(){
		dmgInModCur = dmgInModCur - saboStrCur;
		saboStrCur = 0;
		
	}



}
