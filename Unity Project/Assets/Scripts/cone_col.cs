using UnityEngine;
using System.Collections;

public class cone_col : MonoBehaviour {
	//these are the different stances that determine which effect this ability has
	public enum Stance {Attack, Defend, Support};
	
	//this should be changed in and only in change_stance(...)
	public Stance stance;
	

	public int ability_strength;
	public int ability_time;
	
	//how long does this object exist?
	public int duration;
	
	//used to track time
	private float time_next_tick;

	// Use this for initialization
	void Start () {
		time_next_tick = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		//decrease its life
		if (Time.time >= time_next_tick) { 
			time_next_tick = Time.time + 1;
			duration--;
		}

		//destroy cone when duration expires
		if (duration == 0) {
			Destroy(this.gameObject);
		}
	}
	
	//applies effect to enimies based on what is using the cone
	void OnTriggerEnter(Collider other){
		if (stance == Stance.Attack) {
			dmg_in_mod_robo roboIn = other.GetComponent<dmg_in_mod_robo>();
			if ( roboIn != null){
				roboIn.sabo_def(ability_time, ability_strength);
			}
		}
		if (stance == Stance.Support) {
			sab_speed sabSpeedTmp = other.GetComponent<sab_speed>();
			if (sabSpeedTmp != null){
				sabSpeedTmp.sabo_speed(ability_time);
			}
		}
	}
}
