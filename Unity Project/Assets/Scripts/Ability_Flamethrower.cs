﻿using UnityEngine;
using System.Collections;

public class Ability_Flamethrower : Ability {
	public float defense_duration = 5f;
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
	

	//attack stance effect.  Should only be called using this.stance_delegate
	protected override int attack(){
		float nextActionTime = 0.0f;
		float period = 1f;
		while (this.defense_duration > 0) {
			if (Time.time > nextActionTime) { 
				nextActionTime += period;
				print ("pasta!");
			}
			this.defense_duration = this.defense_duration - 1;
		}



		return max_cooldown_attack;
	}
	//defend stance effect.  Should only be called using this.stance_delegate
	protected override int defend(){
		//circles of fire on the ground around the player and companion that hurt bad guys within every second

		return max_cooldown_defend;
	}
}
