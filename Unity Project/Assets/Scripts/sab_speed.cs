using UnityEngine;
using System.Collections;

public class sab_speed : MonoBehaviour {

	//time left for sabotage
	int saboTime;

	// Use this for initialization
	void Start () {
		saboTime = 0;
	
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

	public void sabo_speed(int saboInTime){
		//interect with speed in AI
		saboTime = saboInTime;
		}

	void sabofall(){
		//undo sabo_speed effect
		}


}
