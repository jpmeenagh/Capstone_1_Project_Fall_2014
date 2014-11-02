using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon_Missile : MonoBehaviour {
	//how much damage does this do
	public int damage = 50;

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

	// Use this for initialization
	void Start () {
		this.following_object = GameObject.FindWithTag (following_tags [target]);
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position =  new Vector3(this.following_object.transform.position[0], this.transform.position[1], this.following_object.transform.position[2]);
		this.transform.rotation = this.following_object.transform.rotation;
	//	if (Time.time >= this.time_trigger_is_ready) { 
	//		this.duration = this.duration - 1;
	//		print ("Missile:  UPDATE  |  time left:  " + this.duration);
	//	}
		
		if (this.duration == 0) {
			Destroy(this.gameObject);
		}
	}
}
