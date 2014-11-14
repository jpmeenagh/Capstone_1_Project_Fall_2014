using UnityEngine;
using System.Collections;

public class Ability_Heal : Ability {

	animation_ctrl_comp animcomp;
	// Use this for initialization
	void Start () {
		animcomp = GetComponent<animation_ctrl_comp> ();

	}
	void Awake(){
		this.name = "Heal";
	}
	
	// Update is called once per frame
	void Update () {}
	
	
	//attack stance effect.  Should only be called using this.stance_delegate
	protected override int attack(){

		return max_cooldown_attack;
	}
	//defend stance effect.  Should only be called using this.stance_delegate
	protected override int defend(){
		GameObject station = Resources.Load<GameObject>("health_station");
		Instantiate(station, transform.position, Quaternion.identity);
		animcomp.animAbil ("sabo");
		return max_cooldown_defend;
	}
	//support stance effect.  Should only be called using this.stance_delegate
	protected override int support(){
		GameObject player_resource = Resources.Load<GameObject>("missile");
		Instantiate(player_resource, this.transform.position - (2 * transform.forward), Quaternion.identity);
		animcomp.animAbil ("sabo");
		return max_cooldown_support;
	}
}