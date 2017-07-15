using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class moveToNextLevel : MonoBehaviour {

	public string levelName;

	public string passText;
	public string failText;

	private bool pass;

	public void completeLevel() {
		gameObject.GetComponent<Text> ().text = passText;
		pass = true;
	}

	public void loseLevel() {
		gameObject.GetComponent<Text> ().text = failText;
		pass = false;
	}

	void Start() {
		enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (pass) {
			if (Input.GetKeyDown (KeyCode.O)) {
				SceneManager.LoadScene (levelName, LoadSceneMode.Single);
			}
		} else if (Input.GetKeyDown (KeyCode.O)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name, LoadSceneMode.Single);
		}
	}
}
