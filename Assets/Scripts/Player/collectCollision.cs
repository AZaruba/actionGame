using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectCollision : MonoBehaviour {

	private RaycastHit coinHit;
	private Collider[] hitArray;
    private coinUpdate blueScore;
	private coinUpdate greenScore;
	private int layerMask;
    private int goalMask;

	private int blueCoinCount;
	private int greenCoinCount;

    public string metricName;

	// Use this for initialization
	void Start () {
		layerMask = 1 << 8;
        goalMask = 1 << 11;
		blueCoinCount = 0;
		greenCoinCount = 0;
        foreach (GameObject metric in GameObject.FindGameObjectsWithTag("metrics"))
        {
            if (metric.name.Equals("Score"))
                blueScore = metric.GetComponent<coinUpdate>();
			if (metric.name.Equals ("greenScore"))
				greenScore = metric.GetComponent<coinUpdate> ();
        }
    }

	public int getCoinCount(string type) {
        if (type.Equals("blue"))
		    return this.blueCoinCount;
        if (type.Equals("green"))
            return this.greenCoinCount;
        return 0;
    }

	public void setBlueCoinCount(int count) {
		this.blueCoinCount = count;
	}

    public void setGreenCoinCount(int count)
    {
        this.greenCoinCount = count;
    }

    public void coinCollision()
    {
        hitArray = Physics.OverlapSphere(transform.position, 1.0f, layerMask);
        if (hitArray.Length > 0)
        {
			if (hitArray [0].gameObject.name.Contains ("blueGem")) {
				Destroy (hitArray [0].gameObject);
				blueCoinCount++;

				blueScore.collectCoin ();
			}
			if (hitArray [0].gameObject.name.Contains ("greenGem")) {
				Destroy (hitArray [0].gameObject);
				greenCoinCount++;

				greenScore.collectCoin ();
			}
        }
    }

    public void goalItemCollision()
    {
        hitArray = Physics.OverlapSphere(transform.position, 1.0f, goalMask);
		if (hitArray.Length > 0) {
			if (hitArray [0].name.Contains ("blue") && blueScore.coinsInLevel == blueCoinCount) {
				Destroy (hitArray [0].gameObject.GetComponent<goalItemInteraction> ().scoreText);
				Destroy (hitArray [0].gameObject);
			}
			if (hitArray [0].name.Contains ("green") && greenScore.coinsInLevel == greenCoinCount) {
				Destroy (hitArray [0].gameObject.GetComponent<goalItemInteraction> ().scoreText);
				Destroy (hitArray [0].gameObject);
			}
		}
    }
}
