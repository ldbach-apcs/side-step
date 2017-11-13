using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public Camera mainCamera;
	public GameObject asteroid_01;
	public GameObject asteroid_02;
	public GameObject asteroid_03;
	public GameObject rune;


	private int asteroidCount = 8;
	private int[] asteroidCountVar = new int[] { -1, 3 };
	private List<GameObject> objects = new List<GameObject>();

	private float startWait = 1.0f;
	private float spawnWait = 0.5f;
	private float waveWait = 0.6f;

	private bool spawnRune = false;

	private Vector3 spawnValues = Vector3.zero;

	// Use this for initialization
	void Start () {
		objects.Add (asteroid_01);
		objects.Add (asteroid_02);
		objects.Add (asteroid_03);
		objects.Add (rune);

		spawnValues.x = mainCamera.orthographicSize * mainCamera.aspect;
		spawnValues.y = 0;
		spawnValues.z = mainCamera.orthographicSize * 2 + 2;

		StartCoroutine (SpawnWaves ());
	}
	
	IEnumerator SpawnWaves () { 
		yield return new WaitForSeconds (startWait);

		while (true) {

			for (int i = 0; i < asteroidCount; ++i) {
				SpawnRandom ();
				yield return new WaitForSeconds (spawnWait);
			}

			if (spawnRune) {
				SpawnRune ();
			}

			asteroidCount += Random.Range (asteroidCountVar [0], asteroidCountVar [1]);
			spawnRune = !spawnRune;
		
			yield return new WaitForSeconds (waveWait);
		}
	}

	void SpawnRandom () {
		Vector3 spawnPos = 
			new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
		Quaternion spawnRot = Quaternion.identity;

		Instantiate (objects[(int) Random.Range (0, 2)], spawnPos, spawnRot);
	}

	void SpawnRune () {
		float padding = 1.0f;
		Vector3 spawnPos = 
			new Vector3 (Random.Range (-spawnValues.x + padding, spawnValues.x - padding), spawnValues.y, spawnValues.z);
		Quaternion spawnRot = Quaternion.identity;

		Instantiate (objects[3], spawnPos, spawnRot);
	}
}
