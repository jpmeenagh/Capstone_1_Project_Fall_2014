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
		Rigidbody instantiatedProjectile = Instantiate(projectile, 
		                                               new Vector3(transform.position.x, transform.position.y, transform.position.z) 
		                                               + (10 * transform.forward),
		                                               Quaternion.identity) as Rigidbody;
	}
	//defend stance effect.  Should only be called using this.stance_delegate
	protected override void defend(){
		print ("burrito");
	}
	//support stance effect.  Should only be called using this.stance_delegate
	protected override void support(){
		print ("guac");
	}
}
