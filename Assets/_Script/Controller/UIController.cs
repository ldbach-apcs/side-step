using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour {

	public Text scoreText, gameOverText, restartText, highScoreText;



	private int score = 0;

	// Use this for initialization
	void Start () {
		gameOverText.text = "";
		restartText.text = "";
		UpdateScore (0);
	}

	public void UpdateGameOverText ()
	{
		gameOverText.text = "Game over";
	}

	public void UpdateRestartText ()
	{
		restartText.text = "Tap anywhere to try again";
	}

	public void UpdateScore (int increment)
	{
		score += increment;
		scoreText.text = "Score: " + score;
	}

	public void UpdateHighScore () {
		highScoreText.text = "Highscore: " + SaveLoad.LoadHighScore ();
	}

	public int GetScore () {
		return score;
	}
}
