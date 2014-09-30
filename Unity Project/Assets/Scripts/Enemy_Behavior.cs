using UnityEngine;
using System.Collections;


public class Enemy_Behavior : MonoBehaviour {
	
	
	public Transform target;
	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    		transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);

	
	}
}
