using UnityEngine;
using System.Collections;

public class Ability_Sabotage : Ability {

	//public AudioClip meleeSound;	
	AudioSource[] sounds;
	AudioSource weakSoundSource;
	AudioSource slowSoundSource;

	animation_ctrl_comp animcomp;

	// Use this for initialization
	void Start () {
		animcomp = GetComponent<animation_ctrl_comp> ();

		sounds = this.GetComponents<AudioSource>();
		weakSoundSource = sounds[1];
		slowSoundSource = sounds[2];
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override int attack(){
		GameObject cone_collider = Resources.Load<GameObject>("sabotage_cone_attack");
		Instantiate (cone_collider, transform.position, transform.rotation);
		animcomp.animAbil ("sabo");
		weakSoundSource.Play ();
		return max_cooldown_attack;
	}

	protected override int defend(){

		return max_cooldown_defend;
	}

	protected override int support(){

		GameObject cone_collider = Resources.Load<GameObject>("sabotage_cone_defend");
		Instantiate (cone_collider, transform.position, transform.rotation);
		animcomp.animAbil ("sabo");
		slowSoundSource.Play ();
		return max_cooldown_support;
	}
}
