using UnityEngine;
using System.Collections;


public class OLDEnemy_Behavior : MonoBehaviour {
	
	
	public Transform target;
	public Transform aimTarget;
	public float speed = 3.0f;
	public Rigidbody projectile;
    public float bulletSpeed;
	public Transform shooter;
	public float fireRate;
	float nextFire = 0.0f;	
	public int approach;
	public int range;
	float tmpspeed;
	Collider tmpCol1;
	bool trigger1 = false;

	
	// Use this for initialization
	void Start () {
		tmpspeed = speed;
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 aim = aimTarget.transform.position;
		Vector3 move = target.transform.position;
		transform.LookAt(aim);
		float distanceToAim = Vector3.Distance (aim, transform.position);
		float distanceToMove = Vector3.Distance (move, transform.position);

		//check to see if tmpCpl1 still exists
		if (trigger1) {
			if (tmpCol1 != null){}
			else{
				speed = tmpspeed;
				trigger1 = false;
				}
		}

		//move twards player if out of range
		if (distanceToMove > approach || distanceToMove < -approach){
			transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
		}

		//shoot if in range
 		if (distanceToAim < range && distanceToAim > -range && Time.time > nextFire)
        	{
		nextFire = Time.time + fireRate;
				Rigidbody instantiatedProjectile = Instantiate(projectile, new Vector3(shooter.position.x, shooter.position.y, shooter.position.z),shooter.rotation) as Rigidbody;
            	instantiatedProjectile.velocity = shooter.TransformDirection(new Vector3(0, 0,bulletSpeed));

        	}
	}

	//stops the robot when it enters robot impasible terrain
	void OnTriggerEnter(Collider othObj){
		if (othObj.gameObject.tag == "RoboImpas"){
			tmpspeed = speed;
			speed = 0.0f;
			tmpCol1 = othObj;
			trigger1 = true;
		}
		}

	/*//starts the robot when it leaves robot imasible terrain
	void OnTriggerExit(Collider othObj){
		if (othObj.gameObject.tag == "RoboImpas") {
			speed = tmpspeed;
				}
		}*/
}
