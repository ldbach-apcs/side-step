using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Camera mainCamera;
	public GameObject backgroundMusic;
	public GameObject asteroid_01;
	public GameObject asteroid_02;
	public GameObject asteroid_03;
	public GameObject rune;

	// UI
	private UIController uiController;

	private int asteroidCount = 8;
	private int[] asteroidCountVar = new int[] { -1, 3 };
	private List<GameObject> objects = new List<GameObject>();

	private float startWait = 1.5f;
	private float spawnWait = 0.5f;
	private float waveWait = 0.6f;

	private bool spawnRune = false;

	private Vector3 spawnValues = Vector3.zero;
	private bool isGameOver = false;
	private bool canRestart = false;



	// Use this for initialization
	void Start () {
		
		Input.backButtonLeavesApp = true;

		objects.Add (asteroid_01);
		objects.Add (asteroid_02);
		objects.Add (asteroid_03);
		objects.Add (rune);

		// setup spawn region
		spawnValues.x = mainCamera.orthographicSize * mainCamera.aspect;
		spawnValues.y = 0;
		spawnValues.z = mainCamera.orthographicSize * 2 + 2;

		// setup music
		GameObject musicObject = GameObject.FindWithTag ("Music");
		if (musicObject == null)
			Instantiate (backgroundMusic, Vector3.zero, Quaternion.identity);

		// Setup UI
		GameObject uiControllerObject = GameObject.FindWithTag ("UIController");
		uiController =	uiControllerObject.GetComponent<UIController> ();

		StartCoroutine (SpawnWaves ());
	}

	void Update ()
	{
		if (canRestart) {
			//if (Input.GetKeyDown (KeyCode.R)) 	
			if (Input.touchCount > 0)
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
	
	IEnumerator SpawnWaves () { 
		yield return new WaitForSeconds (startWait);

		while (!isGameOver) {

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

		yield return new WaitForSeconds (waveWait);

		//restartText.text = "Tap anywhere to restart";
		uiController.UpdateRestartText ();

		canRestart = true;
	}

	void SpawnRandom () {
		Vector3 spawnPos = 
			new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
		Quaternion spawnRot = Quaternion.identity;

		Instantiate (objects[(int) Random.Range (0, objects.Count - 1)], spawnPos, spawnRot);
	}

	void SpawnRune () {
		float padding = 1.0f;
		Vector3 spawnPos = 
			new Vector3 (Random.Range (-spawnValues.x + padding, spawnValues.x - padding), spawnValues.y, spawnValues.z);
		Quaternion spawnRot = Quaternion.identity;

		Instantiate (objects[objects.Count - 1], spawnPos, spawnRot);
	}

	public void EndGame ()
	{
		isGameOver = true;
		uiController.UpdateGameOverText ();
		//gameOverText.text = "Game Over";
	}
}
