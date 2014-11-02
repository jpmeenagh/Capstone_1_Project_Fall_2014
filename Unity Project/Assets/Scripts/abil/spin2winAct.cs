using UnityEngine;
using System.Collections;

public class spin2winAct : MonoBehaviour {

	int lifeTimeAmt = 50;
	int spinDmg = 15;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		lifeTimeAmt = lifeTimeAmt - 1;
		if (lifeTimeAmt <= 0){
			Destroy(gameObject);
		}
	}

	//set dmg
	public void setPerams(int Dmg,int timeT){
		spinDmg = Dmg;
		lifeTimeAmt = timeT;
		}

	//apply dmg to effected enemies
	void OnTriggerEnter(Collider other){
		Health tempHP = other.GetComponent<Health> ();
		//makes sure hit object can take dmg
		if ( tempHP != null){
			//makes sure hit object is enemy
			if(tempHP.faction == Health.Faction.Enemy){
				tempHP.TakeDamage(spinDmg, this.transform.position, "Player");
				
			}
		}
		
	}
}
