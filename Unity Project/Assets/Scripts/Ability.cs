using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour {
	//this is the cooldown on the ability
	public int max_cooldown_attack = 0;
	public int max_cooldown_defend = 0;
	public int max_cooldown_support = 0;

	public string name;
	
	//these are the different stances that determine which effect this ability has
	public enum Stance {Attack, Defend, Support};

	//this should be changed in and only in change_stance(...)
	protected Stance stance;

	//stores which method should be called from the stance
	public delegate int Ability_Delegate();
	public Ability_Delegate stance_delegate;

	// Use this for initialization
	void Start (Stance given_stance) {
		this.stance = given_stance;
	}
	
	// Update is called once per frame
	void Update () {}

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
		else if(this.stance == Stance.Support){
			this.stance_delegate = new Ability_Delegate (support);
		}
	}
	
	//attack stance effect.  Should only be called using this.stance_delegate
	protected virtual int attack(){
		print ("confetti");
		return max_cooldown_attack;
	}
	//defend stance effect.  Should only be called using this.stance_delegate
	protected virtual int defend(){
		print ("cake");
		return max_cooldown_defend;
	}
	//support stance effect.  Should only be called using this.stance_delegate
	protected virtual int support(){
		print ("support");
		return max_cooldown_support;
	}
}
