using UnityEngine;
using System.Collections;

public class char_rot : MonoBehaviour {

	public float sens = 10.0f;

	//public float angularVelocity = 12.0f;

	public float radialDeadZone = 0.25f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hitinfo;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // casts a ray from the center of the camer to the mouse pointer
		if (Physics.Raycast(ray, out hitinfo)) // stores the collision info of the raycast
		{
			transform.LookAt(new Vector3(hitinfo.point.x, this.transform.position.y,hitinfo.point.z)); // look at the point of space you hit
		}

		//rotates the player using the right stick
		//transform.Rotate(0, Input.GetAxis("char_rot") * sens/* * sensitivityX */, 0);

		//transform.eulerAngles.z -= rh  rotateSensitivity  Time.deltaTime;

		//Vector3 tempAngle = transform.eulerAngles;
		//tempAngle.z -= Input.GetAxis("char_rot") * sens * Time.deltaTime;
		//transform.eulerAngles = tempAngle;

		//transform.LookAt(transform.position + new Vector3(Input.GetAxis("char_rot"), Input.GetAxis("char_rot_vert"), 0.0f)/*, -Vector3.forward*/);

		//KINDA WORKS
//		Vector3 lookDir = new Vector3(Input.GetAxis("char_rot"), 0.0f, -Input.GetAxis("char_rot_vert"));
		//new
//		lookDir.Normalize();

		//float fHeading = Vector3.Dot(Vector3.right, lookDir);
		//Vector3 vNewRotation = transform.rotation.eulerAngles;
		//vNewRotation.y = fHeading * 90.0f;
		//if(lookDir.z > 0.0f)
		//{    
			//Debug.Log(new Vector2(Input.GetAxis("RightStickX"), Input.GetAxis("RightStickY"))); //report value of right thumbstick
			
			//vNewRotation.x = 180.0f - vNewRotation.x;    
		//}

		//transform.rotation = Quaternion.Euler(vNewRotation);

//		if (/*lookDir.magnitude > 0.0f &&*/ lookDir.magnitude > radialDeadZone) {
//				transform.LookAt (transform.position + lookDir, Vector3.up);
//				}
		/*var direction = new Vector3(Input.GetAxis("char_rot"), Input.GetAxis("char_rot_vert"), 0);
		
		if (direction.magnitude > 0) //Only update rotation if we’re actually pressing a direction, otherwise idle snaps rotation upward
				
		{
			
			var currentRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up/*direction);
			
			//The first parameter tells us that forward, which is the positive Z axis, should still remain along the positive Z axis. The second parameter is where up, or positive Y, should face. That facing should be the same direction as wherever the analog stick is pointing.
			
			//As for what a Quaternion is, I have no idea. As far as I’m concerned, Quaternions are magical elves you conjure up to spin things around for you. Trust me on that one. 
			
			transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, Time.deltaTime * angularVelocity);
			
			//This takes the current rotation and changes it by conjuring up a Quaternion elf who uses the Lerp wand to do so. The parameters are your starting rotation (just your current rotation), your desired end-point (where your analog stick is currently pointing), and how far along you should be rotated towards that end point in this frame (number usually between 0 and 1 which represents a % change, e.g. the parameters (0, 30, 0.5) would cause you to rotate 15* in the first frame, 7.5* in the second frame and so on. 
			
		}*/

	}
}
