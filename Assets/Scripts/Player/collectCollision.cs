using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectCollision : MonoBehaviour {

	private RaycastHit coinHit;
	private Collider[] hitArray;
    private GameObject scores;
	private int layerMask;
    private int goalMask;

	private int coinCount;

	// Use this for initialization
	void Start () {
		layerMask = 1 << 8;
        goalMask = 1 << 11;
		coinCount = 0;
        scores = GameObject.FindGameObjectWithTag("metrics");
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
            Destroy(hitArray[0].gameObject);
			coinCount++;

			scores.GetComponent<coinUpdate> ().collectCoin ();
        }
    }

    public void goalItemCollision()
    {
        hitArray = Physics.OverlapSphere(transform.position, 1.0f, goalMask);
        if (hitArray.Length > 0 && scores.GetComponent<coinUpdate>().coinsInLevel == coinCount)
        {
            Destroy(hitArray[0].gameObject.GetComponent<goalItemInteraction>().scoreText);
            Destroy(hitArray[0].gameObject);
        }
    }
}
