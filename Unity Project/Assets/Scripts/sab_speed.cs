using UnityEngine;
using System.Collections;

public class sab_speed : MonoBehaviour {
	//put this on the enemy

	//time left for sabotage
	int saboTime;

	//stores the amount of speed changed 
	float speedChange;

	Enemy_Behavior en_b;

	// Use this for initialization
	void Start () {
		saboTime = 0;
		en_b = this.GetComponent<Enemy_Behavior> ();
	
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

	//interect with speed in AI
	public void sabo_speed(int saboInTime){
		speedChange = en_b.speed * (3.0f / 4.0f);
		en_b.speed = en_b.speed - speedChange;
		saboTime = saboInTime;
		}

	void sabofall(){
		//undo sabo_speed effect
		en_b.speed = en_b.speed + speedChange;
		}


}
