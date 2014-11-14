using UnityEngine;
using System.Collections;

public class Ability_Sabotage : Ability {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override int attack(){
		GameObject cone_collider = Resources.Load<GameObject>("sabotage_cone_attack");
		Instantiate (cone_collider, transform.position, transform.rotation);
		return max_cooldown_attack;
	}

	protected override int defend(){

		return max_cooldown_defend;
	}

	protected override int support(){

		GameObject cone_collider = Resources.Load<GameObject>("sabotage_cone_defend");
		Instantiate (cone_collider, transform.position, transform.rotation);
		return max_cooldown_support;
	}
}
