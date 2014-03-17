using UnityEngine;
using System.Collections;

public class arrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	//untested
	// Update is called once per frame
	void Update () {
		var projectile : Transform;
		var bulletSpeed : float = 20;
		
		function Update () {
			// Put this in your update function
			if (Input.GetButtonDown("Fire1")) {
				
				// Instantiate the projectile at the position and rotation of this transform
				var clone : Transform;
				clone = Instantiate(projectile, transform.position, transform.rotation);
				
				// Add force to the cloned object in the object's forward direction
				clone.rigidbody.AddForce(clone.transform.forward * shootForce);
			}
		}
	}
}
