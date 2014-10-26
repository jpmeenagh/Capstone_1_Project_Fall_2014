﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon_Firemine : MonoBehaviour {
	//how much damage does the do per second?
	public int damagePerSecond = 10;

	//how long does this exist?
	public int duration = 15;

	//what can this follow and what is the tag of the option?
	public enum Following {Player, Companion};

	//tags associated with different following options
	protected Dictionary<Following, string> following_tags = new Dictionary<Following, string>(){{Following.Player, "Player"}, {Following.Companion, "Companion"}};
	
	//what is this actually following
	public Following target = Following.Player;

	//the object it is following
	private GameObject following_object;

	//this is used as a timer for when to do OnTriggerStay stuff again
	private float nextActionTime;

	//this is the time before triggering again, in seconds
	private float period = 1f;

	//should OnTriggerStay do stuff?
	private bool cause_harm = true;

	void Start () {
		this.following_object = GameObject.FindWithTag (following_tags [target]);
		this.nextActionTime = Time.time;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		this.transform.position =  new Vector3(this.following_object.transform.position[0], this.transform.position[1], this.following_object.transform.position[2]);

		if (Time.time > this.nextActionTime) { 
			nextActionTime += period;
			this.cause_harm = true;
			this.duration = this.duration - 1;
			print ("Firemine time left:  " + this.duration);
		}

		if (this.duration == 0) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerStay(Collider other) {
		if (!(other.CompareTag (following_tags [target])) && this.cause_harm) {
			print ("Firemine:  hit tag:  " + other.tag);

			// Try and find an EnemyHealth script on the gameobject hit.
			Health enemyHealth = other.GetComponent <Health> ();

			// If the EnemyHealth component exist...
			if (enemyHealth != null) {
				if (enemyHealth.faction != this.following_object.GetComponent<Health> ().faction) {
					// ... the enemy should take damage.
					//enemyHealth.TakeDamage (this.following_object.GetComponent<dmg_out_mod_player>().modDmg(damagePerSecond), new Vector3(0f,0f,0f), following_tags[target]);
					enemyHealth.TakeDamage (10, new Vector3 (0f, 0f, 0f), following_tags [target]);
				}
			}
			this.cause_harm = false;
		}
	}
}