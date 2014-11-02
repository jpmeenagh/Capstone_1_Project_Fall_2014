using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Companion_Behavior : MonoBehaviour {
	
	//companion vars
	public GameObject tether;
	public float tetherDistance;
	public int tetherRange = 8;
	
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
	public int playerNearbyEnemies;
	public float nearbyRange = 4;
	
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
	
	//Companion Ability List
	public List<Ability> arrayOfAbilities; //the array of ability scripts
	public List<int> arrayOfCooldowns; //the array of ability cooldowns
	public List<string> arrayOfNames; //the array of ability names
	
	//Companion Stance
	public string stance = "";
	public string defaultStance = "Attack";
	
	//test abilities to auto add, since there's no menu to set up companion
	public Ability testAbility1;
	public string  testAbility1Name;
	public Ability testAbility2;
	public string  testAbility2Name;
	public Ability testAbility3;
	public string  testAbility3Name;

	// Use this for initialization
	void Start () {
		target = transform; //set target to self when not hostile
		targetLocation = transform.position;
		
		//initialize ability list
		arrayOfAbilities = new List<Ability>();
		arrayOfCooldowns = new List<int>();
		arrayOfNames     = new List<string>();

		//add abilities
		addAbility (testAbility1, testAbility1Name);
		addAbility (testAbility2, testAbility2Name);
		addAbility (testAbility3, testAbility3Name);

		changeStance(defaultStance); //set the default stance
	}
	
	// Update is called once per frame
	void Update () {
		stanceColor ();
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
		
		//if (target == transform) { randomMovement(); } //if not hostile, move around randomly *only works on enemy
		if (target != transform) {
			if(tetherDistance <= tetherRange){
				transform.LookAt (new Vector3 (target.transform.position.x, target.transform.position.y, target.transform.position.z)); 
				if (targetDistance > approachRange) {transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime);}
			}
			shoot(); //shoot when in range
			useAbilities(); //use companion abilities when you have a target
		}
		
		if (target.GetComponent<Health> ().currentHealth <= 0) { target = transform; }
		
		controlStances ();
		
		nearbyEnemies = storeNearbyEnemies ();
		playerNearbyEnemies = storePlayerNearbyEnemies ();
	}
	
	/*====================================================================
	======Functions======================================================
	====================================================================*/
	
	//Check how many enemies are within nearbyRange of the companion
	int storeNearbyEnemies(){
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, nearbyRange);
		int i = 0;
		int count = 0; 
		while (i < hitColliders.Length) {
			hitColliders [i].SendMessage ("AddDamage");
			if (hitColliders [i].tag == "Enemy") {
				count++;
			}
			i++;
		}
		return count;
	}
	
	//Check how many enemies are within nearbyRange of the tether (player)
	int storePlayerNearbyEnemies(){
		Collider[] hitColliders = Physics.OverlapSphere (tether.transform.position, nearbyRange);
		int i = 0;
		int count = 0; 
		while (i < hitColliders.Length) {
			hitColliders [i].SendMessage ("AddDamage");
			if (hitColliders [i].tag == "Enemy") {
				count++;
			}
			i++;
		}
		return count;
	}
	
	
	void controlStances(){
		if (Input.GetButtonUp ("SwitchStanceUp")) {
			if(stance == "Attack"){ changeStance("Defend"); }
			else if(stance == "Defend"){ changeStance("Support"); }
			else if(stance == "Support"){ changeStance("Attack"); }
		}
		
		if (Input.GetButtonUp ("SwitchStanceDown")) {
			if(stance == "Attack"){ changeStance("Support"); }
			else if(stance == "Defend"){ changeStance("Attack"); }
			else if(stance == "Support"){ changeStance("Defend"); }
		}
	}
	
	void stanceColor(){
		if (stance == "Attack") { gameObject.renderer.material.color = Color.red; }
		if (stance == "Defend") { gameObject.renderer.material.color = Color.blue; }
		if (stance == "Support") { gameObject.renderer.material.color = Color.white; }
	}
	
	void changeStance(string newStance){ 
		Ability.Stance newStanceEnum = Ability.Stance.Attack;
		if (newStance == "Attack") { newStanceEnum = Ability.Stance.Attack; }
		if (newStance == "Defend") { newStanceEnum = Ability.Stance.Defend; }
		if (newStance == "Support") { newStanceEnum = Ability.Stance.Support; }
		foreach (Ability ability in arrayOfAbilities) {
			if(ability != null){ ability.change_stance(newStanceEnum); }//Set the correct stance in each ability		
		}
		
		stance = newStance;
		print ("" + newStance);
	}
	
	//add ability to list
	void addAbility(Ability ability, string testAbilityName){
		arrayOfAbilities.Add(ability);
		//Add a 0 default cooldown at the same index in the cooldowns array
		arrayOfCooldowns.Add(0);
		arrayOfNames.Add (testAbilityName);
	}
	
	//Use abilities and increment cooldowns
	void useAbilities(){
		int cooldownIndex = 0;
		
		foreach (Ability ability in arrayOfAbilities) {
			if (arrayOfCooldowns [cooldownIndex] == 0) {
				if(checkAbilityUseCase(arrayOfNames[cooldownIndex])){
					arrayOfCooldowns [cooldownIndex] = ability.stance_delegate();//use the ability for the current stance, it will return the cooldown
				}
			} else {
				arrayOfCooldowns [cooldownIndex]--; //reduce cooldowns by 1
			}
			cooldownIndex++; //increase the index of index for the cooldowns
		}
	}
	
	//check if it's the right situation to use an ability
	bool checkAbilityUseCase (string name){
		//if (name == "Flamethrower") {
			//print ("" + name);
			return true;
		//} else {
			//return false;	
		//}
	}
	
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
		gos = GameObject.FindGameObjectsWithTag("Enemy");
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
