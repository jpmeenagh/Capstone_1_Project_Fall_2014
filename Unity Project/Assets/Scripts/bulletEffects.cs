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
		/*
		if(otherObj.gameObject.tag == "Enemy")
    		{
			// Try and find an EnemyHealth script on the gameobject hit.
			EnemyHealth enemyHealth = otherObj.GetComponent <EnemyHealth> ();
			
			// If the EnemyHealth component exist...
			if(enemyHealth != null)
			{
				// ... the enemy should take damage.
				enemyHealth.TakeDamage (50, new Vector3(0,0,0));
			}
        	
 		}
	*/
		Destroy(this.gameObject);

	

	}

	/*void OnTriggerEnter (Collider trig){
		if (trig.name.Equals("bb_shi")) {
			Destroy(this.gameObject);
				}
		}*/

}