using UnityEngine;
using System.Collections;

public class animation_ctrl_rangedEn : MonoBehaviour {

	protected Animator animator;
	
	//    public float DirectionDampTime = .25f;
	
	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	void Update () 
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		if(animator)
		{
			
			//get the current state
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			
			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.ranged_Attack"))
			{
				animator.SetBool("animRangedAttack", false);
			}
			
			
			//animator.SetFloat("animRoboSpeed", h*h+v*v);
			
			/*
			//get the current state
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			
			//if we're in "Run" mode, respond to input for jump, and set the Jump parameter accordingly. 
			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.RunBT"))
			{
				if(Input.GetButton("Fire1")) 
					animator.SetBool("Jump", true );
			}
			else
			{
				animator.SetBool("Jump", false);                
			}
			
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			
			//set event parameters based on user input
			animator.SetFloat("Speed", h*h+v*v);
			animator.SetFloat("Direction", h, DirectionDampTime, Time.deltaTime);
			*/
		}       
	} 
	
	public void animAtk(){
		if (animator) {
			
			animator.SetBool("animRangedAttack", true);
			
			
		}
	}
}
