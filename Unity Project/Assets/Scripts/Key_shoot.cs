using UnityEngine;
using System.Collections;

public class Key_shoot : MonoBehaviour {
	public string attack_ranged_ray_key = "Fire1";
	public string attack_melee_ray_key = "Fire2";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// If the Fire1 button is being press and it's time to fire...
		if(Input.GetButton (attack_ranged_ray_key))
		{
			// ... shoot the gun.
			Attack_Ranged_Ray rayranged = this.GetComponent <Attack_Ranged_Ray>();
			rayranged.Shoot();
		}
		// If the Fire1 button is being press and it's time to fire...
		if(Input.GetButton (attack_melee_ray_key))
		{
			// ... shoot the gun.
			Attack_Melee_Ray raymelee = this.GetComponent <Attack_Melee_Ray>();
			raymelee.Shoot();
		}
	}
}
