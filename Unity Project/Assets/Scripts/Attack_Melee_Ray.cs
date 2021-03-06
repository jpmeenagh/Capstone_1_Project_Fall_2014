using UnityEngine;

//http://docs.unity3d.com/Manual/Layers.html

public class Attack_Melee_Ray : MonoBehaviour
{
	public int damagePerShot = 20;                  // The damage inflicted by each bullet.
	public float timeBetweenAttacks = 0.15f;        // The time between each shot.
	public float range = 100f;                      // The distance the gun can fire.
	public string sourceDmg = "player";				//souce of the dmg, fed to what it is hitting
	
	float timer;                                    // A timer to determine when to fire.
	Ray shootRay;                                   // A ray from the gun end forwards.
	RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
	int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	
	float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

	//public AudioClip meleeSound;					//sound for melee
	AudioSource[] sounds;
	AudioSource meleeSoundSource;
	
	void Awake ()
	{
		// Create a layer mask for the Shootable layer.
		//shootableMask = 1 << 8;
		//shootableMask = ~shootableMask;
		shootableMask = 1 << 10;
		shootableMask = ~shootableMask;
		sounds = this.GetComponents<AudioSource>();
		meleeSoundSource = sounds[0];
	}
	
	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;

		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if(timer >= timeBetweenAttacks * effectsDisplayTime)
		{
			// ... disable the effects.
			DisableEffects ();
		}
	}
	
	public void DisableEffects ()
	{
		
	}
	
	public void Shoot ()
	{
		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if(timer < timeBetweenAttacks)
		{
			return;
		}

		// Reset the timer.
		timer = 0f;
		
		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		//play sound
		//audio.PlayOneShot (meleeSound);
		//audio.Stop ();
		//audio.Play ();
		meleeSoundSource.Play ();
		
		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			print("hit");
			// Try and find an EnemyHealth script on the gameobject hit.
			Health enemyHealth = shootHit.collider.GetComponent <Health> ();
			
			// If the EnemyHealth component exist...
			if(enemyHealth != null)
			{
				if(enemyHealth.faction != this.GetComponent<Health>().faction){
				// ... the enemy should take damage.
				enemyHealth.TakeDamage (this.GetComponent<dmg_out_mod_player>().modDmg(damagePerShot), shootHit.point, sourceDmg);
				}
				}
		}
		// If the raycast didn't hit anything on the shootable layer...
		else
		{
			print("miss");
		}
	}
}