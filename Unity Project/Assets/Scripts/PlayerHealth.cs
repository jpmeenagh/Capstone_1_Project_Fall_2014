using UnityEngine;
using System.Collections;
 
public class PlayerHealth : MonoBehaviour {
 
    public int maxHealth = 100;
    public int curHealth = 100;
    public float healthBarLength = 100;

    public Vector3 pos;
    public float xOffset;
    public float yOffset;

    GUIStyle style = new GUIStyle();
    Texture2D texture;
    Color redColor = Color.red;
    Color greenColor = Color.green;
 
    // Use this for initialization
    void Start () {
	texture = new Texture2D(1, 1);
        texture.SetPixel(1, 1, greenColor);
    }
 
    // Update is called once per frame
    void Update () {
 	
	pos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z)); //The offsets are to position the health bar so it's placed above and centered to the character
	pos.y = Screen.height - pos.y;
        AddjustCurrentHealth(0);	

	if (curHealth > 40)
        {
            texture.SetPixel(1, 1, greenColor);
        }
 
        if (curHealth < 40)
        {
            texture.SetPixel(1, 1, redColor);
        }
	
	//For testing Health Bar, reduce player's health by 10 when Fire1 (ctrl) key is released
	//if(Input.GetButtonUp("Fire1"))
        //{
 	//    AddjustCurrentHealth(-10);
	//}
   
    }
 
    void OnGUI(){

    	texture.Apply();
 
    	style.normal.background = texture;
    	GUI.Box(new Rect(pos.x,pos.y, healthBarLength, 20), new GUIContent(""), style);
    
    	//GUI.Box (new Rect(10,10, healthBarLength, 20), curHealth + "/" + maxHealth);    
    	//GUI.DrawTexture(new Rect(pos.x,pos.y,100,40), aTexture, ScaleMode.ScaleToFit, true, 10.0F);
    	//GUI.Box (new Rect(pos.x,pos.y, healthBarLength, 20), ""); 
 
    }
 
    public void AddjustCurrentHealth(int adj){
 
    curHealth += adj;
 
        if(curHealth <0)
            curHealth = 0;
 
        if(curHealth > maxHealth)
            curHealth = maxHealth;
 
        if(maxHealth <1)
            maxHealth = 1;
 
 
        healthBarLength = 100 * (curHealth / (float)maxHealth);
 
    }

    void OnTriggerEnter(Collider otherObj)
    {
	
      	
	if(otherObj.gameObject.tag == "Bullet")
    	{
 		AddjustCurrentHealth(-10);
        	
 	}

    }


}