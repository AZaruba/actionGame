using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinUpdate : MonoBehaviour {

	public Text txt;
	public GameObject scoreSource;
	private int lastCount;

	// Use this for initialization
	void Start () {
		txt.text = "0";
		lastCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		int coinCount = scoreSource.GetComponent<collectCollision> ().getCoinCount();
		if (coinCount != lastCount) {
			txt.GetComponent<UnityEngine.UI.Text> ().text = coinCount.ToString();
			lastCount = coinCount;
		}
	}
}
