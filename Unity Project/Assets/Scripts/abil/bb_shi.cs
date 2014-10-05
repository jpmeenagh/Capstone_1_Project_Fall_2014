using UnityEngine;
using System.Collections;

public class bb_shi : MonoBehaviour {

	public string bb_shiKey = "Fire3";
	public Transform player;
	public Rigidbody shi;
	public int shieldCD = 100;
	int shieldTime = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetButtonDown (bb_shiKey))&& shieldTime == 0) {
			layShi_bb();
				}
		if (shieldTime > 0){
			shieldTime = shieldTime + 1;
		}
		if (shieldTime > shieldCD) {
			shieldTime = 0;
				}
	}

	void layShi_bb(){
		Instantiate (shi, new Vector3(player.position.x,player.position.y,player.position.z), player.rotation);
		shieldTime = 1;
		}
}
