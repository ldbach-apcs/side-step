using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotator : MonoBehaviour {

	private float tumbleFactor = 4.25f;

	void Start () {
		GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitCircle * tumbleFactor;
	}
}
