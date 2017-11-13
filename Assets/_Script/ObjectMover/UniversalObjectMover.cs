using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalObjectMover : MonoBehaviour {

	public float speedFactor;
	private Rigidbody body;

	void Start ()
	{
		body = GetComponent<Rigidbody> ();

		float yRot = body.rotation.eulerAngles.y;
		yRot = yRot / 180.0f * Mathf.PI;

		Vector3 movement = new Vector3 (Mathf.Sin(yRot), 0.0f, Mathf.Cos(yRot));

		body.velocity = movement * speedFactor;
	}
		
}
