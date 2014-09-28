using UnityEngine;
using System.Collections;

public class cam : MonoBehaviour {

	public Transform target;
	public float distanceZ = 20.0f;
	public float distanceY = 20.0f;
	public float distanceX = 20.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3 (target.position.x + distanceX, target.position.y + distanceY, target.position.z - distanceZ);
	}
}