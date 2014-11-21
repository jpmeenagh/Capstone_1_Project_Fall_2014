using UnityEngine;
using System.Collections;

public class animation_ctrl_comp : MonoBehaviour {

	protected Animator animator;
	
	//    public float DirectionDampTime = .25f;
	
	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	void Update () 
	{
		
		if(animator)
		{
			
			//get the current state
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			
			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.robot_idle"))
			{
				
			}
			
			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.robot_run"))
			{


			}
			
			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.robot_flamethrower"))
			{
				animator.SetInteger("animRoboAbility", 0);
			}
			
			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.robot_saboteur"))
			{
				animator.SetInteger("animRoboAbility", 0);
			}
			
			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.robot_healingRay"))
			{
				animator.SetInteger("animRoboAbility", 0);
			}

			if (this.transform.hasChanged == true){
				animator.SetBool(Animator.StringToHash("animRoboSpeed"), true);
			}
			
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

	public void animAbil(string abil){
		if (animator) {
			if(abil.Equals ("flame")){
				animator.SetInteger("animRoboAbility", 1);
			}
			if(abil.Equals ("heal")){
				animator.SetInteger("animRoboAbility", 2);
			}
			if(abil.Equals ("sabo")){
				animator.SetInteger("animRoboAbility", 3);
			}

				
		
		}
	}
}