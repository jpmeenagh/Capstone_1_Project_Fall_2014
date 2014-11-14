using UnityEngine;
using System.Collections;

public class Ability_Sabotage : Ability {

	animation_ctrl_comp animcomp;

	// Use this for initialization
	void Start () {
		animcomp = GetComponent<animation_ctrl_comp> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override int attack(){
		GameObject cone_collider = Resources.Load<GameObject>("sabotage_cone_attack");
		Instantiate (cone_collider, transform.position, transform.rotation);
		animcomp.animAbil ("sabo");
		return max_cooldown_attack;
	}

	protected override int defend(){

		return max_cooldown_defend;
	}

	protected override int support(){

		GameObject cone_collider = Resources.Load<GameObject>("sabotage_cone_defend");
		Instantiate (cone_collider, transform.position, transform.rotation);
		animcomp.animAbil ("sabo");
		return max_cooldown_support;
	}
}
