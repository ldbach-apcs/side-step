using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary {
	public float xMin, xMax;
}

public class PlayerController : MonoBehaviour {

	public Camera mainCamera;
	public float speedFactor;
	public float tiltFactor;
	public GameObject explosion;

	// Positioning and control
	private const float CONTROL_THRESHOLD = 0.025f;
	private Rigidbody body;
	private Boundary boundary = new Boundary();
	private AudioSource audioSource;


	// Shooting
	public GameObject bullet;
	public Transform spawnFront;
	public Transform spawnLeft;
	public Transform spawnRight;


	public int bulletLevel;

	// private float startFireRate = 1.2f;
	private const float minFireRate = 0.45f;
	private float fireRate = 1.2f;
	private float fireNext = 0.0f;

	private UIController uiController;
	private const int RUNE_SCORE = 5;	

	/*
	 * Set game boundary
	 */ 
	void Start ()
	{
		float areaWidth = mainCamera.orthographicSize * mainCamera.aspect - 1.0f;

		boundary.xMin = -areaWidth;
		boundary.xMax = areaWidth;

		body = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();

		// Setup UI
		GameObject uiObject = GameObject.FindWithTag ("UIController");
		uiController = uiObject.GetComponent <UIController> ();

		// fireRate = startFireRate;
	}

	// Update capture firing
	void Update ()
	{
		// fireRate = Mathf.Max (minFireRate, startFireRate - 0.05f * bulletLevel);

		if (Time.time >= fireNext && bulletLevel != 0) {
			fireNext = Time.time + fireRate;
			audioSource.Play ();

			// Triple bullet
			if (bulletLevel >= 10) {
				spawnBulletLeft ();
				spawnBulletFront ();
				spawnBulletRight ();
			} else if (bulletLevel >= 5) { // Double bullet
				spawnBulletLeft ();
				spawnBulletRight ();
			} else { // Single bullet
				spawnBulletFront ();
			}
		}
	}

	void spawnBulletLeft()
	{
		Instantiate (bullet, spawnLeft);
	}

	void spawnBulletRight ()
	{
		Instantiate (bullet, spawnRight);
	}

	void spawnBulletFront () 
	{
		Instantiate (bullet, spawnFront);
	}

	// FixedUpdate capture movemnet
	void FixedUpdate ()
	{
		float moveHorizontal = Input.acceleration.x;
		// if (Mathf.Abs (moveHorizontal) < CONTROL_THRESHOLD)
		//	moveHorizontal = 0.0f;

		// float moveHorizontal = Input.GetAxis("Horizontal");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f);

		// movement.Normalize();
		body.velocity = movement * speedFactor;

		body.position = new Vector3(
			Mathf.Clamp ( body.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			0.0f);
		
		body.rotation = Quaternion.Euler (0.0f, 0.0f, body.velocity.x * -tiltFactor);
	}

	// Detect getting rune
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Rune")) {
			uiController.UpdateScore (RUNE_SCORE);
			bulletLevel++;
			Destroy (other.gameObject);
			if (fireRate > minFireRate)
				fireRate -= 0.05f;
		}
	}
}
