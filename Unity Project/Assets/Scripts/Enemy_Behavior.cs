using UnityEngine;
using System.Collections;


public class Enemy_Behavior : MonoBehaviour {

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
	public int targetTimer = 30;
	public int nearbyEnemies;
	public float nearbyRange = 4;

	//shooting
	public int shootRange = 10;
	public float fireRate = 1;
	float nextFire = 0.0f;	
	public Rigidbody projectile;
	public float bulletSpeed = 10;
	public Transform shooter;
	
	//andrew's bubble shield code
	float tmpspeed;
	Collider tmpCol1;
	bool trigger1 = false;

	//animation
	animation_ctrl_rangedEn animRa;


	// Use this for initialization
	void Start () {
		target = transform; //set target to self when not hostile
		targetLocation = transform.position;

		animRa = GetComponent<animation_ctrl_rangedEn> ();
	}
	
	// Update is called once per frame
	void Update () {
		currentLocation = transform.position;
		targetDistance = Vector3.Distance (transform.position, target.position);
		moving = Vector3.Distance(targetLocation, currentLocation) > 2; //if we have a target location, we are moving

		CheckCollider(); //check to see if tmpCpl1 still exists

		if (targetTimer <= 0) {	target = FindTargets ().transform; targetTimer = 75; } else { targetTimer--; } 

		if (target == transform) { randomMovement(); } //if not hostile, move around randomly
		if (target != transform) {
			transform.LookAt (new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z));
			if(targetDistance > approachRange){ transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime); }
			shoot(); //shoot when in range
			animRa.animAtk();

			randomStrafe();
		}

		if (target != transform && target.GetComponent<Health>().currentHealth <= 0) { target = transform; }

		nearbyEnemies = storeNearbyEnemies ();

		//lock enemy roation so he stays totally upright
		transform.rotation = Quaternion.Euler(1, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

	}

	/*====================================================================
	======Functions======================================================
	====================================================================*/

	//Check how many enemies are within nearbyRange of this enemy
	int storeNearbyEnemies(){
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, nearbyRange);
		int i = 0;
		int count = 0; 
		while (i < hitColliders.Length) {
//				hitColliders [i].SendMessage ("AddDamage");
				if (hitColliders [i].tag == "Enemy") {
						count++;
				}
				i++;
		}
		return count;
	}


	//move around the target while attacking
	void randomStrafe(){
		if (moving && Vector3.Distance(target.transform.position, targetLocation) < shootRange && targetDistance > 1.0f) {
			transform.position = Vector3.MoveTowards (transform.position, targetLocation, speed * Time.deltaTime);
		} else {
			//if we have no target(since we aren't moving), choose a random spot within 5 units to move towards
			targetLocation = new Vector3(target.transform.position.x + Random.Range(-7.0F, 7.0F), currentLocation.y, target.transform.position.z + Random.Range(-7.0F, 7.0F));
		}
	}

	//shoot if in range
	void shoot(){
		if (targetDistance < shootRange && targetDistance > -shootRange && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			Rigidbody instantiatedProjectile = Instantiate(projectile, new Vector3(shooter.position.x, shooter.position.y, shooter.position.z),shooter.rotation) as Rigidbody;
			instantiatedProjectile.velocity = shooter.TransformDirection(new Vector3(0, 0,bulletSpeed));
			animRa.animAtk();
		}
	}

	//Find a target
	GameObject FindTargets(){
		GameObject[] gos;
		GameObject[] gos2;
		gos = GameObject.FindGameObjectsWithTag("Player");
		gos2 = GameObject.FindGameObjectsWithTag("Companion");
		int size = gos.Length + gos2.Length;
		GameObject[] gos3 = new GameObject[size];
		int index = 0;
		foreach (GameObject go in gos)   { gos3[index++] = go; } 
		foreach (GameObject go in gos2) { gos3[index++] = go; }
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos3) {
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

	//Collide with something besides the ground? Choose a new random spot to move to 
	void OnCollisionStay(Collision collision) {
		if (collision.transform.position.y > 0.5) {
			targetLocation = new Vector3 (currentLocation.x + Random.Range (-10.0F, 10.0F), currentLocation.y, currentLocation.z + Random.Range (-10.0F, 10.0F));
		}
	}


	/*//starts the robot when it leaves robot imasible terrain
	void OnTriggerExit(Collider othObj){
		if (othObj.gameObject.tag == "RoboImpas") {
			speed = tmpspeed;
		}
	}*/
}
