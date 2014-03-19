using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public bool facingRight = false;			// For determining which way the player is currently facing.
	public float moveForce = 30f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 200f;				// The fastest the player can travel in the x axis.
	public GameObject arrowModel;
	public GameObject boss;
	int health = 100;
	public GameObject lost;
	public AudioClip loseClip;
	public AudioClip damageClip;
	public AudioClip shoot;


	// Use this for initialization
	void Start () {
		lost.SetActive(false);
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

		if (health <= 0){
			Debug.Log ("Game Over");
			//gameObject.active = false;
			lost.SetActive(true);
			gameObject.SetActive(false);
			AudioSource.PlayClipAtPoint(loseClip, transform.position);
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
		// Create arrow at current position
		GameObject arrow = (GameObject) Instantiate(arrowModel, transform.position, transform.rotation);
		// Create target
		Vector3 target = boss.transform.position - arrow.transform.position;
		target.Normalize();
		// Rotate by Atan2 of target
		arrow.transform.Rotate(Vector3.forward, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
		// Apply velocity (remember right is "forward")
		arrow.rigidbody2D.velocity = arrow.transform.right * 10;
		AudioSource.PlayClipAtPoint(shoot, transform.position);

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Enemy"){
			health = health-20;
			Debug.Log("Ow!");
			AudioSource.PlayClipAtPoint(damageClip, transform.position);
		}

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Pin"){
			health = health - 20;
			Debug.Log("You're hit!");
			AudioSource.PlayClipAtPoint(damageClip, transform.position);
			col.gameObject.SetActive(false);
		}
		
	}
//	IEnumerator restart_game(float x){
		//gameObject.active = false;
		//Debug.Log("trying to reload");
		//yield return new WaitForSeconds(x);
		//Application.LoadLevel("/Assets/boss_fight_2d");		
	//}

}
