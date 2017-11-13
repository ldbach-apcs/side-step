using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour {

	private float scrollSpeed;
	private float startScrollSpeed = -3.0f;
	private const float targetScrollSpeed = -0.3f;
	public float tileSizeZ;

	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		scrollSpeed = targetScrollSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		float newPos = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
		print ("Speed: " + scrollSpeed + "\nPosition: " + (startPos + Vector3.forward * newPos));
		this.transform.position = startPos + Vector3.forward * newPos;
	}
}
