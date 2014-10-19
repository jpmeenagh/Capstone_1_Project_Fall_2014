using UnityEngine;
using System.Collections;

public class Ability_Flamethrower : Ability {

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	//attack stance effect.  Should only be called using this.stance_delegate
	protected override void attack(){
		print ("taco");
	}
}
