using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject asteroidExplosion;
	public GameObject shipExpolision;

	private UIController uiController;
	private GameController controller;

	private const int ASTEROID_SCORE = 10;

	void Start ()
	{
		// Setup UI
		GameObject uiControllerObject = GameObject.FindWithTag ("UIController");
		uiController =	uiControllerObject.GetComponent<UIController> ();

		GameObject gameObject = GameObject.FindWithTag ("GameController");
		controller = gameObject.GetComponent<GameController> ();

		if (controller == null) {
			print ("Cannot find GameController object");
		}
	}


	// Only destroy Bullet and Player
	void OnTriggerEnter (Collider other) 
	{
		if (other.CompareTag ("Player")) {
			Destroy (other.gameObject);
			Instantiate (shipExpolision, this.transform.position, this.transform.rotation);
			DestroySelf ();
			controller.EndGame ();
		} 

		if (other.CompareTag ("Bullet")) {
			Destroy (other.gameObject);
			uiController.UpdateScore (ASTEROID_SCORE);
			DestroySelf ();
		}
	}

	void DestroySelf () {
		Destroy (this.gameObject);
		Instantiate (asteroidExplosion, this.transform.position, this.transform.rotation);
		Destroy (this.transform.parent.gameObject);
	}
}
