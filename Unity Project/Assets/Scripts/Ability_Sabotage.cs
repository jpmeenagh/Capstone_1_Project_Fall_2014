using UnityEngine;
using System.Collections;

public class Ability_Sabotage : Ability {

	/*
	 * relevent for flamethrower
	int coneSpamTime;
	int coneSpamCount;
	*/

	public int strength;
	public int ability_time;
	public int duration;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override int attack(){
		GameObject cone_collider = Resources.Load<GameObject>("sabotage_cone");
		cone_collider.GetComponent<cone_col>().setPerams (strength, ability_time, duration, cone_col.Stance.Attack);
		Instantiate (cone_collider, transform.position, transform.rotation);
		return max_cooldown_attack;
	}

	protected override int defend(){
		GameObject cone_collider = Resources.Load<GameObject>("sabotage_cone");
		cone_collider.GetComponent<cone_col>().setPerams (strength, ability_time, duration, cone_col.Stance.Defend);
		Instantiate (cone_collider, transform.position, transform.rotation);
		return max_cooldown_defend;
	}

	protected override int support(){
		return max_cooldown_support;
	}
}
