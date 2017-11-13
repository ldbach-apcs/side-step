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
	private const float CONTROL_THRESHOLD = 1.0f / 10.0f;
	private Rigidbody body;
	private Boundary boundary = new Boundary();
	private AudioSource audioSource;


	// Shooting
	public GameObject bullet;
	public Transform spawnFront;
	public Transform spawnLeft;
	public Transform spawnRight;


	public int bulletLevel;
	private float fireRate = 0.5f;
	private float fireNext = 0.0f;

		
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
	}

	// Update capture firing
	void Update ()
	{
		if (Time.time >= fireNext && bulletLevel != 0) {
			fireNext = Time.time + fireRate;
			audioSource.Play ();

			// Triple bullet
			if (bulletLevel >= 3) {
				spawnBulletLeft ();
				spawnBulletFront ();
				spawnBulletRight ();
			} else if (bulletLevel >= 2) { // Double bullet
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
		//float moveHorizontal = Input.acceleration.z;
		//if (Mathf.Abs (moveHorizontal) < CONTROL_THRESHOLD)
		//	moveHorizontal = 0;

		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f);

		movement.Normalize();
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
			bulletLevel++;
			Destroy (other.gameObject);
			//runeAudio.Play ();
		}
	}
}
