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

	//where this is currently moving
	Vector3 move_location;

	//how fast this moves
	public int move_distance = 1;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position =  new Vector3(this.target.transform.position[0], this.transform.position[1], this.target.transform.position[2]);
		this.transform.rotation = this.target.transform.rotation;


	}

	//finds the closest valid target and sets this object's "target" property to point to what it finds
	void set_target(){
		//get an array of things tagged as enemies
		GameObject[] possible_targets_array = GameObject.FindGameObjectsWithTag (target_tag);

		//if it wasn't able to find any objects with that tag at any range, complain and commit suicide
		if (possible_targets_array.Length != 0) {
			print ("ALERT  |  MISSILE:  no targets found with target_tag  " + target_tag);
			Destroy(this.gameObject);
		}

		//turn the array into a List because arrays are tedious as hell to work with
		List<GameObject> possible_targets = new List<GameObject>();
		foreach (GameObject element in possible_targets_array) {
			possible_targets.Add(element);
		}

		//set this to the first element
		GameObject closest_possible_target = possible_targets [0];


		bool found_valid_target = false;
		while(!(found_valid_target)){
			//pick the closest one
			foreach (GameObject element in possible_targets) {
				//if it finds an element closer or the same distance, make it the closest_possible_target
				float distance_to_element = (element.transform.position - transform.position).magnitude;
				float distance_to_closest_possible_target = (closest_possible_target.transform.position - transform.position).magnitude;
				if (distance_to_element <= distance_to_closest_possible_target){
					closest_possible_target = element;
				}
			}

			// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
			shootRay.origin = transform.position;

			//calculate which direction to shoot the ray
			Vector3 closest_heading = closest_possible_target.transform.position - transform.position;
			float closest_distance = closest_heading.magnitude;
			Vector3 closest_direction = closest_heading / closest_distance; // This is now the normalized direction.

			//store the direction to shoot the ray
			shootRay.direction = closest_direction;

			//try to shoot a ray at the closest_possible_target
			if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
			{
				//if it hits the target, set target to point to that object
				if(shootHit.collider == closest_possible_target){
					target = closest_possible_target;
				}
				//if it hits something else, move on to the next object in the array
				else{
					possible_targets.Remove(closest_possible_target);
				}
			}
		}
	}

	void move(){
		//shoot a ray at target
		//if it hits the target
			//move towards the target

			//rotate to face where it's moving
			transform.LookAt (move_location);
			//move
			int md = move_distance;
			transform.position = new Vector3(move_location[0] % md, move_location[1] % md, move_location[2] % md) + transform.position;
		//if it hits a wall or something else
			//either 	
				//blow up
				//check if the target is dead 
					//if not, move towards the last position
	}
}
