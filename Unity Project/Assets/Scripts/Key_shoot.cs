using UnityEngine;
using System.Collections;

public class Key_shoot : MonoBehaviour {
	public string heal_ranged_ray_key = "heal";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//uses triggers for shoot and melee
		if(Input.GetButton("Fire1")/*Input.GetAxis("triggerAxis") == 1*/){
			// ... shoot the gun.
			Attack_Ranged_Ray rayranged = this.GetComponent <Attack_Ranged_Ray>();
			rayranged.Shoot();
		}

		if(Input.GetButton("Fire2")/*Input.GetAxis("triggerAxis") == -1*/){
			// ... shoot the gun.
			Attack_Melee_Ray raymelee = this.GetComponent <Attack_Melee_Ray>();
			raymelee.Shoot();
		}

		//heals for healbutton
		if(Input.GetButton (heal_ranged_ray_key))
		{
			// ... shoot the gun.
			Heal_Ranged_Ray rayranged = this.GetComponent <Heal_Ranged_Ray>();
			rayranged.Shoot();
		}
	}
}
