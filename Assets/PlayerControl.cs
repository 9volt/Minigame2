using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public bool facingRight = false;			// For determining which way the player is currently facing.
	[HideInInspector]
	
	public float moveForce = 15f;			// Amount of force added to move the player left and right.
	public float maxSpeed =2f;				// The fastest the player can travel in the x axis.
	public Rigidbody2D arrowModel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

		// Cache the horizontal input.
		float v = Input.GetAxis("Vertical");
		
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(v * rigidbody2D.velocity.y < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.up * v * moveForce);
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.y) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.y) * maxSpeed, rigidbody2D.velocity.x);


		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();


		
		// Shoot an Arrow
		if (Input.GetButtonDown("Fire1")) {

			shootArrow();
		}
	}


	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

void shootArrow() {
	Rigidbody2D arrow = (Rigidbody2D) Instantiate(arrowModel, transform.position, transform.rotation);
	arrow.velocity = transform.forward * 5;
}

}
