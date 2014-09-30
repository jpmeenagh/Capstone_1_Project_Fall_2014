using UnityEngine;
using System.Collections;


public class Enemy_Behavior : MonoBehaviour {
	
	
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

	
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 aim = aimTarget.transform.position;
		Vector3 move = target.transform.position;
		transform.LookAt(aim);
		float distanceToAim = Vector3.Distance (aim, transform.position);
		float distanceToMove = Vector3.Distance (move, transform.position);
		
		if (distanceToMove > approach || distanceToMove < -approach){
			transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
		}

 		if (distanceToAim < range && distanceToAim > -range && Time.time > nextFire)
        	{
		nextFire = Time.time + fireRate;
            	Rigidbody instantiatedProjectile = Instantiate(projectile,shooter.position,shooter.rotation)as Rigidbody;
            	instantiatedProjectile.velocity = shooter.TransformDirection(new Vector3(0, 0,bulletSpeed));

        	}
	}
}
