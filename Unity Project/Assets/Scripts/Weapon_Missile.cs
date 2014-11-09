using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon_Missile : MonoBehaviour {
	//how much damage does this do
	public int damage = 50;

	public string target_tag = "Enemy";
	GameObject target;


	// A ray to determine if a (possible) target is within line of sight
	Ray shootRay;
	// A raycast hit to get information about what was hit.
	RaycastHit shootHit;
	// A layer mask so the raycast only hits things on the shootable layer.
	int shootableMask;
	//The distance to look for targets
	public float range = 100f;

	//where this moved last
	Vector3 last_move_location;
	
	//how fast this moves
	public float move_distance;

	void Awake ()
	{
		// Create a layer mask for the Shootable layer.
		shootableMask = 1 << 10;
		shootableMask = ~shootableMask;
	}

	// Use this for initialization
	void Start () {
		set_target ();
	}
	
	// Update is called once per frame
	void Update () {
//		this.transform.position =  new Vector3(this.target.transform.position[0], this.transform.position[1], this.target.transform.position[2]);
//		this.transform.rotation = this.target.transform.rotation;

		move ();


	}

	//finds the closest valid target and sets this object's "target" property to point to what it finds
	void set_target(){
		//get an array of things tagged as enemies
		GameObject[] possible_targets_array = GameObject.FindGameObjectsWithTag (target_tag);

		//if it wasn't able to find any objects with that tag at any range, complain and commit suicide
		if (possible_targets_array.Length == 0) {
			print ("ALERT  |  MISSILE:  no targets found with target_tag  " + target_tag);
			Destroy(this.gameObject);
			return;
		}

		//turn the array into a List because arrays are tedious as hell to work with
		List<GameObject> possible_targets = new List<GameObject>();
		foreach (GameObject element in possible_targets_array) {
			possible_targets.Add(element);
		}

		//this will be set in the while loop
		GameObject closest_possible_target;

		//has this found a valid target yet?
		bool found_valid_target = false;

		//find a valid target
		while(!(found_valid_target) && (possible_targets.Count > 0)){
			//pick the closest one
			closest_possible_target = find_closest_possible_target(possible_targets[0], possible_targets);

			print ("loop state: start |  found: " + found_valid_target + "  |  possible: " + possible_targets.Count + "  |  closest: " + closest_possible_target);
		    
			// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
			shootRay.origin = transform.position;

			//calculate which direction to shoot the ray
			Vector3 closest_heading = closest_possible_target.transform.position - transform.position;
			float closest_distance = closest_heading.magnitude;
			Vector3 closest_direction = closest_heading / closest_distance; // This is now the normalized direction.

			//store the direction to shoot the ray
			shootRay.direction = closest_direction;

			//if shooting a ray at the closest_possible_target hits something
			if(Physics.Raycast(shootRay, out shootHit, range, shootableMask)){
				//if it hits the target, set target to point to that object
				if(shootHit.collider.gameObject.GetInstanceID() == closest_possible_target.GetInstanceID()){
					target = closest_possible_target;
					found_valid_target = true;
				}
			}
			//if it hits something else or nothing, remove that from possible_targets and try the next one
			if(!(found_valid_target)){
				possible_targets.Remove(closest_possible_target);
				//closest_possible_target = find_closest_possible_target(possible_targets[0], possible_targets);

			}

			print ("loop state: end  |  found valid target: " + found_valid_target + "  |  target: " + target + "  |  possible: " + possible_targets.Count);
		}

		//if it went through all possible targets and didn't find a valid one, commit suicide
		if(!(found_valid_target)){
			print ("ALERT  |  MISSILE:  No valid targets found with target_tag  " + target_tag);
			Destroy(this.gameObject);
			return;
		}
	}

	private GameObject find_closest_possible_target(GameObject g_closest_possible_target, List<GameObject> g_possible_targets){
		GameObject temp_closest_possible_target = g_closest_possible_target;
		float distance_to_element = 0f;
		float distance_to_closest_possible_target = 0f;

		foreach (GameObject element in g_possible_targets) {
			//calculate distances
			distance_to_element = (element.transform.position - transform.position).magnitude;
			distance_to_closest_possible_target = (temp_closest_possible_target.transform.position - transform.position).magnitude;
			
			//if it finds an element closer or the same distance, make it the closest_possible_target
			if (distance_to_element <= distance_to_closest_possible_target){
				temp_closest_possible_target = element;
			}
		}
		return temp_closest_possible_target;
	}


	void move(){
		//shoot a ray at target

		//set where the ray starts
		shootRay.origin = transform.position;
		
		//calculate which direction to shoot the ray
		Vector3 target_heading = target.transform.position - transform.position;
		float target_distance = target_heading.magnitude;
		Vector3 target_direction = target_heading / target_distance; // This is now the normalized direction.
		
		//store the direction to shoot the ray
		shootRay.direction = target_direction;
		//if shooting a ray at the closest_possible_target hits something
		if(Physics.Raycast(shootRay, out shootHit, range, shootableMask)){
			//if it hits the target, move towards the target
			if(shootHit.collider.gameObject.GetInstanceID() == target.GetInstanceID()){
				//rotate to face where it's moving
				transform.LookAt (target.transform.position);

				//difference in position for each direction
				float diff_x = target.transform.position[0] - transform.position[0];
				float diff_y = target.transform.position[1] - transform.position[1];
				float diff_z = target.transform.position[2] - transform.position[2];

				//calculate whether to move the full allowed distance or less and if the latter, how much
				float move_x = Mathf.Sign(diff_x) * Mathf.Min(Mathf.Abs(diff_x), move_distance);
				float move_y = Mathf.Sign(diff_y) * Mathf.Min(Mathf.Abs(diff_y), move_distance);
				float move_z = Mathf.Sign(diff_z) * Mathf.Min(Mathf.Abs(diff_z), move_distance);

				//make a vector to add to the current position
				Vector3 movement_vector = new Vector3(move_x, move_y, move_z);

				//print("Missile  |  position: " + transform.position + " | move: " + movement_vector + " | target: " + target.transform.position);

				//change the current position
				transform.position = movement_vector + transform.position;
			}
		}

			
		//if it hits a wall or something else
			//either 	
				//blow up
				//check if the target is dead 
					//if not, move towards the last position
	}

	void OnTriggerEnter(Collider other) {
		//hurt 
		hurt_enemy (other);

		//the missile has hurt the enemy, so it should suicide
		Destroy(this.gameObject);
	}

	//hurt enemy, return whether it hit enemy (and hurt them) or notg
	bool hurt_enemy(Collider given_collider){
		// Try and find an EnemyHealth script on the gameobject hit.
		Health enemyHealth = given_collider.GetComponent <Health> ();

		// If the EnemyHealth component exist...
		if (enemyHealth != null) {

			if (given_collider.gameObject.tag.Equals("Enemy")) {
				// ... the enemy should take damage.
				//enemyHealth.TakeDamage (this.following_object.GetComponent<dmg_out_mod_player>().modDmg(damagePerSecond), new Vector3(0f,0f,0f), following_tags[target]);
				enemyHealth.TakeDamage (damage, new Vector3 (0f, 0f, 0f), "Companion");

				return true;
			}
		}
		return false;
	}
}
