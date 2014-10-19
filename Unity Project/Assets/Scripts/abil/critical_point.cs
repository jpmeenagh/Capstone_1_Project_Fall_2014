using UnityEngine;
using System.Collections;

public class critical_point : MonoBehaviour {

	Health thisHp; 

	// Use this for initialization
	void Start () {
		thisHp = this.GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int crit_point_multi (int inDmg){
		//caulates crit multiplyer, max output is 2xinput, min ouput is 1xinput
		return (inDmg + inDmg*(1 -(thisHp.currentHealth/thisHp.startingHealth)));
		}
}
