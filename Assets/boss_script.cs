using UnityEngine;
using System.Collections;

public class boss_script : MonoBehaviour {
	public GameObject victory;
	int health = 100;
	public GameObject pinModel;
	public GameObject player;
	GameObject[] torches;
	public AudioClip winClip;
	public AudioClip boss_damage1;
	public AudioClip boss_damage2;
	// Use this for initialization
	void Start () {
		victory.SetActive(false);
		StartCoroutine(timer(2));
		StartCoroutine(Torch_up(2));
		torches = GameObject.FindGameObjectsWithTag("Torch");
		for(int f = 0; f < torches.Length; f++){
			torches[f].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (health <= 0 && player.activeSelf){
			Debug.Log ("Game Ended");
			gameObject.SetActive(false);
			victory.SetActive(true);
			AudioSource.PlayClipAtPoint(winClip, transform.position);
		}


	}

	void OnTriggerEnter2D (Collider2D col)
	{
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Arrow"){
			health = health - 1;
			Debug.Log("Boss hit!");
			col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			AudioSource.PlayClipAtPoint(boss_damage1, transform.position);
		}
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Fire_arrow"){
			health = health - 10;
			Debug.Log("Fire Arrow Hit!");
			col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			AudioSource.PlayClipAtPoint(boss_damage2, transform.position);
		}
	}

	void shootPin() {
		// Create arrow at current position
		GameObject pin = (GameObject) Instantiate(pinModel, transform.position, transform.rotation);
		// Create target
		Vector3 target = player.transform.position - pin.transform.position;
		target.Normalize();
		// Rotate by Atan2 of target
		pin.transform.Rotate(Vector3.forward, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
		// Apply velocity (remember right is "forward")
		pin.rigidbody2D.velocity = pin.transform.right * 10;
		
	}

	IEnumerator timer(float x){
		yield return new WaitForSeconds(x);
		shootPin();
		if (health > 50){
			StartCoroutine(timer(.5f));
		}else{
			StartCoroutine(timer(.25f));
		}
	}

	IEnumerator Torch_up(float x){
		yield return new WaitForSeconds(x);
		GameObject t = torches[Random.Range(0,torches.Length)];
		t.SetActive(true);
		StartCoroutine(Torch_down(5f, t));
	}

	IEnumerator Torch_down(float x, GameObject t){
		yield return new WaitForSeconds(x);
		t.SetActive(false);
		StartCoroutine(Torch_up(2f));
	}


}