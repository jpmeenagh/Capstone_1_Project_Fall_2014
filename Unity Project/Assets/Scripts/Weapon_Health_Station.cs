using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon_Health_Station : MonoBehaviour {
	//how much damage does this do per second?
	public int health_per_second = 10;
	
	//how long does this exist?
	public int duration = 15;
	
	//used to count the next second
	private float time_next_second;
	//when the trigger is armed to do stuff again
	private float time_trigger_armed;
	
	//this is the time before triggering again, in seconds
	private float time_between_triggers = 1f;
	
	void Start () {
		this.time_next_second = Time.time;
		this.time_trigger_armed = Time.time;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= this.time_next_second) { 
			time_next_second = Time.time + 1;
			this.duration = this.duration - 1;
			print ("Firemine:  UPDATE  |  time left:  " + this.duration + "  trigger ready:  " + this.time_trigger_armed);
		}
		
		if (this.duration == 0) {
			Destroy(this.gameObject);
		}
	}
	
	void OnTriggerStay(Collider other) {
		//if it hit something tagged as the player or companion and it's ready to trigger
		if ((other.CompareTag ("Player") || other.CompareTag("Companion")) && (Time.time >= this.time_trigger_armed)) {
			print ("UPDATE | health station | HIT | tag: " + other.tag + " | time: " + Time.time);

			//set the next time it should be ready to trigger
			time_trigger_armed = Time.time + time_between_triggers;
			
			//try and find an friendly_health script on the gameobject hit.
			Health friendly_health = other.GetComponent <Health> ();
			//if the friendly_health component exists
			if (friendly_health != null) {
				//the thing in the trigger should heal
				friendly_health.Heal (health_per_second, transform.position);
			}
		}
	}
}