using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit (Collider other)
	{
		if (other.CompareTag ("Enemy"))
			Destroy (other.transform.parent.gameObject);
		Destroy (other.gameObject);

	}
}
