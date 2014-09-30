using UnityEngine;
using System.Collections;

public class char_rot : MonoBehaviour {

	public float sens = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//rotates the player using the right stick
		transform.Rotate(0, Input.GetAxis("char_rot") * sens/* * sensitivityX */, 0);
	
	}
}
