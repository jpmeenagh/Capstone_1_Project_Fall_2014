using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour {
	//this is the cooldown on the ability
	public int max_cooldown;

	//these are the different stances that determine which effect this ability has
	public enum Stance {Attack, Defend};

	//this should be changed in and only in change_stance(...)
	protected Stance stance;

	//stores which method should be called from the stance
	public delegate void Ability_Delegate();
	public Ability_Delegate stance_delegate;

	// Use this for initialization
	void Start (int given_max_cooldown, Stance given_stance) {
		this.max_cooldown = given_max_cooldown;
	}
	
	// Update is called once per frame
	void Update (Stance given_stance) {
		change_stance (given_stance);
	}

	//change stance and set the function pointer to the mode it should be using now
	public void change_stance(Stance given_stance){
		//set the stance
		this.stance = given_stance;

		//set the delegate
		if(this.stance == Stance.Attack){
			this.stance_delegate = new Ability_Delegate (attack);
		}
		else if(this.stance == Stance.Defend){
			this.stance_delegate = new Ability_Delegate (defend);
		}
	}
	
	//attack stance effect.  Should only be called using this.stance_delegate
	protected virtual void attack(){
		print ("confetti");
	}
	//defend stance effect.  Should only be called using this.stance_delegate
	protected virtual void defend(){
		print ("cake");
	}
	//support stance effect.  Should only be called using this.stance_delegate
	protected virtual void support(){
		print ("support");
	}
}
