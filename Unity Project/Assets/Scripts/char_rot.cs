using UnityEngine;
using System.Collections;

public class char_rot : MonoBehaviour {

	public float sens = 10.0f;

	public float angularVelocity = 12.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//rotates the player using the right stick
		transform.Rotate(0, Input.GetAxis("char_rot") * sens/* * sensitivityX */, 0);

		//transform.eulerAngles.z -= rh  rotateSensitivity  Time.deltaTime;

		//Vector3 tempAngle = transform.eulerAngles;
		//tempAngle.y -= Input.GetAxis("char_rot") * sens * Time.deltaTime;
		//transform.eulerAngles = tempAngle;

		//transform.LookAt(transform.position + new Vector3(Input.GetAxis("char_rot"), Input.GetAxis("char_rot_vert"), 0.0f), -Vector3.forward);
		
		/*var direction = new Vector3(Input.GetAxis("char_rot"), Input.GetAxis("char_rot_vert"), 0);
		
		if (direction.magnitude > 0) //Only update rotation if we’re actually pressing a direction, otherwise idle snaps rotation upward
				
		{
			
			var currentRotation = Quaternion.LookRotation(Vector3.forward, direction);
			
			//The first parameter tells us that forward, which is the positive Z axis, should still remain along the positive Z axis. The second parameter is where up, or positive Y, should face. That facing should be the same direction as wherever the analog stick is pointing.
			
			//As for what a Quaternion is, I have no idea. As far as I’m concerned, Quaternions are magical elves you conjure up to spin things around for you. Trust me on that one. 
			
			transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, Time.deltaTime * angularVelocity);
			
			//This takes the current rotation and changes it by conjuring up a Quaternion elf who uses the Lerp wand to do so. The parameters are your starting rotation (just your current rotation), your desired end-point (where your analog stick is currently pointing), and how far along you should be rotated towards that end point in this frame (number usually between 0 and 1 which represents a % change, e.g. the parameters (0, 30, 0.5) would cause you to rotate 15* in the first frame, 7.5* in the second frame and so on. 
			
		}*/

	}
}
