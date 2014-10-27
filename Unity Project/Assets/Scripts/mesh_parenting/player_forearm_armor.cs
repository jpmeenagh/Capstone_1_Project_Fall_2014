using UnityEngine;
using System.Collections;

public class player_forearm_armor : MonoBehaviour {

	public GameObject armBone;

	// Use this for initialization
	void Start () {
	//armBone = this
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (armBone.transform.position.x, armBone.transform.position.y, armBone.transform.position.z);
	}
}
