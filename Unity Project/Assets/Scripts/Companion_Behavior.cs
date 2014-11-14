using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Companion_Behavior : MonoBehaviour {
	
	//companion vars
	public GameObject tether;
	public float tetherDistance;
	public int tetherDefenseRange = 6;
	public int tetherFarRange = 9;
	public int tetherTeleportRange = 30;
	bool tooFar;
	
	//GUI
	GUIStyle style = new GUIStyle();
	Texture2D texture;
	
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
	public Transform oldTarget;
	public int oldTargetTimer = 30;
	
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

	//public AudioClip meleeSound;	
	AudioSource[] sounds;
	AudioSource offSoundSource;
	AudioSource defSoundSource;
	AudioSource supSoundSource;
	
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
		
		target = tether.transform; //set default target
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
		
		texture = new Texture2D(1, 1);

		sounds = this.GetComponents<AudioSource>();
		offSoundSource = sounds[3];
		defSoundSource = sounds[4];
		supSoundSource = sounds[5];
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<Health> ().currentHealth > 0) {
			stanceColor ();
			tooFar = withinTetherDistance ();
			currentLocation = transform.position;
			
			//teleport the companion to the player if they get separated by the tetherTeleportRange
			if(Vector3.Distance (currentLocation, tether.transform.position) > tetherTeleportRange) {
				transform.position = new Vector3(tether.transform.position.x + 2, transform.position.y + 5, tether.transform.position.z);
			} 
			
			
			targetDistance = Vector3.Distance (transform.position, target.position);
			tetherDistance = Vector3.Distance (transform.position, tether.transform.position);
			
			CheckCollider (); //check to see if tmpCpl1 still exists
			
			
			if (targetTimer <= 0) {
				target = FindTargets ().transform;
				targetTimer = 10;
			} else {
				targetTimer--;
			} 
			
			//if too far away stop randomly moving and move towards the player, look at them if no target
			if (tooFar) {
				if (target == tether) { transform.LookAt (tether.transform); }
				transform.position = Vector3.MoveTowards (transform.position, tether.transform.position, speed * Time.deltaTime);
			}else if (target == tether.transform) { randomMovement(); }
			
			if (target != tether.transform) {
				transform.LookAt (target.transform);
				
				if(!tooFar){
					if (targetDistance >= approachRange) {
						
						transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime);
					}
				}
				
				shoot(); //shoot when in range
			}
			
			useAbilities(); //use companion abilities when you have a target
			
			if (target.GetComponent<Health> ().currentHealth <= 0) {
				target = transform;
			}
			
			controlStances ();
			
			nearbyEnemies = storeNearbyEnemies ();
			playerNearbyEnemies = storePlayerNearbyEnemies ();
			
			//lock companion roation so he stays totally upright
			transform.rotation = Quaternion.Euler (1, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
			
			if (oldTargetTimer < 1) {
				oldTarget = null;
				oldTargetTimer = 30;
			} else {
				oldTargetTimer--;
			}
			
			
			moving = Vector3.Distance (targetLocation, currentLocation) > 2 && target == tether.transform && !tooFar; //if we have a target location, we are moving
			
		} else {
			//die with no health
			transform.rotation = Quaternion.Euler (75, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
		}
	}
	
	/*====================================================================
	======Functions======================================================
	====================================================================*/
	
	bool withinTetherDistance (){
		
		return (tetherDistance > tetherDefenseRange && stance == "Defense") || (tetherDistance > tetherFarRange && (stance == "Support" || stance == "Attack"));
	}
	
	//Check how many enemies are within nearbyRange of the companion
	int storeNearbyEnemies(){
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, nearbyRange);
		int i = 0;
		int count = 0; 
		while (i < hitColliders.Length) {
			//			hitColliders [i].SendMessage ("AddDamage");
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
			//			hitColliders [i].SendMessage ("AddDamage");
			if (hitColliders [i].tag == "Enemy") {
				count++;
			}
			i++;
		}
		return count;
	}
	
	
	void controlStances(){
		if (Input.GetButtonUp ("SwitchStanceUp")) {
			if(stance == "Attack"){ 
				changeStance("Defend"); 
				defSoundSource.Play();
			}
			else if(stance == "Defend"){ 
				changeStance("Support"); 
				supSoundSource.Play();
			}
			else if(stance == "Support"){ 
				changeStance("Attack"); 
				offSoundSource.Play();
			}
		}
		
		if (Input.GetButtonUp ("SwitchStanceDown")) {
			if(stance == "Attack"){ 
				changeStance("Support"); 
				supSoundSource.Play();
			}
			else if(stance == "Defend"){ 
				changeStance("Attack"); 
				offSoundSource.Play();
			}
			else if(stance == "Support"){ 
				changeStance("Defend"); 
				defSoundSource.Play();
			}
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
		arrayOfNames.Add (testAbilityName);
		arrayOfAbilities.Add(ability);
		//Add a 0 default cooldown at the same index in the cooldowns array
		arrayOfCooldowns.Add(0);
		
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
		if (name == "Flamethrower" && stance == "Attack") {
			if (checkEnemiesAtLocation (new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), 2) 
			    > 0) { 
				return true; 
			} else { 
				return false;
			}
		} else if (name == "Flamethrower" && stance == "Defend") {
			if (checkEnemiesAtLocation (new Vector3 (transform.position.x, transform.position.y, transform.position.z), 3) > 0 ||
			    checkEnemiesAtLocation (new Vector3 (tether.transform.position.x, tether.transform.position.y, tether.transform.position.z), 3) > 2
			    ) { 
				return true; 
			} else { 
				return false;
			}
		} else if (name == "Sabotage" && stance == "Attack") {
			if (checkEnemiesAtLocation (new Vector3 (transform.position.x, transform.position.y, transform.position.z + 2), 2) 
			    > 0) { 
				//target = spreadTheWealth();
				return true; 
			} else { 
				return false;
			}
		} else if (name == "Sabotage" && stance == "Support") {
			if (checkEnemiesAtLocation (new Vector3 (transform.position.x, transform.position.y, transform.position.z + 2), 2) 
			    > 0) { 
				//target = spreadTheWealth();
				return true; 
			} else { 
				return false;
			}
		} else if (name == "Heal" && stance == "Support") {
			if (tether.GetComponent<Health>().currentHealth < 100) {
				return true; 
			} else { 
				return false;
			}
		} else if (name == "Heal" && stance == "Defend") {
			if (tether.GetComponent<Health>().currentHealth < 100) {
				return true; 
			} else { 
				return false;
			}
		} else {
			return false;
		}
	}
	
	Transform spreadTheWealth(){
		Transform newTarget = target;
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, targetingRange);
		int i = 0; 
		while (i < hitColliders.Length) {
			if (hitColliders [i].tag == "Enemy") {
				if(hitColliders[i].transform != oldTarget){ 
					newTarget = hitColliders[i].transform; 
					oldTarget = target;
				}
			}
			i++;
		}
		return newTarget;
		
	}
	
	int checkEnemiesAtLocation(Vector3 location, int range){
		Collider[] hitColliders = Physics.OverlapSphere (location, range);
		int i = 0;
		int count = 0; 
		while (i < hitColliders.Length) {
			if (hitColliders [i].tag == "Enemy") {
				count++;
			}
			i++;
		}
		return count;
		
	}
	
	//shoot if in range
	void shoot(){
		if (targetDistance < shootRange && targetDistance > -shootRange && Time.time > nextFire && target && Time.time > 3){
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
		Vector3 position = tether.transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance && curDistance < targetingRange && 
			    go.GetComponent<Health>().currentHealth > 0 && oldTarget != go.transform){
				closest = go;
				distance = curDistance;
			}
		}
		if (distance > targetingRange) { closest = tether; }
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
			targetLocation = new Vector3(tether.transform.position.x + Random.Range(-tetherDefenseRange, tetherDefenseRange), tether.transform.position.y, tether.transform.position.z + Random.Range(-tetherDefenseRange, tetherDefenseRange));
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
		if (moving) {
			if (collision.transform.position.y > 0.5) {
				targetLocation = new Vector3 (currentLocation.x + Random.Range (-10.0F, 10.0F), currentLocation.y, currentLocation.z + Random.Range (-10.0F, 10.0F));
			}
		}
	}
	
	void OnGUI(){
		
		texture.Apply();
		
		style.normal.background = texture;
		style.normal.textColor = Color.black;
		
		GUI.Label (new Rect (Screen.width - 70, Screen.height - 40, 75, 50), "Stance: \n" + stance, style);
		//Screen.width - 200
		//Screen.width - 320
		GUI.Label (new Rect (Screen.width - 200, Screen.height - 65, 120, 100), "Abilties: \n" + testAbility1Name + " - " + arrayOfCooldowns[0] +
		           "\n" + testAbility2Name + " - " + arrayOfCooldowns[1] +
		           "\n" + testAbility3Name + " - " + arrayOfCooldowns[2], style);
		
		
	}
	
	/*//starts the robot when it leaves robot imasible terrain
	void OnTriggerExit(Collider othObj){
		if (othObj.gameObject.tag == "RoboImpas") {
			speed = tmpspeed;
		}
	}*/
}
