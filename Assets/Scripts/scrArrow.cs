using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrArrow : MonoBehaviour {
	Rigidbody2D rb;
	Quaternion forwards;
	public Trick trick = Trick.none;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (transform.position, rb.velocity, Color.blue); // Arrowhead is supposed to follow trajectory, Can be improved but I'm not gonna bother rn
		if(!rb.freezeRotation)
			transform.LookAt(rb.velocity);

		}
		
	// Collisions
	void OnCollisionEnter2D(Collision2D collision) {
		//Debug.Log (collision.gameObject.name);
		switch (collision.gameObject.name) {
		case "Player(Clone)": // An enemy has been hit
			if (!collision.gameObject.CompareTag (tag)) {
				collision.gameObject.SendMessageUpwards ("Hit", trick);
				rb.freezeRotation = true;
				rb.bodyType = RigidbodyType2D.Static;
			}
			break;
		case "Arrow(Clone)": // Trickshot
			if (trick == Trick.none)
				trick = Trick.trickshot;
			break;
		default: // stick into the ground
			rb.freezeRotation = true;
			rb.bodyType = RigidbodyType2D.Static;
			break;
		}
	}

	public void SetTrick(Trick t) {
		trick = t;
	}
}
