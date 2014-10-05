using UnityEngine;

//http://docs.unity3d.com/Manual/Layers.html

public class Heal_Ranged_Ray : MonoBehaviour
{
	public int damagePerShot = 20;                  // The damage inflicted by each bullet.
	public float timeBetweenAttacks = 0.15f;        // The time between each shot.
	public float range = 100f;                      // The distance the gun can fire.
	
	float timer;                                    // A timer to determine when to fire.
	Ray shootRay;                                   // A ray from the gun end forwards.
	RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
	int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	
	float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
	
	void Awake ()
	{
		// Create a layer mask for the Shootable layer.
		//shootableMask = 1 << 8;
		//shootableMask = ~shootableMask;
		shootableMask = 1 << 10;
		shootableMask = ~shootableMask;
	}
	
	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;
		
		// If the Fire1 button is being press and it's time to fire...
		//if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets)
		//{
		// ... shoot the gun.
		//	Shoot ();
		//}
		
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
		
		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
		{
			// Try and find an EnemyHealth script on the gameobject hit.
			Health targetHealth = shootHit.collider.GetComponent <Health> ();
			
			
			// If the EnemyHealth component exist...
			if(targetHealth != null)
			{
				if(targetHealth.faction == this.GetComponent<Health>().faction)
				{
					print("hit: same faction");
					// ... the enemy should take damage.
					targetHealth.Heal (damagePerShot, shootHit.point);
				}
				else
				{
					print("hit: other faction");
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