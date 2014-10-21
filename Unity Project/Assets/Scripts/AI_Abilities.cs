using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AI_Abilities : MonoBehaviour {
	public Ability[] abilities;
	bool temp;
	// Use this for initialization
	void Start () {
		this.temp = true;
		//get all the abilities in this object
		abilities = this.GetComponents<Ability> ();
		foreach (Ability element in this.abilities){
			print (element);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//print (Time.time);
		if((Time.time > 2) && this.temp){
			this.temp = false;
			print ("abilities activated");
			foreach (Ability element in this.abilities){
				element.change_stance(Ability.Stance.Attack);
				element.stance_delegate();
			}

		}

	}
}
