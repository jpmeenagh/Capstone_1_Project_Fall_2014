using UnityEngine;
using System.Collections;

public class bb_shi_atc : MonoBehaviour {

	public int duration = 100;
	int curDur = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		curDur = curDur + 1;
		if (curDur > duration) {
			Destroy(gameObject);
				}
	}

	//destroys a bullet when it enters the field
	void OnTriggerEnter (Collider othObj){
		if (othObj.gameObject.tag == "Bullet") {
			Destroy(othObj.gameObject);
				}
		}

}
