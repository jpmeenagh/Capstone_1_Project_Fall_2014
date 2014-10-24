using UnityEngine;
using System.Collections;

public class cone_col : MonoBehaviour {

	public int lifeTime = 100;
	int scaleTracker;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		lifeTime = lifeTime - 1;
		if (lifeTime == 0) {
			//this.
				}
		this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z + 1.00f);
		//scaleTracker = scaleTracker + 1;
		//this.transform.localScale.z = scaleTracker;
	}
}
