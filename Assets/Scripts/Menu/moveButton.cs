using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveButton : MonoBehaviour {

	public float horizPosition;
	public float[] vertPositions;

	private int index;
	private RectTransform thisTransform;

	// Use this for initialization
	void Start () {
		thisTransform = gameObject.GetComponent<RectTransform> ();
		thisTransform.anchoredPosition = new Vector2 (horizPosition, vertPositions [0]);
		index = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Vertical") < 0 && index != (vertPositions.Length - 1)) {
			index++;
			thisTransform.anchoredPosition = new Vector2 (horizPosition, vertPositions [index]);
		}
		if (Input.GetAxis ("Vertical") > 0 && index != 0) {
			index--;
			thisTransform.anchoredPosition = new Vector2 (horizPosition, vertPositions [index]);
		}

		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.JoystickButton0)) {
			if (index == 0) {
				SceneManager.LoadScene ("firstScene", LoadSceneMode.Single);
			} else {
				Application.Quit ();
			}
		}
		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
	}
}
