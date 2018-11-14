using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrShoot : MonoBehaviour {
	public string teamName;

	public float charge = 0.0f;
	public float maxCharge = 100.0f;
	public float increment = 1.0f;

	public string inputAxis = "Fire1";
	public float constPower = 10;
	public GameObject projectile;
	public static Queue<GameObject> litter = new Queue<GameObject> ();
	private static int maxProjectiles = 25;

	void Start() {
		teamName = transform.parent.tag; // Get the team of the parent object
	}

	void Update() {
		// Charge projectile
		if(Input.GetButton(inputAxis) && (charge < maxCharge)) {
			charge += increment;
			if (charge >= maxCharge) charge = maxCharge; // Maximum charge

			/*
			Debug.Log (
				inputAxis + ": " + Input.GetButton (inputAxis) 
				+ "\nCharge:" + charge + "/" + maxCharge);
				*/
		}

		// Fire projectile
		if (Input.GetButtonUp (inputAxis)) {
			// Create projectile
			GameObject instanceProjectile = Instantiate (projectile, transform.position, transform.rotation);
			instanceProjectile.tag = teamName;
			// Tricks
			if (litter.Count == 1)
				instanceProjectile.SendMessage ("SetTrick", Trick.skillshot); // SendMessage because I don't wanna mess with inheritance.
			if (charge == maxCharge)
				instanceProjectile.SendMessage ("SetTrick", Trick.powershot);
			// Launch
			instanceProjectile.GetComponent<Rigidbody2D> ().AddForce (transform.rotation*Vector3.right*constPower*charge);
			// Keep track of projectile for garbage collection
			litter.Enqueue(instanceProjectile);
			if (litter.Count > maxProjectiles) Destroy(litter.Dequeue ()); // Remove least recent shot from litter and destroy instance
			charge = 0;
		}
	}
}
