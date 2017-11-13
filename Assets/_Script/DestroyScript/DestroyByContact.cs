using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject asteroidExplosion;
	public GameObject shipExpolision;

	// Only destroy Bullet and Player
	void OnTriggerEnter (Collider other) 
	{
		if (other.CompareTag ("Player")) {
			Destroy (other.gameObject);
			Instantiate (shipExpolision, this.transform.position, this.transform.rotation);
			DestroySelf ();
		} 

		if (other.CompareTag ("Bullet")) {
			Destroy (other.gameObject);
			DestroySelf ();
		}


	}

	void DestroySelf () {
		Destroy (this.gameObject);
		Instantiate (asteroidExplosion, this.transform.position, this.transform.rotation);
		Destroy (this.transform.parent.gameObject);
	}
}
