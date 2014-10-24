using UnityEngine;
using System.Collections;

public class Ability_Flamethrower : Ability {
	public Rigidbody projectile;  //this should be hard coded probably
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	//attack stance effect.  Should only be called using this.stance_delegate
	protected override void attack(){
		print ("fix this");
		/*
			Rigidbody instantiatedProjectile = Instantiate(projectile, 
		                                               new Vector3(transform.position.x, transform.position.y, transform.position.z) 
		                                               + (10 * transform.forward),
		                                               Quaternion.identity) as Rigidbody;
		*/
		GameObject player_resource = Resources.Load<GameObject>("fire_mine");
		Instantiate(player_resource, this.transform.position - (2 * transform.forward), Quaternion.identity);
	}
	//defend stance effect.  Should only be called using this.stance_delegate
	protected override void defend(){


	}
	//support stance effect.  Should only be called using this.stance_delegate
	protected override void support(){
		print ("guac");
	}
}
