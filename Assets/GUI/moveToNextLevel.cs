using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToNextLevel : MonoBehaviour {

	public string levelName;

	// Update is called once per frame
	void Update () {
		if (!GameObject.Find ("blueCrystal")) {
			if (Input.GetKeyDown (KeyCode.O)) {
				SceneManager.LoadScene (levelName, LoadSceneMode.Single);
			}
		}
			
	}
}
