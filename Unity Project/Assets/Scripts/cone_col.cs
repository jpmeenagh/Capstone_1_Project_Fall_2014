using UnityEngine;
using System.Collections;

public class cone_col : MonoBehaviour {

	public int lifeTime = 100;
	//mode for cone collider
	//1.0 = sabotage offensive
	//1.1 = sabotage defensive
	//1.2 = sabotage support
	//2 = flamethrower
	public float mode = 0.0f;

	public int abilStr;
	public int abilTime;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		lifeTime = lifeTime - 1;
		//destroy cone when lifetime expires
		if (lifeTime == 0) {
			//destroy
			Destroy(this.gameObject);
				}
		this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z + 1.00f);
	}

	//setter for perameters of cone to determin behavior
	public void setPerams(int strn, int inTime, float modec){
		abilStr = strn;
		abilTime = inTime;
		mode = modec;
		}

	//applies effect to enimies based on what is using the cone
	void OnTriggerEnter(Collider other){
		if (mode == 1.0f) {
			dmg_in_mod_robo roboIn = other.GetComponent<dmg_in_mod_robo>();
			if ( roboIn != null){
				roboIn.sabo_def(abilTime, abilStr);
				}
			}
		if (mode == 1.1f) {
			sab_speed sabSpeedTmp = other.GetComponent<sab_speed>();
			if (sabSpeedTmp != null){
				sabSpeedTmp.sabo_speed(abilTime);
				}
			}

		}


}
