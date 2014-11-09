using UnityEngine;
using System.Collections;

public class cone_col : MonoBehaviour {
	//these are the different stances that determine which effect this ability has
	public enum Stance {Attack, Defend, Support};
	
	//this should be changed in and only in change_stance(...)
	public Stance stance;
	

	private int ability_strength;
	private int ability_time;


	//how long does this object exist?
	private int duration;
	
	//used to track time
	private float time_tick;
	
	
	
	// Use this for initialization
	void Start () {
		time_tick = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		print (duration);
		//decrease its life
		if (Time.time >= time_tick) { 
			time_tick = Time.time + 1;
			duration--;
			print ("cone_col:  UPDATE  |  time left:  " + duration + "  trigger ready:  " + ability_strength);
		}

		//destroy cone when duration expires
		if (duration == 0) {
			Destroy(this.gameObject);
		}

		//stretch the mesh out towards the front by scaling it
		this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z + 1.00f);
	}
	
	//sets parameters of the cone to determine its behavior
	public void setPerams(int g_strength, int g_ability_time, int g_duration, Stance g_mode){
		ability_strength = g_strength;
		ability_time = g_ability_time;
		print ("holy shit fuck:  " + g_duration);
		duration = g_duration;
		stance = g_mode;
	}
	
	//applies effect to enimies based on what is using the cone
	void OnTriggerEnter(Collider other){
		if (stance == Stance.Attack) {
			dmg_in_mod_robo roboIn = other.GetComponent<dmg_in_mod_robo>();
			if ( roboIn != null){
				roboIn.sabo_def(ability_time, ability_strength);
			}
		}
		if (stance == Stance.Defend) {
			sab_speed sabSpeedTmp = other.GetComponent<sab_speed>();
			if (sabSpeedTmp != null){
				sabSpeedTmp.sabo_speed(ability_time);
			}
		}
		
	}
	
	
}
