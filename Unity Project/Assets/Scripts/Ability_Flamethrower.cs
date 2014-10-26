using UnityEngine;
using System.Collections;

public class Ability_Flamethrower : Ability {
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
	

	//attack stance effect.  Should only be called using this.stance_delegate
	protected override int attack(){
		GameObject player_resource = Resources.Load<GameObject>("fire_mine");
		Instantiate(player_resource, new Vector3(0,1,0), Quaternion.identity);


		return max_cooldown_attack;
	}
	//defend stance effect.  Should only be called using this.stance_delegate
	protected override int defend(){
		//circles of fire on the ground around the player and companion that hurt bad guys within every second

		return max_cooldown_defend;
	}
}
