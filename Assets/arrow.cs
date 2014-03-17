using UnityEngine;
using System.Collections;

public class arrow : MonoBehaviour {
	public Rigidbody2D arrowModel;
	//float bulletSpeed = 20;
	// Use this for initialization
	void Start () {
	
	}
	//untested
	// Update is called once per frame
	void Update() {
	
			// Put this in your update function
			if (Input.GetButtonDown("Fire1")) {
				
				// Instantiate the projectile at the position and rotation of this transform
				//GameObject clone = Instantiate(projectile, transform.position, transform.rotation);
				
				// Add force to the cloned object in the object's forward direction
				//clone.rigidbody.AddForce(clone.transform.forward * shootForce);
			shootArrow();
			}
	}

	void shootArrow() {
		Rigidbody arrow = (Rigidbody) Instantiate(arrowModel, transform.position, transform.rotation);
		arrow.velocity = transform.forward * 5;
		}
}
