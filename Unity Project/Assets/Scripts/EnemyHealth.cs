﻿using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.

	//CapsuleCollider enemyCollider;            // Reference to the capsule collider.
	BoxCollider enemyCollider;
	bool isDead;                                // Whether the enemy is dead.
	bool isSinking;                             // Whether the enemy has started sinking through the floor.

	
	void Awake ()
	{
		// Setting up the references.
		//enemyCollider = GetComponent <CapsuleCollider> ();
		enemyCollider = GetComponent <BoxCollider> ();
		
		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;
	}
	
	void Update ()
	{
		// If the enemy should be sinking...
		if(isSinking)
		{
			// ... move the enemy down by the sinkSpeed per second.
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
			print (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}
	
	
	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		// If the enemy is dead...
		if(isDead)
			// ... no need to take damage so exit the function.
			return;
		
		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;
		
		// If the current health is less than or equal to zero...
		if(currentHealth <= 0)
		{
			// ... the enemy is dead.
			Death ();
		}
	}
	
	
	void Death ()
	{
		// The enemy is dead.
		isDead = true;
		
		// Turn the collider into a trigger so shots can pass through it.
		enemyCollider.isTrigger = true;
	}
	
	
	public void StartSinking ()
	{
		// Find and disable the Nav Mesh Agent.
		GetComponent <NavMeshAgent> ().enabled = false;
		
		// Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
		GetComponent <Rigidbody> ().isKinematic = true;

		// The enemy should no sink.
		isSinking = true;
		
		// After 2 seconds destory the enemy.
		Destroy (gameObject, 2f);
	}

	void OnTriggerEnter(Collider otherObj)
   	{
	
      	
	if(otherObj.gameObject.tag == "Bullet")
    	{
 		TakeDamage(17, new Vector3(0,0,0));
        	
 	}

    }
	

	

}