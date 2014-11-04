using UnityEngine;
using System.Collections;



public class bulletEffects : MonoBehaviour {

	

	// Use this for initialization
	
	void Start () {
	
		//Destroy(this.gameObject, 3);
	
	
	}
	
	

	// Update is called once per frame
	
	void Update () {
	
	
	
	
	}
	
	
	void OnCollisionEnter(Collision otherObj)
	{

		if(otherObj.gameObject.tag == "Player" || otherObj.gameObject.tag == "Companion" || otherObj.gameObject.tag == "Enemy")
    		{
			// Try and find an EnemyHealth script on the gameobject hit.
			Health health = otherObj.transform.GetComponent <Health>();
			
			// If the EnemyHealth component exist...
			if(health != null)
			{
				print("hit: " + otherObj.gameObject.tag);
				// ... the enemy should take damage.
				health.TakeDamage (10 , new Vector3(0,0,0), "Bullet");
			}
        	
 		}
	
		Destroy(this.gameObject);

	

	}

	/*void OnTriggerEnter (Collider trig){
		if (trig.name.Equals("bb_shi")) {
			Destroy(this.gameObject);
				}
		}*/

}