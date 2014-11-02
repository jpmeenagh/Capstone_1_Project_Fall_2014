using UnityEngine;

//http://docs.unity3d.com/Manual/Layers.html

public class Attack_Ranged_Ray : MonoBehaviour
{
	public int damagePerShot = 20;                  // The damage inflicted by each bullet.
	public float timeBetweenAttacks = 0.15f;        // The time between each shot.
	public float range = 100f;                      // The distance the gun can fire.
	public string sourceDmg = "player";				//souce of the dmg, fed to what it is hitting
	
	float time_since_last_attack;                                    // A timer to determine when to fire.
	float time_at_last_attack;
	Ray shootRay;                                   // A ray from the gun end forwards.
	RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
	int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	LineRenderer gunLine;


	float effects_display_time = 0.1f;                // The proportion of the timeBetweenBullets that the effects will display for.
	
	void Awake ()
	{
		// Create a layer mask for the Shootable layer.
		shootableMask = 1 << 10;
		shootableMask = ~shootableMask;

		//set the line renderer
		gunLine = GetComponent<LineRenderer> ();

		time_at_last_attack = 0f;
		time_since_last_attack = 0f;
	}
	
	void Update ()
	{
		// Add the time since Update was last called to time_since_last_attack.
		time_since_last_attack += Time.deltaTime;

		if(Time.time >= time_at_last_attack + effects_display_time)
		{
			DisableEffects ();
		}

	}
	
	public void DisableEffects ()
	{
		//print ("called disableeffects");
		gunLine.enabled = false;
	}
	
	public void Shoot ()
	{
		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		//if there hasn't been enough time between attacks
		if(time_since_last_attack < timeBetweenAttacks)
		{
			print ("Attack_Ranged:  NOFIRE  |  time_since_last:  " + time_since_last_attack);
			/*
			if(time_since_last_attack > time_at_last_attack + effects_display_time)
			{
				DisableEffects ();
			}
			return;
			*/
		}
		else{
			print ("Attack_Ranged:  FIRE  |  time_since_last:  " + time_since_last_attack);

			// Reset the time_since_last_attack.
			time_since_last_attack = 0f;

			time_at_last_attack = Time.time;


			gunLine.enabled = true;
			gunLine.SetPosition (0, transform.position);

			// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;
			
			// Perform the raycast against gameobjects on the shootable layer and if it hits something...
			if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
			{
				// Try and find an EnemyHealth script on the gameobject hit.
				Health targetHealth = shootHit.collider.GetComponent <Health> ();

				
				// If the EnemyHealth component exist...
				if(targetHealth != null)
				{
					if(targetHealth.faction != this.GetComponent<Health>().faction)
					{
						print("hit: other faction");
						// ... the enemy should take damage.
						targetHealth.TakeDamage (this.GetComponent<dmg_out_mod_player>().modDmg(damagePerShot), shootHit.point, sourceDmg);
					}
					else
					{
						print("hit: same faction");
					}
				}
				gunLine.SetPosition(1, shootHit.point);
			}
			// If the raycast didn't hit anything on the shootable layer...
			else
			{
				print("miss");
				gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
			}
		}
	}
}