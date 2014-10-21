using UnityEngine;
using System.Collections;


public class Companion_Behavior : MonoBehaviour {

	//companion vars
	public GameObject tether;
	public float tetherDistance;
	public int tetherRange = 10;

	//basic movement 
	public float speed = 3.0f;
	public bool moving;
	public Vector3 currentLocation;
	public Vector3 targetLocation;

	//targeting
	public Transform target;
	public float targetDistance;
	public int targetingRange = 100;
	public int approachRange = 7;
	public int targetTimer = 75;

	//shooting
	public int shootRange = 10;
	public float fireRate = 0.5F;
	float nextFire = 0.0f;	
	public Rigidbody projectile;
	public float bulletSpeed = 10;
	public Transform shooter;
	
	//andrew's bubble shield code
	float tmpspeed;
	Collider tmpCol1;
	bool trigger1 = false;
	

	// Use this for initialization
	void Start () {
		target = transform; //set target to self when not hostile
		targetLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		currentLocation = transform.position;
		targetDistance = Vector3.Distance (transform.position, target.position);
		tetherDistance = Vector3.Distance (transform.position, tether.transform.position);
		moving = Vector3.Distance(targetLocation, currentLocation) > 2; //if we have a target location, we are moving

		CheckCollider(); //check to see if tmpCpl1 still exists

		if (targetTimer <= 0) {	target = FindTargets ().transform; targetTimer = 75; } else { targetTimer--; } 

		if (tetherDistance > tetherRange) {
			if(target == transform){ transform.LookAt(tether.transform); }
			transform.position = Vector3.MoveTowards (transform.position, tether.transform.position, speed * Time.deltaTime); 
		} 

		//if (target == transform) { randomMovement(); } //if not hostile, move around randomly
		if (target != transform) {
			if(tetherDistance <= tetherRange){
				transform.LookAt (new Vector3 (target.transform.position.x, target.transform.position.y, target.transform.position.z));
				if (targetDistance > approachRange) {transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime);}
			}
			shoot(); //shoot when in range
		}
	
		if (target.GetComponent<Health> ().currentHealth <= 0) { target = transform; }
	}

	/*====================================================================
	======Functions======================================================
	====================================================================*/

	//shoot if in range
	void shoot(){
		if (targetDistance < shootRange && targetDistance > -shootRange && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			Rigidbody instantiatedProjectile = Instantiate(projectile, new Vector3(shooter.position.x, shooter.position.y, shooter.position.z),shooter.rotation) as Rigidbody;
			instantiatedProjectile.velocity = shooter.TransformDirection(new Vector3(0, 0,bulletSpeed));
		}
	}

	//Find a target
	GameObject FindTargets(){
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Robot");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance && curDistance < targetingRange && go.GetComponent<Health>().currentHealth > 0){
				closest = go;
				distance = curDistance;
			}
		}
		if (distance > targetingRange) { closest = gameObject; }
		return closest;
	}

	//move around randomly when targetless
	void randomMovement(){
		if (moving) {
			//if we have a target, look towards it and move in that direction at speed
		 	transform.LookAt (new Vector3(targetLocation.x, currentLocation.y, targetLocation.z));
			transform.position = Vector3.MoveTowards (transform.position, targetLocation, speed * Time.deltaTime);
		} else {
			//if we have no target(since we aren't moving), choose a random spot within 5 units to move towards
			targetLocation = new Vector3(currentLocation.x + Random.Range(-10.0F, 10.0F), currentLocation.y, currentLocation.z + Random.Range(-10.0F, 10.0F));
		}
	}

	//stops the robot when it enters robot impasible terrain, for bubble shield
	void OnTriggerEnter(Collider othObj){
		if (othObj.gameObject.tag == "RoboImpas"){
			tmpspeed = speed;
			speed = 0.0f;
			tmpCol1 = othObj;
			trigger1 = true;
		}
	}

	//check to see if tmpCpl1 still exists
	void CheckCollider(){
		if (trigger1) {
			if (tmpCol1 != null){}
			else{
				speed = tmpspeed;
				trigger1 = false;
			}
		}
	}

	/*//starts the robot when it leaves robot imasible terrain
	void OnTriggerExit(Collider othObj){
		if (othObj.gameObject.tag == "RoboImpas") {
			speed = tmpspeed;
		}
	}*/
}
