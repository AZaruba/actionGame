using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectCollision : MonoBehaviour {

	private RaycastHit coinHit;
	private Collider[] hitArray;
	private int layerMask;

	private int coinCount;

	// Use this for initialization
	void Start () {
		layerMask = 1 << 8;
		coinCount = 0;
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
			GameObject scores = GameObject.FindGameObjectWithTag ("metrics");
			scores.GetComponent<coinUpdate> ().collectCoin ();
        }
    }
}
