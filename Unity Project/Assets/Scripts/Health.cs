using UnityEngine;

public class Health : MonoBehaviour
{
	public enum Faction {Ally, Enemy, Neutral};
	public Faction faction = Faction.Neutral;
	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	public int maxHealth = 150;
	public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.

	//CapsuleCollider enemyCollider;            // Reference to the capsule collider.
	BoxCollider enemyCollider;
	bool isDead;                                // Whether the enemy is dead.
	bool isSinking;                             // Whether the enemy has started sinking through the floor.

	//tmp variables to feed to frenzy
	GameObject tmpPlayerObj;					//game object used to temporarily store the player for frenzy feedback on kill
	Frenzy_suit playFz;							//frenzy suit extracted from player

	//stores dmg mod scrip
	dmg_in_mod_ally dmgModAlly;
	dmg_in_mod_robo dmgModEnemy;

	
	public bool allowGameOver = true;
	public bool gameOver = false;
	public int gameOverTimer = 1;
	public bool CompanionDead = false;
	public bool allowCompanionDeath = true;

	//Health Bar Variables
        public float healthBarLength = 100;

        public Vector3 pos;
        public float xOffset;
        public float yOffset;

        GUIStyle style = new GUIStyle();
        Texture2D texture;
		Texture2D texture2;
		Color redColor = Color.red;
        Color greenColor = Color.green;
 
	 // Use this for initialization
    void Start () {
		texture = new Texture2D(1, 1);
        texture.SetPixel(1, 1, greenColor);

		texture2 = new Texture2D(1, 1);
		texture2.SetPixel(1, 1, Color.white);

		if (this.faction == Faction.Ally){
			dmgModAlly = this.GetComponent<dmg_in_mod_ally>();
		}
		if (this.faction == Faction.Enemy){
			dmgModEnemy = this.GetComponent<dmg_in_mod_robo>();
		}
    }

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
		//END THE GAME 
		if(tag == "Player" ){
			if (currentHealth <= 0 && allowGameOver && gameOver == false) {
				gameOver = true;
				gameOverTimer = 200;
					}
			if (gameOver) { gameOverTimer--; }
			if (gameOverTimer == 0){ GameOver(); }
		}
		if (tag == "Companion" && allowCompanionDeath && currentHealth <= 0) {
			CompanionDead = true;
		}
		// If the enemy should be sinking...
		if(isSinking)
		{
			// ... move the enemy down by the sinkSpeed per second.
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
			print (-Vector3.up * sinkSpeed * Time.deltaTime);
		}

		pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z)); 
		//The offsets are to position the health bar so it's placed above and centered to the character
		pos.y = Screen.height - pos.y;
		 
		if (currentHealth > 40)
        	{
 	           	texture.SetPixel(1, 1, greenColor);
        	}
 
 	       if (currentHealth < 40)
        	{
 	           	texture.SetPixel(1, 1, redColor);
        	}
			
		//For testing Health Bar, reduce player's health by 10 when Fire1 (ctrl) key is released
		//if(Input.GetButtonUp("Fire1"))
        	//{
 	    	//	TakeDamage(10, new Vector3(0,0,0));
		//}
	}
	
	
	public void TakeDamage (int amount, Vector3 hitPoint, string sourceDmg)
	{
		// If the enemy is dead...
		if (isDead) 
			{
						// ... no need to take damage so exit the function.
						return;
			}
		
		// Reduce the current health by the amount of damage sustained.
		if (this.faction == Faction.Neutral) {
				currentHealth -= amount;
			}

		if (this.faction == Faction.Ally) {
			currentHealth -= dmgModAlly.modDmg(amount);
		}

		if (this.faction == Faction.Enemy) {
			currentHealth -= dmgModEnemy.modDmg(amount);
		}
		
		// If the current health is less than or equal to zero...
		if(currentHealth <= 0)
		{
			// ... the enemy is dead.
			Death ();
			//get player, extract frenzy suit, tell frenzy we got a kill
			if (sourceDmg.Equals("Player")){
				tmpPlayerObj = GameObject.FindWithTag("Player");
				playFz = tmpPlayerObj.GetComponent<Frenzy_suit>();
				playFz.gotKill();
			}
		}
		
		healthBarLength = 100 * (currentHealth / (float)startingHealth);
	}

	public void Heal (int amount, Vector3 hitPoint)
	{
		// Reduce the current health by the amount of damage sustained.
		if (currentHealth < maxHealth) 
		{
			if(currentHealth + amount > maxHealth){
				currentHealth = maxHealth;
			}
			else{
				currentHealth += amount;
			}
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
 		TakeDamage(17, new Vector3(0,0,0), "Bullet");
        	
 	}

    }

    void OnGUI(){

    	texture.Apply();
 
		style.normal.background = texture;

		if (tag == "Player") {
			GUI.Box (new Rect (10, Screen.height - 20, healthBarLength, 20), new GUIContent (""), style);
			//GUI.Label (new Rect (10,Screen.height - 40,100,20), "PlayerHP");

			} else if (tag == "Companion") {
			//GUI.Label (new Rect (Screen.width - 120,Screen.height - 40,100,20), "CompanionHP");
			GUI.Box (new Rect (Screen.width - 120, Screen.height - 20, healthBarLength, 20), new GUIContent (""), style);
			} else {
				GUI.Box (new Rect (pos.x, pos.y, healthBarLength, 20), new GUIContent (""), style);
		}

		style.normal.background = texture2;
		style.normal.textColor = Color.black;
		if (tag == "Player") {
			GUI.Label (new Rect (10,Screen.height - 40,100,20), "PlayerHP", style);
		} else if (tag == "Companion") {
			GUI.Label (new Rect (Screen.width - 120,Screen.height - 40,100,20), "CompanionHP", style);
		} 

		if (gameOver) {
			GUI.Label (new Rect (Screen.width / 3, Screen.height / 2, Screen.width, Screen.height), "GAME OVER\nYOU HAVE DIED!\nrestart in: " + gameOverTimer);
		}
		if (CompanionDead) {
			GUI.Label (new Rect (Screen.width / 2, Screen.height / 2, Screen.width, Screen.height), "COMPANION HAS DIED, BRO\nYOUR FRIEND!!!!!");
		}
	

    }

	void GameOver ()
	{

		Debug.Break();
	}


	

}