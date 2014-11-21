using UnityEngine;
using System.Collections;

public class animation_contrl_player : MonoBehaviour {

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

			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.Idle"))
			{
				if (Input.GetButton("Fire2")){
					animator.SetInteger("animAttack", 1);
				}
				if (Input.GetAxis("Fire1") == 1){
					animator.SetInteger("animAttack", 2);
				}
				if (Input.GetButton("spinKey")){
					animator.SetInteger("animAbility", 1);
				}
				if (Input.GetButton("heal")){
					animator.SetInteger("animAbility", 2);
				}
				if (Input.GetButton("Fire3")){
					animator.SetInteger("animAbility", 3);
				}
			
			}

			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.main_run-full"))
			{

				if (Input.GetButton("Fire2")){
					animator.SetInteger("animAttack", 1);
				}
				if (Input.GetAxis("Fire1") == 1){
					animator.SetInteger("animAttack", 2);
				}
				if (Input.GetButton("spinKey")){
					animator.SetInteger("animAbility", 1);
				}
				if (Input.GetButton("heal")){
					animator.SetInteger("animAbility", 2);
				}
				if (Input.GetButton("Fire3")){
					animator.SetInteger("animAbility", 3);
				}
				
			}

			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.main_swordAttack_L"))
			{
				animator.SetInteger("animAttack", 0);
			}

			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.main_gunAttack_R"))
			{
				animator.SetInteger("animAttack", 0);
			}

			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.main_whirlwind"))
			{
				animator.SetInteger("animAbility", 0);
			}

			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.main_bubbleShield"))
			{
				animator.SetInteger("animAbility", 0);
			}

			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.main_healingShot"))
			{
				animator.SetInteger("animAbility", 0);
			}

			animator.SetFloat("animSpeed", h*h+v*v);

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
}
