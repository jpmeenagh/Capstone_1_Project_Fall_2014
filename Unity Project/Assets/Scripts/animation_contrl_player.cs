using UnityEngine;
using System.Collections;

public class animation_contrl_player : MonoBehaviour {

	//Transform LastTransTMPT;
	Vector3 LastTrasVec;

	protected Animator animator;

	//    public float DirectionDampTime = .25f;
	
	void Start () 
	{
		animator = GetComponent<Animator>();
		//LastTransTMPT = this.transform;
		LastTrasVec = this.transform.position;
	
	}
	
	void Update () 
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		//Vector3 LastTransDir = LastTransTMPT.InverseTransformDirection();
		//Vector3 ThisTransDir = this.transform.InverseTransformDirection ();

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

			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.main_run_back"))
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

			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.main_sideRun_L"))
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

			if(stateInfo.nameHash == Animator.StringToHash("Base Layer.main_sideRun_R"))
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


			if (this.transform.InverseTransformPoint(LastTrasVec).x > 0){
				animator.SetFloat("animDir2", -1.0f);
				animator.SetInteger("animDir", 2);
				if (this.transform.InverseTransformPoint(LastTrasVec).z > 0){
					animator.SetFloat("animDir2", 1.0f);
					animator.SetInteger("animDir", 3);
				}
			}

			if (this.transform.InverseTransformPoint(LastTrasVec).x < 0){
				animator.SetFloat("animDir2", -1.0f);
				animator.SetInteger("animDir", 3);
				if (this.transform.InverseTransformPoint(LastTrasVec).z > 0){
					animator.SetFloat("animDir2", 1.0f);
					animator.SetInteger("animDir", 2);
				}
			}

			if (this.transform.InverseTransformPoint(LastTrasVec).x > -0.05 &&
			    this.transform.InverseTransformPoint(LastTrasVec).x < 0.05){
				if (this.transform.InverseTransformPoint(LastTrasVec).z > 0){
					animator.SetInteger("animDir", 1);
				}
				
				if (this.transform.InverseTransformPoint(LastTrasVec).z < 0){
					animator.SetInteger("animDir", 0);
				}
				
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

		//LastTransTMPT = this.transform;
		LastTrasVec = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
	} 
}
