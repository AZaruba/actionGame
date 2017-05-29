using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalItemInteraction : MonoBehaviour {
	
	void OnDestroy() {
		GameObject scores = GameObject.FindGameObjectWithTag ("metrics");
		int maxScore = scores.GetComponent<coinUpdate>().coinsInLevel;

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		int currScore = player.GetComponent<collectCollision> ().getCoinCount ();

		if (currScore == maxScore) {
			// player wins!
		} else {
			// let the player know what they need to do
		}
	}
}
