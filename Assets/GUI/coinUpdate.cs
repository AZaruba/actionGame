using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinUpdate : MonoBehaviour {

	public Text txt;
	public GameObject scoreSource;
	public int coinsInLevel;
	private int lastCount;

	// Use this for initialization
	void Start () {
        coinsInLevel = GameObject.FindGameObjectWithTag("coinGen").GetComponent<itemPlacementScript>().getNumCoins();
		txt.text = "0/" + coinsInLevel.ToString();
		lastCount = 0;
	}

    public void resetCount()
    {
        lastCount = 0;
        txt.text = "0/" + coinsInLevel.ToString();
    }

	public void collectCoin () {
		int coinCount = scoreSource.GetComponent<collectCollision> ().getCoinCount();
		if (coinCount != lastCount) {
			txt.GetComponent<UnityEngine.UI.Text> ().text = coinCount.ToString() + "/" + coinsInLevel.ToString();
			lastCount = coinCount;
		}
        // change color of goal item text
        if (coinCount == coinsInLevel)
        {
            GameObject.FindGameObjectWithTag("goalEntity").GetComponent<goalItemInteraction>().scoreText.color = Color.white;
        }
	}
}
