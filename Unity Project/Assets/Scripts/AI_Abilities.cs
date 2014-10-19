using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AI_Abilities : MonoBehaviour {
	public List<Ability> abilities = new List<Ability>();
	// Use this for initialization
	void Start () {
		//new Ability_Flamethrower().omg (Ability.Stance.Attack);
		this.abilities.Add (new Ability_Flamethrower ());
	}
	
	// Update is called once per frame
	void Update () {
		print (Time.time);
		if(Time.time > 10){
			foreach (Ability x in this.abilities){
				x.change_stance(Ability.Stance.Attack);
				x.stance_delegate();
			}
		}
	
	}
}
