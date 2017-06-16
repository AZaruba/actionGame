using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour {

	public GameObject player;
	public GameObject blueCoinGen;
    public GameObject greenCoinGen;
    public GameObject score;
    public GameObject greenScore;

	public void backToOne() {
		player.GetComponent<collectCollision> ().setBlueCoinCount (0);
        player.GetComponent<collectCollision>().setGreenCoinCount(0);
        blueCoinGen.GetComponent<itemPlacementScript> ().genCoins ();
        greenCoinGen.GetComponent<itemPlacementScript>().genCoins();
        score.GetComponent<coinUpdate>().resetCount();
        greenScore.GetComponent<coinUpdate>().resetCount();
	}
}
