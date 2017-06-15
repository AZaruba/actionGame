using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPlacementScript : MonoBehaviour {

    public string type;
	public GameObject coinPrefab;

	public Vector3[] coinPositionList; /* = new [] {new Vector3(2,0.8f,12), new Vector3(2,1.3f,16), 
		                                         new Vector3(2,1.8f,20), new Vector3(2,2.3f,24),
                                                 new Vector3(-2,2.8f,38), new Vector3(-2,5.5f,10)}; */

	private GameObject[] coinList;

    public int getNumCoins()
    {
        return coinList.Length;
    }

	/*
	 * This script simply initializes positions for al the coins in a given level
	 */
	public void genCoins() {
        foreach (GameObject coin in GameObject.FindGameObjectsWithTag(type+"Coin"))
        {
            Destroy(coin);
        }
		
		for (int i = 0; i < coinList.Length; i++) {
			coinList [i] = Object.Instantiate (coinPrefab);
			coinList [i].transform.position = coinPositionList [i];
            coinList[i].tag = type+"Coin";
		}
	}

	void Awake () {
		coinList = new GameObject[coinPositionList.Length];
		genCoins ();
	}
}
