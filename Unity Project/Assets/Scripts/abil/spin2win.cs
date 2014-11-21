using UnityEngine;
using System.Collections;

public class spin2win : MonoBehaviour {

	//4
	//public AudioClip meleeSound;	
	AudioSource[] sounds;
	AudioSource spinSoundSource;

	//collider
	public GameObject spinZone;
	GameObject spinZoneNow;
	//for dmg calculations
	public int spinDmg = 15;
	dmg_out_mod_player thisOutDmg;
	//spin duration
	public int spinTime = 50;
	//min cd for anti spam
	public int minCD = 10;
	int curCD = 0;

	// Use this for initialization
	void Start () {
		sounds = this.GetComponents<AudioSource>();
		spinSoundSource = sounds[4];
	}
	
	// Update is called once per frame
	void Update () {

		//spins when key is pressed
		if(Input.GetButton ("spinKey")){
			if (curCD <= 0){
				spin ();
			}
		}
		if (curCD > 0) {
			curCD = curCD - 1;
				}
	}

	//called to preform wirlwind atk
	void spin (){
		spinZoneNow = (GameObject)Instantiate (spinZone, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation);
		spin2winAct spinScript = spinZoneNow.GetComponent<spin2winAct> ();
		spinScript.setPerams (spinDmg, spinTime);
		curCD = minCD;
		spinSoundSource.Play ();
		//animation code here....

	}



}
