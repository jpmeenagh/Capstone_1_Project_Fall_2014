using UnityEngine;

//http://docs.unity3d.com/Manual/Layers.html

public class Heal_Ranged_Ray : MonoBehaviour
{
	public int damagePerShot = 20;                  // The damage inflicted by each bullet.
	public float timeBetweenAttacks = 0.15f;        // The time between each shot.
	public float range = 100f;                      // The distance the gun can fire.
	public string sourceDmg = "Companion";				//souce of the dmg, fed to what it is hitting

	private bool was_firing = false;

	//at what height do the rays shoot out from
	public float origin_y;
	
	//position rays shoot out from
	private Vector3 shooting_origin;
	
	float time_since_last_attack;                                    // A timer to determine when to fire.
	float time_at_last_attack;
	Ray shootRay;                                   // A ray from the gun end forwards.
	RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
	int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	LineRenderer gunLine;
	
	
	float effects_display_time = 0.1f;                // The proportion of the timeBetweenBullets that the effects will display for.
	
	//public AudioClip shootSound;					//sound for shooting
	AudioSource[] sounds;
	AudioSource shootSoundSource;
	
	void Awake ()
	{
		// Create a layer mask for the Shootable layer.
		shootableMask = 1 << 10;
		shootableMask = ~shootableMask;
		
		//set the line renderer
		gunLine = GetComponent<LineRenderer> ();
		
		time_at_last_attack = 0f;
		time_since_last_attack = 0f;
		
		sounds = this.GetComponents<AudioSource>();
		shootSoundSource = sounds[1];
		
		shooting_origin = new Vector3(transform.position.x, origin_y, transform.position.z);
	}
	
	void Update ()
	{
		// Add the time since Update was last called to time_since_last_attack.
		time_since_last_attack += Time.deltaTime;
		
		if((Time.time >= time_at_last_attack + effects_display_time) && was_firing)
		{
			DisableEffects ();
			was_firing = false;
		}
	}
	
	public void DisableEffects ()
	{
		//print ("called disableeffects");
		gunLine.enabled = false;
	}
	
	public void Shoot (){
		//if there has been enough time between attacks
		if(time_since_last_attack >= timeBetweenAttacks){
			print ("UPDATE | Heal_Ranged_Ray | Fired:  true | time_since_last:  " + time_since_last_attack);
			
			shooting_origin = new Vector3(transform.position.x, origin_y, transform.position.z);
			
			// Reset the time_since_last_attack.
			time_since_last_attack = 0f;
			
			time_at_last_attack = Time.time;
			
			
			gunLine.enabled = true;
			gunLine.SetPosition (0, shooting_origin);
			
			// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
			shootRay.origin = shooting_origin;
			shootRay.direction = transform.forward;
			
			//play audio
			//audio.Play();
			shootSoundSource.Play();
			
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
						print("UPDATE | Heal_Ranged_Ray | hit: same faction");
						// ... the enemy should take damage.
						targetHealth.Heal (damagePerShot, shootHit.point);
					}
					else
					{
						print("UPDATE | Heal_Ranged_Ray | hit: other faction");
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
			was_firing = true;
		}
		else{
			print ("UPDATE | Heal_Ranged_Ray | Fired:  false | time_since_last:  " + time_since_last_attack);
		}
	}
}
