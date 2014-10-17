using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour {
	//this is the cooldown on the ability
	public int max_cooldown;

	//stores which method should be called from the stance
	public delegate void stance_delegate();

	// Use this for initialization
	void Start (int given_max_cooldown) {
		this.max_cooldown = given_max_cooldown;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void basic(){}
	public void attack(){}
	public void defend(){}
	public void support(){}
}
