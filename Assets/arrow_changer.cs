using UnityEngine;
using System.Collections;

public class arrow_changer : MonoBehaviour {
	public Sprite fireModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		// If the colliding gameobject is an Enemy...
		if(col.gameObject.tag == "Torch"){
			GetComponent<SpriteRenderer>().sprite = fireModel;
			gameObject.tag = "Fire_arrow";
			this.enabled = false;
		}
	}
}
