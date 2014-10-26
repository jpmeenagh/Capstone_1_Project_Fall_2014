using UnityEngine;
using System.Collections;

public class Ability_Sabotage : Ability {

	/*
	 * relevent for flamethrower
	int coneSpamTime;
	int coneSpamCount;
	*/
	public GameObject cone_colider;
	GameObject cone_aoe;

	public int saboStr;
	public int sabotDur;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void attack(){
		cone_aoe = (GameObject)Instantiate (cone_colider, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation);
		cone_col cone_col_tmp = cone_aoe.GetComponent<cone_col>();
		cone_col_tmp.setPerams (saboStr, sabotDur, 1.0f);
	}

	protected override void defend(){
		cone_aoe = (GameObject)Instantiate (cone_colider, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation);
		cone_col cone_col_tmp = cone_aoe.GetComponent<cone_col>();
		cone_col_tmp.setPerams (saboStr, sabotDur, 1.1f);
	}

	protected override void support(){

	}
}
