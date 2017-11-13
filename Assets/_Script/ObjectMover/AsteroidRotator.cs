using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotator : MonoBehaviour {

	private float tumbleFactor = 3;

	void Start () {
		GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitCircle * tumbleFactor;
	}
}
