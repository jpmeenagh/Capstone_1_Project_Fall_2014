using UnityEngine;
using System.Collections;

public class Ability_Flamethrower : Ability {

	public string name = "Flamethrower";

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
	

	//attack stance effect.  Should only be called using this.stance_delegate
	protected override int attack(){
		GameObject mine1 = Resources.Load<GameObject>("flame");
		mine1.GetComponent<Weapon_Firemine> ().target = Weapon_Firemine.Following.Player;
		Instantiate(mine1, new Vector3(0,1,0), Quaternion.identity);


		return max_cooldown_attack;
	}
	//circles of fire on the ground around the player and companion that hurt bad guys within every second
	protected override int defend(){
		GameObject mine1 = Resources.Load<GameObject>("fire_mine");
		mine1.GetComponent<Weapon_Firemine> ().target = Weapon_Firemine.Following.Player;
		Instantiate(mine1, new Vector3(0,1,0), Quaternion.identity);
		
		GameObject mine2 = Resources.Load<GameObject>("fire_mine");
		mine2.GetComponent<Weapon_Firemine> ().target = Weapon_Firemine.Following.Companion;
		Instantiate(mine2, new Vector3(0,1,0), Quaternion.identity);

		return max_cooldown_defend;
	}
}
