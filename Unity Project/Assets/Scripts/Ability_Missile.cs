using UnityEngine;
using System.Collections;

public class Ability_Missile : Ability {
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
	
	
	//attack stance effect.  Should only be called using this.stance_delegate
	protected override int attack(){
		GameObject player_resource = Resources.Load<GameObject>("fire_mine");
		Instantiate(player_resource, this.transform.position - (2 * transform.forward), Quaternion.identity);
		return max_cooldown_attack;
	}
	//defend stance effect.  Should only be called using this.stance_delegate
	protected override int defend(){
		print ("cake");
		return max_cooldown_defend;
	}
	//support stance effect.  Should only be called using this.stance_delegate
	protected override int support(){
		print ("support");
		return max_cooldown_support;
	}
}
