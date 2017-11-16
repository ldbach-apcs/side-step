using System.Collections;
using UnityEngine;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public static class SaveLoad {

	private const string SAVE_TAG = "sidestep-save";

	public static int LoadHighScore ()
	{
		return PlayerPrefs.GetInt (SAVE_TAG, 0);
	}

	public static void SaveHighScore (int score)
	{
		if (score > LoadHighScore ())
			PlayerPrefs.SetInt (SAVE_TAG, score);
	}
}
