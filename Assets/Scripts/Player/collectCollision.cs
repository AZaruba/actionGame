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

	private int coinCount;

    public string metricName;

	// Use this for initialization
	void Start () {
		layerMask = 1 << 8;
        goalMask = 1 << 11;
		coinCount = 0;
        foreach (GameObject metric in GameObject.FindGameObjectsWithTag("metrics"))
        {
            if (metric.name.Equals("Score"))
                blueScore = metric.GetComponent<coinUpdate>();
			if (metric.name.Equals ("greenScore"))
				greenScore = metric.GetComponent<coinUpdate> ();
        }
    }

	public int getCoinCount() {
		return this.coinCount;
	}

	public void setCoinCount(int count) {
		this.coinCount = count;
	}

    public void coinCollision()
    {
        hitArray = Physics.OverlapSphere(transform.position, 1.0f, layerMask);
        if (hitArray.Length > 0)
        {
			if (hitArray [0].gameObject.name.Contains ("blueGem")) {
				Destroy (hitArray [0].gameObject);
				coinCount++;

				blueScore.collectCoin ();
			}
			if (hitArray [0].gameObject.name.Contains ("greenGem")) {
				Destroy (hitArray [0].gameObject);
				coinCount++;

				greenScore.collectCoin ();
			}
        }
    }

    public void goalItemCollision()
    {
        hitArray = Physics.OverlapSphere(transform.position, 1.0f, goalMask);
        if (hitArray.Length > 0 && blueScore.coinsInLevel == coinCount)
        {
            Destroy(hitArray[0].gameObject.GetComponent<goalItemInteraction>().scoreText);
            Destroy(hitArray[0].gameObject);
        }
    }
}
